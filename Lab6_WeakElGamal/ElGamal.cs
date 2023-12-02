//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lab6_WeakElGamal
//{
//    public class ElGamal
//    {
//        public long p;
//        public long g;
//        public long x;
//        public long y;
//        public long k;
//        public long a;
//        public long b;
//        public long message;
//        public long decryptedMessage;

//        private static Random random = new Random();

//        public long GenerateP()
//        {
//            p = Helpers.GeneratePrime();
//            return p;
//        }

//        public long GenerateG()
//        {
//            g = Helpers.GenerateRandomNumber(p);
//            return g;
//        }

//        public string GenerateX()
//        {
//            x = GenerateRandomNumber(p - 1);
//            return x.ToString();
//        }

//        // ------------------------------
//        // Генерируем y
//        // ------------------------------
//        public string GenerateY()
//        {
//            y = BigInteger.ModPow(g, x, p);

//            return y.ToString();
//        }

//        public string GenerateMessage()
//        {
//            message = GenerateRandomNumber(p - 1);
//            return message.ToString();
//        }

//        // ------------------------------
//        // Генерируем k
//        // ------------------------------
//        public string GenerateK()
//        {
//            k = GenerateRandomNumber(p - 2);

//            return k.ToString();
//        }

//        // ------------------------------
//        // Генерируем a
//        // ------------------------------
//        public string GenerateA()
//        {
//            a = BigInteger.ModPow(g, k, p);
//            return a.ToString();
//        }

//        // ------------------------------
//        // Генерируем b
//        // ------------------------------
//        public string GenerateB(ref BigInteger message)
//        {
//            b = (BigInteger.ModPow(y, k, p) * message) % p;
//            return b.ToString();
//        }

//        // ------------------------------
//        // Расшифровка
//        // ------------------------------
//        public string Decrypt()
//        {
//            decryptedMessage = (b * BigInteger.ModPow(a, p - 1 - x, p)) % p;
//            return decryptedMessage.ToString();

//        }
//    }
//}
