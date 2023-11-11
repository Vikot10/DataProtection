
using System.Text;

var t = new Task4();
t.Encode();

public class Task4
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";

    CongruentGenerator congruentGenerator = new CongruentGenerator(40693, 16);
    LFSR lfsr = new LFSR(17);
    Dictionary<char,string> alfAndCode = new Dictionary<char,string>();

    public Task4()
    {
        for (char i = 'А'; i <= 'я'; i++)
        {
            alfAndCode.Add(i,Helpers.IntToBinaryString(((byte)i),8));
        }
    }

    //public void Rewrite()
    //{
    //    var text = File.ReadAllText(_originalPath);
    //    var result = Caesar.ProofAndRewrite(text); 
    //    File.WriteAllText(_originalPath, result);
    //}

    public void Encode()
    {
        var text = File.ReadAllText(_originalPath);

        Console.WriteLine("Текст:\n" + text);

        int firstStage = congruentGenerator.Generate(12345);
        string gamma = lfsr.GetGamma(firstStage, 128);

        Console.WriteLine("Гамма:\n" + gamma);

        var binaryText = textToBinaryString(text);

        Console.WriteLine("Текст в бинарном представлении:\n" + binaryText);

        for (; gamma.Length < binaryText.Length;)
        {
            gamma += gamma;
        }

        Console.WriteLine("Полная гамма в бинарном представлении:\n" + gamma);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < binaryText.Length; i++)
        {
            sb.Append(Helpers.XorOne(binaryText[i], gamma[i]));
        }
        var encodeBinary = sb.ToString();

        Console.WriteLine("Текст после гаммирования в бинарном представлении:\n" + encodeBinary);

        var encodeText = binaryToText(encodeBinary);

        Console.WriteLine("Текст после гаммирования в буквенном представлении:\n" + encodeText);

        File.WriteAllText(_encodedPath, encodeText);
    }

    public string textToBinaryString(string text)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            sb.Append(alfAndCode[c]);
        }
        return sb.ToString();
    }
    public string binaryToText(string binaryText)
    {
        var sb = new StringBuilder();
        for(int i = 0;i<binaryText.Length;i+=8)
        {
            sb.Append(Helpers.BinaryToChar(binaryText.Substring(i, 8), 8));
        }
        return sb.ToString();
    }
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
            number *= 2;
        }
        return number;
    }
    public static string IntToBinaryString(int number, int rate)
    {
        string s = "";
        for (int i = 0; i<rate;i++)
        {
             s += (number >> i & 1).ToString();
        }
        return s;
    }
    public static char BinaryToChar(string binary,int rate)
    {
        int res = 0;
        for (int i=0;i<rate;i++)
        {
            res += Pow(Int32.Parse(binary[i].ToString()),i);
        }
        return (char)res;
    }
    public static char XorOne(char ch1,char ch2)
    {
        return ch1 == ch2 ? '0' : '1';
    }
    public static int XorAllElelemnt(string s)
    {
        int result = 0;
        foreach (var ch in s)
        {
            result ^= Int16.Parse(ch.ToString());
        }
        return result;
    }
    public static int XorAllElelemnt(UInt32 number)
    {
        return System.Numerics.BitOperations.PopCount(number) & 1;
    }
}

public class CongruentGenerator
{
    readonly int a ;
    readonly int b ;
    readonly int m ;
    readonly int rate ;

    public CongruentGenerator(int a, int rate)
    {
        this.a = a;
        this.rate = rate;
        this.m = Helpers.Pow(1,rate);
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
    readonly int rate;
    public LFSR(int rate)
    {
        this.m = Helpers.Pow(1,rate);
        this.rate = rate;
    }

    public string GetGamma(int start, int lenght)
    {
        string s = "";
        int el = start;
        for (int i = 0; i < (int)Math.Ceiling((double)lenght/rate); i++)
        {
            el = Generate(el);
            s += Helpers.IntToBinaryString(el, rate);
        }
        return s[..128];
    }

    public int Generate(int start)                      
    {
        start = (start << 1) % m;
        int xorTmp = Helpers.XorAllElelemnt((uint)start);
        start = xorTmp ^ start;

        return start;
    }

    public int Generate2(int start)
    {
        //string bin = Helpers.IntToBinaryString(start, 16);

        //Console.WriteLine(Convert.ToString(start, toBase: 2));
        //start <<= 1;
        //Console.WriteLine(Convert.ToString(start, toBase: 2));

        //var n = System.Numerics.BitOperations.(start);

        //Console.WriteLine(Convert.ToString(n, toBase: 2));
        return 1;
    }
}
