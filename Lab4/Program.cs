
using System.Security.Cryptography;
using System.Xml.XPath;

var t = new Task3();
LFSR lfsr = new LFSR(UInt16.MaxValue);
lfsr.Generate(UInt16.MaxValue);

public class Task3
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";
    CongruentGenerator congruentGenerator = new CongruentGenerator(40693, UInt16.MaxValue);
    LFSR LFSR = new LFSR(UInt16.MaxValue);

    //public void Rewrite()
    //{
    //    var text = File.ReadAllText(_originalPath);
    //    var result = Caesar.ProofAndRewrite(text); 
    //    File.WriteAllText(_originalPath, result);
    //}

    //public void Encode()
    //{
    //    var text = File.ReadAllText(_originalPath);
    //    var k = Int32.Parse(Console.ReadLine()!);

    //    var result = Caesar.Encode(text, k);
    //    File.WriteAllText(_encodedPath, result);
    //}

    //public void Find()
    //{
    //    var text = File.ReadAllText(_encodedPath);

    //    (var result, var key) = Caesar.FindBestKey(text);
    //    Console.WriteLine($"Лучший ключ = {key}");
    //    File.WriteAllText(_decodedPath, result);
    //}
}

public static class Helpers
{
    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    public static int GetMaxB(int m)
    {
        int b = m / 2 - 1;
        while (GCD(b, m) != 1)
        {
            b++;
        }
        return b;
    }

    public static int Pow(int number,int degree)
    {
        for (int i = 0;i<degree;i++)
        {
            number *= number;
        }
        return number;
    }

    public static string IntToBinaryString(int number, int rate)
    {
        string s = "";
        for (int i = 0; i<rate;i++)
        {
             s += ((byte)(number >> i & 1)).ToString();
        }
        return s;
    }

    public static int XOR(string s)
    {
        int result = 0;
        foreach (var ch in s)
        {
            result ^= Int16.Parse(ch.ToString());
        }
        return result;
    }
    public static int XOR(UInt32 number)
    {
        return System.Numerics.BitOperations.PopCount(number) & 1;
    }
}

public class CongruentGenerator
{
    readonly int a ;
    readonly int b ;
    readonly int m ;

    public CongruentGenerator(int a, int m)
    {
        this.a = a;
        this.m = m;
        this.b = Helpers.GetMaxB(m);
    }

    public int Generate(int start)
    {
        return (a * start + b) % m;
    }
}

public class LFSR
{
    readonly int m;
    public LFSR(int m)
    {
        this.m = m;
    }

    public int Generate(int start)                      
    {
        start = (start << 1) % m;
        int xorTmp = Helpers.XOR(((uint)start));
        start = xorTmp ^ start;


        //string bin = Helpers.IntToBinaryString(start, 16);

        //Console.WriteLine(Convert.ToString(start, toBase: 2));
        //start <<= 1;
        //Console.WriteLine(Convert.ToString(start, toBase: 2));

        //var n = System.Numerics.BitOperations.(start);

        //Console.WriteLine(Convert.ToString(n, toBase: 2));

        return start;
    }

    public int Generate2(int start)
    {
        return 1;
    }
}
