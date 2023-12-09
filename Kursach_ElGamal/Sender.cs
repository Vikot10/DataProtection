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
        public BigInt b;
        public BigInt message;

        public void Receive(BigInt g, BigInt p, BigInt y)
        {
            this.g = g;
            this.p = p;
            this.y = y;
        }
        public BigInt GenerateMessage()
        {
            message = new BigInt(false,p);
            return message;
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
        public BigInt GenerateB()
        {
            BigInt ms = BigInt.PowMod(y, k, p);
            BigInt mz = message%p;
            BigInt mc = (ms * mz) % p;
            b = mc;
            return b;
        }

    }
}
