
public class Task3
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";
    CongruentGenerator congruentGenerator = new CongruentGenerator(40693, Helpers.Pow(2,16));
    public void Rewrite()
    {
        var text = File.ReadAllText(_originalPath);
        var result = Caesar.ProofAndRewrite(text); 
        File.WriteAllText(_originalPath, result);
    }

    public void Encode()
    {
        var text = File.ReadAllText(_originalPath);
        var k = Int32.Parse(Console.ReadLine()!);

        var result = Caesar.Encode(text, k);
        File.WriteAllText(_encodedPath, result);
    }

    public void Find()
    {
        var text = File.ReadAllText(_encodedPath);

        (var result, var key) = Caesar.FindBestKey(text);
        Console.WriteLine($"Лучший ключ = {key}");
        File.WriteAllText(_decodedPath, result);
    }
}

public static class Helpers
{
    static int GCD(int a, int b)
    {
        if (a == 0)
        {
            return b;
        }
        else
        {
            while (b != 0)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;
        }
    }

    public static int GetMaxB(int m)
    {
        int b = m - 1;
        while (GCD(b, m) != 1)
        {
            b--;
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
