using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_WeakElGamal
{
    public class Sender
    {
        public uint p;
        public uint g;
        public uint y;
        public uint k;
        public uint a;
        public uint b;
        public uint message;

        public void Receive(uint g,uint p,uint y)
        {
            this.g = g;
            this.p = p;
            this.y = y;
        }
        public uint GenerateMessage()
        {
            message = Helpers.GenerateRandomNumber(p - 1);
            return message;
        }
        
        public uint GenerateK()
        {
            k = Helpers.GenerateRandomNumber(p - 1);
            while (Helpers.GCD(k, p - 1) != 1)
            {
                k = Helpers.GenerateRandomNumber(p - 1);
            }
            return k;
        }
        public uint GenerateA()
        {
            a = Helpers.PowMod(g, k, p);
            return a;
        }
        public uint GenerateB()
        {
            ulong ms = Helpers.PowMod(y, k, p);
            ulong mz = Helpers.PowMod(message, 1, p);
            ulong mc = (ms * mz) % (ulong)p;
            b = (uint)mc;
            return b;
        }

    }
}
