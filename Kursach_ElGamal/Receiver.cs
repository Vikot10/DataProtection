using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_ElGamal
{
    public class Receiver
    {
        public BigInt p;
        public BigInt g;
        public BigInt x;
        public BigInt y;
        public BigInt a;
        public BigInt b;
        public BigInt message;
        public BigInt decMessage;

        public BigInt GenerateP()
        {
            p = new BigInt(true);
            return p;
        }

        public BigInt GenerateG()
        {
            g = new BigInt(false,p);
            return g;
        }

        public BigInt GenerateX()
        {
            x = new BigInt(false, p);
            return x;
        }

        public BigInt GenerateY()
        {
            y = BigInt.PowMod(g, x, p);
            return y;
        }
        public void Receive(BigInt a, BigInt b)
        {
            this.a = a; 
            this.b = b;
        }
        public BigInt Decode()
        {
            decMessage = (b * BigInt.PowMod(a, p - new BigInt(1) - x, p)) % p;
            return decMessage;
        }
    }
}
