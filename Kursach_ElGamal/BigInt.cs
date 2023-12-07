
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Globalization;

namespace Kursach_ElGamal
{
    public class BigInt
    {
        private uint[] data = null;
        public bool sign = true;
        public int dataLength;
        private const int maxLength = 50;
        Random rand = new Random();

        public static readonly int[] primesNumbers = {
           2,    3,    5,    7,   11,   13,   17,   19,   23,   29,   31,   37,   41,   43,   47,   53,   59,   61,   67,   71,
          73,   79,   83,   89,   97,  101,  103,  107,  109,  113,  127,  131,  137,  139,  149,  151,  157,  163,  167,  173,
         179,  181,  191,  193,  197,  199,  211,  223,  227,  229,  233,  239,  241,  251,  257,  263,  269,  271,  277,  281,
         283,  293,  307,  311,  313,  317,  331,  337,  347,  349,  353,  359,  367,  373,  379,  383,  389,  397,  401,  409,
         419,  421,  431,  433,  439,  443,  449,  457,  461,  463,  467,  479,  487,  491,  499,  503,  509,  521,  523,  541,
         547,  557,  563,  569,  571,  577,  587,  593,  599,  601,  607,  613,  617,  619,  631,  641,  643,  647,  653,  659,
         661,  673,  677,  683,  691,  701,  709,  719,  727,  733,  739,  743,  751,  757,  761,  769,  773,  787,  797,  809,
         811,  821,  823,  827,  829,  839,  853,  857,  859,  863,  877,  881,  883,  887,  907,  911,  919,  929,  937,  941,
         947,  953,  967,  971,  977,  983,  991,  997, 1009};

        public byte[] ToByteArray()
        {
            var tmp = new byte[dataLength * 4];
            for (int i = 0; i < dataLength; i++)
            {
                int index = 0;
                foreach (byte b in BitConverter.GetBytes(data[i]))
                {
                    tmp[i * 4 + index] = b;
                    index++;
                }
            }
            return tmp;
        }
        public override string ToString()
        {
            var info = NumberFormatInfo.CurrentInfo;
            string format = null;
            var _bits = data;
            int digits = -1;
            char fmt = 'R';

            bool decimalFmt = (fmt == 'g' || fmt == 'G' || fmt == 'd' || fmt == 'D' || fmt == 'r' || fmt == 'R');

            // First convert to base 10^9.
            const uint kuBase = 1000000000; // 10^9
            const int kcchBase = 9;

            int cuSrc = _bits.Length;
            int cuMax;
            try
            {
                cuMax = checked(cuSrc * 10 / 9 + 2);
            }
            catch (OverflowException e) { throw new FormatException("Format_TooLarge", e); }
            uint[] rguDst = new uint[cuMax];
            int cuDst = 0;

            for (int iuSrc = cuSrc; --iuSrc >= 0;)
            {
                uint uCarry = _bits[iuSrc];
                for (int iuDst = 0; iuDst < cuDst; iuDst++)
                {
                    Debug.Assert(rguDst[iuDst] < kuBase);
                    ulong uuRes = ((ulong)rguDst[iuDst] << 32) | uCarry;
                    rguDst[iuDst] = (uint)(uuRes % kuBase);
                    uCarry = (uint)(uuRes / kuBase);
                }
                if (uCarry != 0)
                {
                    rguDst[cuDst++] = uCarry % kuBase;
                    uCarry /= kuBase;
                    if (uCarry != 0)
                        rguDst[cuDst++] = uCarry;
                }
            }

            int cchMax;
            try
            {
                // Each uint contributes at most 9 digits to the decimal representation.
                cchMax = checked(cuDst * kcchBase);
            }
            catch (OverflowException e) { throw new FormatException("Format_TooLarge", e); }

            if (decimalFmt)
            {
                if (digits > 0 && digits > cchMax)
                    cchMax = digits;
            }

            int rgchBufSize;

            try
            {
                // We'll pass the rgch buffer to native code, which is going to treat it like a string of digits, so it needs
                // to be null terminated.  Let's ensure that we can allocate a buffer of that size.
                rgchBufSize = checked(cchMax + 1);
            }
            catch (OverflowException e) { throw new FormatException("Format_TooLarge", e); }

            char[] rgch = new char[rgchBufSize];

            int ichDst = cchMax;

            for (int iuDst = 0; iuDst < cuDst - 1; iuDst++)
            {
                uint uDig = rguDst[iuDst];
                Debug.Assert(uDig < kuBase);
                for (int cch = kcchBase; --cch >= 0;)
                {
                    rgch[--ichDst] = (char)('0' + uDig % 10);
                    uDig /= 10;
                }
            }
            for (uint uDig = rguDst[cuDst - 1]; uDig != 0;)
            {
                rgch[--ichDst] = (char)('0' + uDig % 10);
                uDig /= 10;
            }

            // Format Round-trip decimal
            // This format is supported for integral types only. The number is converted to a string of
            // decimal digits (0-9), prefixed by a minus sign if the number is negative. The precision
            // specifier indicates the minimum number of digits desired in the resulting string. If required,
            // the number is padded with zeros to its left to produce the number of digits given by the
            // precision specifier.
            int numDigitsPrinted = cchMax - ichDst;
            while (digits > 0 && digits > numDigitsPrinted)
            {
                // pad leading zeros
                rgch[--ichDst] = '0';
                digits--;
            }
            return new string(rgch, ichDst, cchMax - ichDst);
        }

        public BigInt(bool isPrime) // контсруктор для простых чисел
        {
            InitData();
            if (isPrime)
            {
                while (true)
                {
                    GenRandomArrayUint();
                    if (IsPrime())
                    {
                        break;
                    }
                }
            }
        }
        public BigInt(bool isClone, BigInt value) // конструктор копирования и для чисел с максимальным значением
        {
            InitData();
            if (isClone)
            {
                dataLength = value.dataLength;
                for (int i = 0; i < dataLength; i++)
                    data[i] = value.data[i];
            }
            else
            {
                GenRandomArrayUint(value.dataLength - 1, value.data[dataLength - 1]);
            }
        }
        public BigInt(ulong value)
        {
            InitData();
            if (value != 0)
                dataLength = 0;
            while (value != 0)
            {
                data[dataLength] = (uint)(value & 0xFFFFFFFF);
                value >>= 32;
                dataLength++;
            }
        }
        public BigInt(uint[] inData)
        {
            dataLength = inData.Length;

            data = new uint[maxLength];

            for (int i = dataLength - 1, j = 0; i >= 0; i--, j++)
                data[j] = inData[i];

            //while (dataLength > 1 && data[dataLength - 1] == 0)
            //    dataLength--;
            EqualizeLenght(this);
        }
        public BigInt(List<uint> nums)
        {
            dataLength = nums.Count;

            data = new uint[dataLength];

            for (int j = 0; j < dataLength; j++)
                data[j] = nums[j];
        }
        public void InitData()
        {
            data = new uint[maxLength];
            dataLength = 1;
        }
        private static void EqualizeLenght(BigInt v)
        {
            while (v.dataLength > 1 && v.data[v.dataLength - 1] == 0)
                v.dataLength--;
        }
        public void GenRandomArrayUint(int length = 50, uint maxValue = 0)
        {
            byte[] randBytes = new byte[length * 4];
            rand.NextBytes(randBytes);

            for (int i = 0; i < length; i++)
                data[i] = BitConverter.ToUInt32(randBytes, i * 4);

            if (maxValue != 0)
            {
                data[length] = NextUInt(maxValue);
                for (int i = length + 1; i < maxLength; i++)
                    data[i] = 0;
                length++;
            }
            dataLength = length;
            data[0] |= 0x01;
        }
        public uint NextUInt(uint maxValue)
        {
            int res = Math.Abs((int)maxValue);
            res = rand.Next(res);
            return (uint)res;
        }
        public int IntValue()
        {
            return (int)data[0];
        }
        public bool IsPrime()
        {
            //BigInt thisVal;
            //if ((this.data[maxLength - 1] & 0x80000000) != 0)        // negative
            //    thisVal = -this;
            //else
            //    thisVal = this;

            //// test for divisibility by primes < 2000
            //for (int p = 0; p < primesNumbers.Length; p++)
            //{
            //    BigInt divisor = new BigInt((ulong)primesNumbers[p]);

            //    if (divisor >= thisVal)
            //        break;

            //    BigInt resultNum = thisVal % divisor;
            //    if (resultNum.IntValue() == 0)
            //        return false;
            //}
            return true;
        }
        public static BigInt operator +(BigInt bi1, BigInt bi2)
        {
            int lenght = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
            List<uint> numbers = new List<uint>(lenght);

            long carry = 0;
            for (int i = 0; i < lenght; i++)
            {
                long sum = carry;
                if (bi1.dataLength > i)
                    sum += bi1.data[i];
                if (bi2.dataLength > i)
                    sum += bi2.data[i];
                carry = sum >> 32;
                numbers.Add((uint)(sum & 0xFFFFFFFF));
            }
            var res = new BigInt(numbers);
            EqualizeLenght(res);
            return res;
        }
        public static BigInt operator -(BigInt bi1, BigInt bi2)
        {
            uint[] d1, d2;
            bool sign = true;
            if (bi1 > bi2)
            {
                d1 = new uint[bi1.dataLength];
                d2 = new uint[bi2.dataLength];
                for (int i = 0; i < bi1.dataLength; i++)
                {
                    d1[i] = bi1.data[i];
                }
                for (int i = 0; i < bi2.dataLength; i++)
                {
                    d2[i] = bi2.data[i];
                }
            }
            else
            {
                sign = false;
                d1 = new uint[bi2.dataLength];
                d2 = new uint[bi1.dataLength];
                for (int i = 0; i < bi2.dataLength; i++)
                {
                    d1[i] = bi2.data[i];
                }
                for (int i = 0; i < bi1.dataLength; i++)
                {
                    d2[i] = bi1.data[i];
                }
            }

            List<uint> result = new List<uint>();
            uint carry = 0;
            ulong mu = 1;

            for (int i = 0; i < d2.Length; i++)
            {
                long tmp = 0;
                tmp = d1[i] - d2[i] - carry;
                if (tmp > 0)
                {
                    result.Add((uint)tmp);
                    carry = 0;
                }
                else
                {
                    ulong dt = (mu << 32) | d1[i];
                    dt -= (d2[i] + carry);
                    result.Add((uint)dt);
                    carry = 1;
                }
            }

            var res = new BigInt(result)
            {
                sign = sign
            };
            EqualizeLenght(res);

            return res;
        }
        public static BigInt operator *(BigInt bi1, BigInt bi2)
        {
            int lenght = bi1.dataLength + bi2.dataLength + 1;
            List<uint>[] numbers = new List<uint>[lenght];
            for (int i = 0; i < lenght; i++)
            {
                numbers[i] = new List<uint>();
            }

            for (int i = 0; i < bi1.dataLength; i++)
            {
                for (int j = 0; j < bi2.dataLength; j++)
                {
                    ulong mul = (ulong)bi1.data[i] * (ulong)bi2.data[j];
                    ulong carry = mul >> 32;
                    numbers[j + i].Add((uint)(mul & 0xFFFFFFFF));
                    numbers[j + i + 1].Add((uint)carry);
                }
            }
            List<uint> result = new List<uint>(numbers.Length);
            ulong car = 0;
            foreach (var masnum in numbers)
            {
                ulong sum = car;
                foreach (var num in masnum)
                {
                    sum += num;
                }
                car = sum >> 32;
                result.Add((uint)(sum & 0xFFFFFFFF));
            }
            if (car != 0)
                result.Add((uint)car);

            var res = new BigInt(result);
            EqualizeLenght(res);
            return res;
        }

        public static BigInt operator >>(BigInt b, int shiftVal)
        {
            int skipBlock = shiftVal / 32;
            int shiftAmount = shiftVal % 32;
            int lenght = b.dataLength - skipBlock;

            List<uint> tmp = new List<uint>(lenght);

            uint[] newData = new uint[lenght];
            for (int i = 0; i < lenght; i++)
            {
                newData[i] = b.data[i + skipBlock];
            }
            uint carry = 0;
            for (int i = lenght - 1; i >= 0; i--)
            {
                uint cur = newData[i] >> shiftAmount;
                cur |= carry;
                tmp.Add(cur);
                carry = newData[i] << (32 - shiftAmount);
            }
            tmp.Reverse();
            BigInt result = new BigInt(tmp);
            EqualizeLenght(result);
            return result;
        }
        public static BigInt operator ~(BigInt bi1)
        {
            BigInt result = new BigInt(true, bi1);

            for (int i = 0; i < maxLength; i++)
                result.data[i] = (uint)(~(bi1.data[i]));

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            return result;
        }
        public static BigInt operator -(BigInt bi1)
        {
            // handle neg of zero separately since it'll cause an overflow
            // if we proceed.

            if (bi1.dataLength == 1 && bi1.data[0] == 0)
                return (new BigInt(false));

            BigInt result = new BigInt(true, bi1);

            // 1's complement
            for (int i = 0; i < maxLength; i++)
                result.data[i] = (uint)(~(bi1.data[i]));

            // add one to result of 1's complement
            long val, carry = 1;
            int index = 0;

            while (carry != 0 && index < maxLength)
            {
                val = (long)(result.data[index]);
                val++;

                result.data[index] = (uint)(val & 0xFFFFFFFF);
                carry = val >> 32;

                index++;
            }

            if ((bi1.data[maxLength - 1] & 0x80000000) == (result.data[maxLength - 1] & 0x80000000))
                throw (new ArithmeticException("Overflow in negation.\n"));

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;
            return result;
        }
        public static bool operator ==(BigInt bi1, BigInt bi2)
        {
            return bi1.Equals(bi2);
        }
        public static bool operator !=(BigInt bi1, BigInt bi2)
        {
            return !(bi1.Equals(bi2));
        }
        public override bool Equals(object o)
        {
            BigInt bi = (BigInt)o;

            if (this.dataLength != bi.dataLength)
                return false;

            for (int i = 0; i < this.dataLength; i++)
            {
                if (this.data[i] != bi.data[i])
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public static bool operator >(BigInt bi1, BigInt bi2)
        {
            if (bi1.dataLength > bi2.dataLength)
                return true;
            else if (bi2.dataLength > bi1.dataLength)
                return false;

            int pos;

            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
            for (pos = len - 1; pos >= 0 && bi1.data[pos] == bi2.data[pos]; pos--) ;

            if (pos >= 0)
            {
                if (bi1.data[pos] > bi2.data[pos])
                    return true;
                return false;
            }
            return false;
        }
        public static bool operator <(BigInt bi1, BigInt bi2)
        {
            if (bi2.dataLength > bi1.dataLength)
                return true;
            else if (bi1.dataLength > bi2.dataLength)
                return false;

            int pos;

            int len = (bi2.dataLength > bi1.dataLength) ? bi2.dataLength : bi1.dataLength;
            for (pos = len - 1; pos >= 0 && bi1.data[pos] == bi2.data[pos]; pos--) ;

            if (pos >= 0)
            {
                if (bi2.data[pos] > bi1.data[pos])
                    return true;
                return false;
            }
            return false;
        }
        public static bool operator >=(BigInt bi1, BigInt bi2)
        {
            return (bi1 == bi2 || bi1 > bi2);
        }
        public static bool operator <=(BigInt bi1, BigInt bi2)
        {
            return (bi1 == bi2 || bi1 < bi2);
        }
        private int GetBits()
        {
            int shiftBlocks = 0, shiftBits = 0;
            while (data[dataLength - shiftBlocks] == 0)
            {
                shiftBlocks++;
            }
            while ((data[dataLength - shiftBlocks] >> shiftBits) != 0)
            {
                shiftBits++;
            }
            return (dataLength - shiftBlocks) * 32 - (32 - shiftBits);
        }

        public static BigInt operator /(BigInt a, BigInt b)
        {
            if (a < b)
                return new BigInt(false);

            int div = a.dataLength - b.dataLength;
            int shift = 0;
            BigInt plus = new BigInt(1);
            BigInt mul = new BigInt(2);

            BigInt x = a >> (div * 32);
            for (; ; )
            {
                if (x > b)
                    break;
                shift++;
                x = a >> (div * 32 - shift);
            }
            int leftLenght = div * 32 - shift;
            uint min = 0;
            uint max = 2147483650;
            BigInt res = null;
            BigInt bb = null;
            while (min <= max)
            {
                uint mid = (min + max) / 2;
                res = new BigInt(mid);
                bb = x - (b * res);
                if (bb.sign == false)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            if (bb.sign == false)
            {
                res = new BigInt(min);
                bb = x - (b * res);
                if (bb.sign == false)
                {
                    res = new BigInt(max);
                    bb = x - (b * res);
                }
            }
            BigInt r = null;
            for (int i = 0; i < leftLenght; i++)
            {
                x = a >> (leftLenght - i);
                for (; ; )
                {
                    r = x - (b * res);
                    BigInt r2 = res + plus;
                    if (r.sign == true)
                    {
                        r = x - (b * r2);
                        if (r.sign == false)
                        {
                            break;
                        }
                        var r3 = r2 + new BigInt(100);
                        r = x - (b * r3);
                        if (r.sign == true)
                        {
                            var r4 = r2 + new BigInt(1000);
                            r = x - (b * r4);
                            if (r.sign == true)
                            {
                                var r5 = r2 + new BigInt(10000);
                                r = x - (b * r5);
                                if (r.sign == true)
                                {
                                    var r6 = r2 + new BigInt(100000);
                                    r = x - (b * r6);
                                    if (r.sign == true)
                                    {
                                        var r7 = r2 + new BigInt(1000000);
                                        r = x - (b * r7);
                                        if (r.sign == true)
                                        {
                                            var r8 = r2 + new BigInt(10000000);
                                            r = x - (b * r8);
                                            if (r.sign == true)
                                            {
                                                res = r8;
                                                continue;
                                            }
                                            res = r7;
                                            continue;
                                        }
                                        res = r6;
                                        continue;
                                    }
                                    res = r5;
                                    continue;
                                }
                                res = r4;
                                continue;
                            }
                            res = r3;
                            continue;
                        }
                        res = r2;
                    }
                    else
                    {
                        res -= plus;
                    }
                }
                res *= mul;
            }
            EqualizeLenght(res);
            return res;
        }

        //public (BigInt, BigInt) Divide(BigInt a, BigInt b)
        //{
        //}

        //public static BigInt operator %(BigInt bi1, BigInt bi2)
        //{

        //}
    }
}
