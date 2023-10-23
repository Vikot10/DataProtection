using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;

var t = new Task3();
//t.Rewrite();
t.Encode();
t.Find();

public class Task3
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";
    Caesar Caesar = new Caesar();

    public void Rewrite()
    {
        var text = File.ReadAllText(_originalPath);
        var result = Caesar.ProofAndRewrite(text);
        File.WriteAllText(_originalPath, result);
    }

    public void Encode()
    {
        var text = File.ReadAllText(_originalPath);
        Console.WriteLine("Введите ключ");
        var k = Int32.Parse(Console.ReadLine()!);

        var result = Caesar.Encode(text, k);
        File.WriteAllText(_encodedPath, result);
    }

    public void Find()
    {
        var text = File.ReadAllText(_encodedPath);

        (var result,var key) = Caesar.FindBestKey(text);
        Console.WriteLine($"Лучший ключ = {key}");
        File.WriteAllText(_decodedPath, result);
    }
}

public class Caesar
{
    string alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя .,-!?";
    Dictionary<char, int> LetterAndCount = new Dictionary<char, int>();
    Dictionary<char, double> LetterAndStatistic = new Dictionary<char, double>()
    {
        {' ', 0.175},
        {'О', 0.090},
        {'Е', 0.072},
        {'Ё', 0.072},
        {'А', 0.062},
        {'И', 0.062},
        {'Н', 0.053},
        {'Т', 0.053},
        {'С', 0.045},
        {'Р', 0.040},
        {'В', 0.038},
        {'Л', 0.035},
        {'К', 0.028},
        {'М', 0.026},
        {'Д', 0.025},
        {'П', 0.023},
        {'У', 0.021},
        {'Я', 0.018},
        {'Ы', 0.016},
        {'З', 0.016},
        {'Ь', 0.014},
        {'Б', 0.014},
        {'Г', 0.013},
        {'Ъ', 0.014},
        {'Ч', 0.012},
        {'Й', 0.010},
        {'Х', 0.009},
        {'Ж', 0.007},
        {'Ю', 0.006},
        {'Ш', 0.006},
        {'Ц', 0.004},
        {'Щ', 0.003},
        {'Э', 0.003},
        {'Ф', 0.002},
    };

    public Caesar()
    {
        foreach (var letter in LetterAndStatistic)
        {
            LetterAndCount.Add(letter.Key, 0);
        }
    }

    public string ProofAndRewrite(string text)
    {
        var result = new StringBuilder();
        foreach (var ch in text)
        {
            if (alf.Contains(Char.ToLower(ch)))
                result.Append(ch);
        }
        return result.ToString();
    }

    public string Encode(string text, int k)
    {
        var decAlf = new Dictionary<char, char>();
        for (var i = 0; i < alf.Length; i++)
        {
            decAlf.Add(alf[i], alf[(i + k) % alf.Length]);  
        }

        var result = new StringBuilder();
        foreach (var ch in text)
        {
            if (Char.IsUpper(ch))
                result.Append(Char.ToUpper(decAlf[Char.ToLower(ch)]));
            else result.Append(decAlf[Char.ToLower(ch)]);
        }
        return result.ToString();
    }

    public Dictionary<char, int> GetCountDistrubutions(string text, int k)
    {
        var decAlf = new Dictionary<char, char>();
        var letterAndCount = new Dictionary<char, int>(LetterAndCount);
        for (var i = 0; i < alf.Length; i++)
        {
            decAlf.Add(alf[(alf.Length + i - k) % alf.Length], alf[i] );
        }

        var decodedText = Decode(text, k);

        foreach (var ch in decodedText)
        {
            var chU = Char.ToUpper(ch);
            if (letterAndCount.ContainsKey(chU))
                letterAndCount[chU]++;
        }
        return letterAndCount;
    }

    public string Decode(string text, int k)
    {
        var decAlf = new Dictionary<char, char>();
        for (var i = 0; i < alf.Length; i++)
        {
            decAlf.Add(alf[(i + k) % alf.Length], alf[i]);
        }

        var result = new StringBuilder();
        foreach (var ch in text)
        {
            if(Char.IsUpper(ch))
                result.Append(Char.ToUpper(decAlf[Char.ToLower(ch)]));
            else result.Append(decAlf[Char.ToLower(ch)]);
        }
        return result.ToString();
    }

    public (string,int) FindBestKey(string text)
    {
        Dictionary<char, int> letterAndCount = new Dictionary<char, int>();
        List<(double,int)> degrees = new List<(double,int)>();
        int count = text.Where(x => alf.Contains(Char.ToLower(x))).Count();
        foreach (var letter in LetterAndStatistic)
        {
            letterAndCount.Add(letter.Key, 0);
        }
        for (var i = 0;i < alf.Length;i++)
        {
            var tmpLetterAndCount = GetCountDistrubutions(text, i);
            var w = DegreeOfStatisticalDiscrepancy(tmpLetterAndCount, count);
            degrees.Add((w,i));
        }
        foreach (var degree in degrees.OrderBy(x=>x.Item1))
        {
            Console.WriteLine($"Для ключа {degree.Item2}  W = {degree.Item1}");
        }
        double minW = degrees.Min(x=>x.Item1);
        int key = degrees.First(x=> x.Item1 == minW).Item2;
        string decodeText = Decode(text, key);
        return (decodeText, key);
    }

    public double DegreeOfStatisticalDiscrepancy(Dictionary<char, int> letterAndCount, int count)
    {
        double W=0;
        foreach(var letter in letterAndCount)
        {
            W += Math.Pow(((double)letter.Value / count - LetterAndStatistic[letter.Key]), 2);
        }
        return W;
    }
}



