using System.Diagnostics;
using System.Security.Cryptography;

namespace Kursach_ElGamal
{
    public class BigInt
    {
        private uint[] data = null;
        public bool sign = true;
        public int dataLength;
        private const int maxLength = 16;
        public const int textLenght = 8;
        Random rand = new Random();
        public BigInt(bool isPrime)
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
        public BigInt(bool isClone, BigInt value) 
        {
            dataLength = value.dataLength;
            data = new uint[dataLength];
            if (isClone)
            {
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
        public BigInt(byte[] bytes)
        {
            dataLength = bytes.Length/4;
            data = new uint[dataLength+1];

            for (int i = 0;i<dataLength;i++)
                data[i] = BitConverter.ToUInt32(bytes, i * 4);
            if (bytes.Length % 4 > 0)
            {
                data[dataLength] = BitConverter.ToUInt32(bytes, dataLength * 4);
                dataLength++;
            }

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
        private int GetBits()
        {
            int shiftBlocks = 0, shiftBits = 0;
            while ((data[dataLength - 1] >> shiftBits) != 1)
            {
                shiftBits++;
            }
            return (dataLength - shiftBlocks) * 32 - (32 - shiftBits);
        }
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
            var _bits = data;
            int digits = -1;
            char fmt = 'R';
            bool decimalFmt = (fmt == 'g' || fmt == 'G' || fmt == 'd' || fmt == 'D' || fmt == 'r' || fmt == 'R');

            const uint kuBase = 1000000000;
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
            if (cuDst == 0 && rgch[0] == '\0')
                return "0";
            for (uint uDig = rguDst[cuDst - 1]; uDig != 0;)
            {
                rgch[--ichDst] = (char)('0' + uDig % 10);
                uDig /= 10;
            }
            int numDigitsPrinted = cchMax - ichDst;
            while (digits > 0 && digits > numDigitsPrinted)
            {
                rgch[--ichDst] = '0';
                digits--;
            }
            return new string(rgch, ichDst, cchMax - ichDst);
        }
        public void GenRandomArrayUint(int length = maxLength, uint maxValue = 0)
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
        public uint LastValue()
        {
            return data[0];
        }
        public bool IsPrime()
        {
            return MillerRabinTest(this,100);
        }
        public bool MillerRabinTest(BigInt n, int k)
        {
            BigInt one = new BigInt(1);
            BigInt two = new BigInt(2);
            BigInt three = new BigInt(3);
            BigInt zero = new BigInt(0);

            if (n == two || n == three)
                return true;

            if (n < two || n % two == zero)
                return false;

            BigInt t = n - one;
            int s = 0;

            while (t % two == zero)
            {
                t /= two;
                s += 1;
            }

            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] _a = new byte[n.ToByteArray().LongLength];
                BigInt a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInt(_a);
                }
                while (a < two || a >= n - two);

                BigInt x = BigInt.PowMod(a, t, n);
                if (x == one || x == n - one)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInt.PowMod(x, two, n);
                    if (x == one)
                        return false;

                    if (x == n - one)
                        break;
                }
                if (x != n - one)
                    return false;
            }
            return true;
        }
        public static BigInt GCD(BigInt a, BigInt b)
        {
            BigInt zero = new BigInt(0);
            while (b != zero)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
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
            if (carry != 0)
            {
                numbers.Add((uint)carry);
            }
            var res = new BigInt(numbers);
            EqualizeLenght(res);
            return res;
        }
        public static BigInt operator -(BigInt bi1, BigInt bi2)
        {
            bool sign = true;
            List<uint> result = new List<uint>();
            uint carry = 0;
            ulong mu = 1;
            mu <<= 32;
            if (bi1 >= bi2)
            {
                int diffLenght = bi1.dataLength - bi2.dataLength;
                for (int i = 0; i < bi2.dataLength; i++)
                {
                    long tmp = 0;
                    tmp = (long)bi1.data[i] - (long)bi2.data[i] - (long)carry;
                    if (tmp >= 0)
                    {
                        result.Add((uint)tmp);
                        carry = 0;
                    }
                    else
                    {
                        ulong dt = mu | bi1.data[i];
                        dt -= (bi2.data[i] + carry);
                        result.Add((uint)dt);
                        carry = 1;
                    }
                }
                if (diffLenght > 0)
                {
                    result.Add(bi1.data[bi2.dataLength] -= carry);
                    for (int i = bi2.dataLength + 1; i < bi1.dataLength; i++)
                    {
                        result.Add(bi1.data[i]);
                    }
                }
            }
            else
            {
                sign = false;
                int diffLenght = bi2.dataLength - bi1.dataLength;
                for (int i = 0; i < bi1.dataLength; i++)
                {
                    long tmp = 0;
                    tmp = bi2.data[i] - bi1.data[i] - carry;
                    if (tmp > 0)
                    {
                        result.Add((uint)tmp);
                        carry = 0;
                    }
                    else
                    {
                        ulong dt = mu | bi2.data[i];
                        dt -= (bi1.data[i] + carry);
                        result.Add((uint)dt);
                        carry = 1;
                    }
                }
                if (diffLenght > 0)
                {
                    result.Add(bi2.data[bi1.dataLength] -= carry);
                    for (int i = bi1.dataLength + 1; i < bi2.dataLength; i++)
                    {
                        result.Add(bi2.data[i]);
                    }
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
                if (shiftAmount > 0)
                    carry = newData[i] << (32 - shiftAmount);
            }
            tmp.Reverse();
            BigInt result = new BigInt(tmp);
            EqualizeLenght(result);
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
        public static BigInt operator /(BigInt a, BigInt b)
        {
            if (a < b)
                return new BigInt(false);
            if (a == b)
                return new BigInt(1);

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
            BigInt resPlus;
            for (int i = 0; i < leftLenght; i++)
            {
                x = a >> (leftLenght - i);
                int index = 0;
                for (; ; )
                {
                    resPlus = res + plus;
                    r = x - (b * resPlus);
                    if (r.sign == false)
                    {
                        break;
                    }
                    res = resPlus;                   
                }
                res *= mul;
            }
            resPlus = res + plus;
            r = a - (b * resPlus);
            if (r.sign == true)
            {
                res = resPlus;
            }
            EqualizeLenght(res);
            return res;
        }
        public static BigInt operator %(BigInt a, BigInt b)
        {
            return a - b * (a / b);
        }
        public BigInt gcd(BigInt b)
        {
            BigInt x = this;
            BigInt y = b;

            BigInt g = y;

            while (x.dataLength > 1 || (x.dataLength == 1 && x.data[0] != 0))
            {
                g = x;
                x = y % x;
                y = g;
            }

            return g;
        }
        public static BigInt PowMod(BigInt value, BigInt exponent, BigInt modulus)
        {
            return barmodpow(value, exponent, modulus);
        }
        public static BigInt barmodpow(BigInt B, BigInt X, BigInt M)
        {
            int S;
            BigInt D, R;
            BigInt zero = new BigInt(0);

            S = M.GetBits();
            R = sprecip(M, S);
            D = new BigInt(1);
            B %= M;
            if ((X.LastValue() & 1) == 1)
            {
                D = new BigInt(true, B);
            }

            while ((X >>= 1) != zero)
            {
                B = barmodmul(B, B, M, R, S);
                if ((X.LastValue() & 1) == 1)
                {
                    D = barmodmul(D, B, M, R, S);
                }
            }
            return D;
        }
        static BigInt sprecip(BigInt N, int S)
        {
            BigInt D = new BigInt(1);
            for (int i = 0; i < (S << 1) - 1; i++)
            {
                D *= new BigInt(2);
            }
            D /= N;
            return D;
        }
        static BigInt barmodmul(BigInt A, BigInt B, BigInt M, BigInt R, int S)
        {
            BigInt T, P;

            P = new BigInt(true, A);
            P *= B;

            if (P >= M)
            {
                T = P >> S;
                T *= R;
                T >>= (S - 1);
                T *= M;
                P -= T;

                //uint Ct = 4;
                while ((P >= M))
                {
                    P -= M;
                }
            }
            return P;
        }
    }
}
