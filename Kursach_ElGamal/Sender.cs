using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach_ElGamal
{
    public class Sender
    {
        public BigInt p;
        public BigInt g;
        public BigInt y;
        public BigInt k;
        public BigInt a;
        public List<BigInt> b;
        public string message;

        public void Receive(BigInt g, BigInt p, BigInt y)
        {
            this.g = g;
            this.p = p;
            this.y = y;
        }
        
        public BigInt GenerateK()
        {
            k = new BigInt(false,p);
            BigInt one = new BigInt(1);
            while (BigInt.GCD(k, p ) != one)
            {
                k = new BigInt(false, p);
            }
            return k;
        }
        public BigInt GenerateA()
        {
            a = BigInt.PowMod(g, k, p);
            return a;
        }
        public List<BigInt> GenerateB()
        {
            b = new List<BigInt>();
            int bitsL = (BigInt.textLenght * 4);
            int takeL = bitsL;
            int messageLenght = (int)Math.Ceiling((double)message.Length / bitsL);
            for (int i = 0;i< messageLenght; i++)
            {
                string t;
                if (i == messageLenght - 1)
                {
                    t = message.Substring(i * bitsL);
                }
                else
                {
                    t = message.Substring(i * bitsL, bitsL);
                }
                List<byte> by = new List<byte>();
                foreach(var ch in t)
                {
                    by.Add((byte)ch);
                }
                if (by.Count % 4 != 0)
                {
                    var len = 4 - by.Count % 4;
                    for (int cvb = 0; cvb < len; cvb++)
                    {
                        by.Add(0);
                    }
                }
                BigInt tmp = new BigInt(by.ToArray());
                b.Add((BigInt.PowMod(y, k, p) * tmp) % p);
            }
            return b;
        }

    }
}
