
using System.Drawing;

namespace Kursach_ElGamal
{
    public class BigInt
    {
        private uint[] data = null;
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
        public BigInt(bool isClone,BigInt value) // конструктор копирования и для чисел с максимальным значением
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
                GenRandomArrayUint(value.dataLength-1, value.data[dataLength - 1]);
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

            for (int j = 0; j < dataLength; j--)
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
            BigInt thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)        // negative
                thisVal = -this;
            else
                thisVal = this;

            // test for divisibility by primes < 2000
            for (int p = 0; p < primesNumbers.Length; p++)
            {
                BigInt divisor = new BigInt((ulong)primesNumbers[p]);

                if (divisor >= thisVal)
                    break;

                BigInt resultNum = thisVal % divisor;
                if (resultNum.IntValue() == 0)
                    return false;
            }
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

            return new BigInt(numbers);
        }
        public static BigInt operator -(BigInt bi1, BigInt bi2)
        {
            BigInt result = new BigInt(false)
            {
                dataLength = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength
            };

            long carryIn = 0;
            for (int i = 0; i < result.dataLength; i++)
            {
                long diff;

                diff = (long)bi1.data[i] - (long)bi2.data[i] - carryIn;
                result.data[i] = (uint)(diff & 0xFFFFFFFF);

                if (diff < 0)
                    carryIn = 1;
                else
                    carryIn = 0;
            }

            if (carryIn != 0)
            {
                for (int i = result.dataLength; i < maxLength; i++)
                    result.data[i] = 0xFFFFFFFF;
                result.dataLength = maxLength;
            }

            EqualizeLenght(result);
            return result;
        }
        public static BigInt operator *(BigInt bi1, BigInt bi2)
        {
            int lenght = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            List<List<uint>> numbers = new List<List<uint>>(lenght);
            numbers.Add(new List<uint>());

            for (int i = 0; i < bi1.dataLength; i++)
            {
                numbers.Add(new List<uint>());
                for (int j = 0; j < bi2.dataLength; j++)
                {
                    ulong mul = (ulong)bi1.data[i] * (ulong)bi2.data[j];
                    ulong carry = mul >> 32;
                    numbers[i].Add((uint)(mul & 0xFFFFFFFF));
                    numbers[i+1].Add((uint)carry);
                }
            }
            List<uint> result = new List<uint>(numbers.Count);
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

            return new BigInt(result);
        }
        
        public static BigInt operator >>(BigInt b, int shiftVal)
        {
            int skipBlock = shiftVal / 32;
            int shiftAmount = shiftVal % 32;
            int lenght = b.dataLength - skipBlock;

            List<uint> tmp = new List<uint>(lenght);
            uint carry = 0;
            for (int i=0;i< lenght; i++)
            {
                uint cur = b.data[b.dataLength - i] >> shiftAmount;
                cur |= carry;
                tmp.Add(cur);
                carry = cur << (32 - shiftAmount);
            }
            tmp.Reverse();
            BigInt result = new BigInt(tmp);

            return result;
        }       
        public static BigInt operator ~(BigInt bi1)
        {
            BigInt result = new BigInt(true,bi1);

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

            BigInt result = new BigInt(true,bi1);

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

        public static BigInt operator /(BigInt bi1, BigInt bi2)
        {
            
        }
        public static BigInt operator %(BigInt bi1, BigInt bi2)
        {
            
        }
    }
}
