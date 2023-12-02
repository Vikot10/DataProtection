using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_WeakElGamal
{
    public class Receiver
    {
        public uint p;
        public uint g;
        public uint x;
        public uint y;
        public uint a;
        public uint b;
        public uint message;
        public uint decMessage;

        public uint GenerateP()
        {
            p = Helpers.GeneratePrime();
            return p;
        }

        public uint GenerateG()
        {
            g = Helpers.GenerateRandomNumber(p);
            return g;
        }

        public uint GenerateX()
        {
            x = Helpers.GenerateRandomNumber(p - 1);
            return x;
        }

        public uint GenerateY()
        {
            y = Helpers.PowMod(g, x, p);
            return y;
        }
        public void Receive(uint a, uint b)
        {
            this.a = a; 
            this.b = b;
        }
        public uint Decode()
        {
            ulong ms = Helpers.PowMod(a, p - 1 - x, p);
            ulong mz = Helpers.PowMod(b, 1, p);
            ulong mc = (ms * mz) % (ulong)p;
            decMessage = (uint)mc;
            return decMessage;
        }
    }
}
