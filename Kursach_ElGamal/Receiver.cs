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
        public List<BigInt> b;
        public BigInt message;
        public string decMessage;

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
        public void Receive(BigInt a, List<BigInt> b)
        {
            this.a = a; 
            this.b = b;
        }
        public string Decode()
        {
            foreach(var c in b)
            {
                var dec = (c * BigInt.PowMod(a, p - new BigInt(1) - x, p)) % p;
                var decBytes = dec.ToByteArray();
                foreach(var bb in decBytes)
                {
                    decMessage += (char)bb;
                }                
            }
            return decMessage;
        }
    }
}
