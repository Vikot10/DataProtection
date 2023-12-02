using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_WeakElGamal
{
    public static class Helpers
    {
        private static Random random = new Random();
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
        public static string ConvertTextToBits(string text)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(text);

            return ConvertArrayByteToBits(byteArray);
        }

        public static string ConvertBitsToText(string textBits)
        {
            StringBuilder textBuilder = new StringBuilder();

            for (int i = 0; i < textBits.Length; i += 8)
            {
                string binary = textBits.Substring(i, 8);
                int decimalValue = Convert.ToInt32(binary, 2);
                char character = Convert.ToChar(decimalValue);
                textBuilder.Append(character);
            }

            return textBuilder.ToString();
        }

        public static string ConvertArrayByteToBits(byte[] byteArray)
        {
            StringBuilder binarySequence = new StringBuilder();
            foreach (byte b in byteArray)
            {
                string binaryString = Convert.ToString(b, 2).PadLeft(8, '0');
                binarySequence.Append(binaryString);
            }

            return binarySequence.ToString();
        }

        public static bool IsPrime(uint number)
        {
            for (int p = 0; p < primesNumbers.Length; p++)
            {
                uint divisor = (uint)primesNumbers[p];

                if (divisor >= number)
                    break;

                uint resultNum = number % divisor;
                if (resultNum == 0)
                    return false;
            }
            return true;
        }

        public static uint GeneratePrime()
        {
            uint t = (uint)random.Next();
            while(!IsPrime(t))
            {
                t = (uint)random.Next();
            }
            return t;
        }
        public static uint GenerateRandomNumber(uint maxValue)
        {
            uint t = (uint)random.Next((int)maxValue);
            return t;
        }
        public static uint GCD(uint a, uint b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
        public static uint PowMod(uint value, uint exponent, uint modulus)
        {
            return (uint)barmodpow(value, exponent, modulus);
        }
        static uint barmodpow(uint B, uint X, uint M)
        {
            uint S;
            uint D, R;

            S = CeilLog2(M);
            R = sprecip(M, S);
            D = 1;
            B %= M;
            if ((X & 1) == 1)
            {
                D = B;
            }

            while ((X >>= 1) != 0)
            {
                B = barmodmul(B, B, M, R, S);
                if ((X & 1) == 1)
                {
                    D = barmodmul(D, B, M, R, S);
                }
            }
            return D;
        }

        static uint barmodmul(uint A, uint B, uint M, uint R, uint S)
        {
            ulong P, T;

            P = A;
            P *= (ulong)B;

            if (P >= M)
            {
                T = P >> (int)S;
                T *= R;
                T >>= ((int)S - 1);
                T *= M;
                P -= T;

                uint Ct = 4;
                while ((P >= M) && (Ct-- != 0))
                {
                    P -= M;
                }
            }
            return (uint)P;
        }

        static uint CeilLog2(uint V)
        {
            uint S = 0;

            while (V > 0)
            {
                S++;
                V >>= 1;
            }
            return S;
        }

        static uint sprecip(uint N, uint S)
        {
            ulong D = 1;
            D <<= (((int)S << 1) - 1);
            D /= N;
            return (uint)D;
        }
    }
}
