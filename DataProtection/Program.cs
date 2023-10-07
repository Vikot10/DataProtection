
var t = new Task1();
//t.firstStage();
t.secondStage();

public class Task1
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _wordKey = Environment.CurrentDirectory + "/wordKey.txt";
    readonly string _caesarEncodedPath = Environment.CurrentDirectory + "/caesarEncoded.txt";
    readonly string _caesarDecodedPath = Environment.CurrentDirectory + "/caesarDecoded.txt";
    readonly string _tablesEncodedPath = Environment.CurrentDirectory + "/tablesEncoded.txt";
    readonly string _tablesDecodedPath = Environment.CurrentDirectory + "/tablesDecoded.txt";
    string text;
    Caesar caesar = new Caesar();
    Tables tables = new Tables();

    public Task1()
    {
        text = File.ReadAllText(_originalPath);
    }

    public void firstStage()
    {
        Console.WriteLine("Ввведите два ключа A и K");
        var keys = Console.ReadLine()!.Trim().Split(" ");
        var A = Int32.Parse(keys.First());
        var K = Int32.Parse(keys.Last());
        File.WriteAllText(_caesarEncodedPath, caesar.AthenianCaesarCipher(text, A, K));
        var decodedText = File.ReadAllText(_caesarEncodedPath);
        File.WriteAllText(_caesarDecodedPath, caesar.DecodeAthenianCaesarCipher(decodedText, A, K));
    }

    public void secondStage()
    {
        var keyAndRows = File.ReadAllText(_wordKey).Split(" ");
        var key = keyAndRows.First();
        var rows = int.Parse(keyAndRows.Last());
        File.WriteAllText(_tablesEncodedPath, Tables.Encode2(text, key, rows));
        var decodedText = File.ReadAllText(_tablesEncodedPath);
        File.WriteAllText(_tablesDecodedPath, Tables.Decode2(decodedText, key, rows));
    }
}

public class Caesar
{
    string alf = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ,.0123456789";
    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
    void Check(string text, int A, int K)
    {
        foreach (var ch in text)
        {
            if (!alf.Contains(ch))
                throw new Exception($"символа {ch} нет в алфавите");
            if (A < 0 || A > alf.Length - 1)
                throw new Exception("0 <= A <= (M-1)");
            if (K < 0 || K > alf.Length - 1)
                throw new Exception("0 <= K <= (M-1)");
            if (GCD(A, alf.Length) != 1)
                throw new Exception("НОД(A,M) должен быть равен 1");
        }
    }

    public string AthenianCaesarCipher(string text, int A, int K)
    {
        Check(text, A, K);
        var decAlf = new Dictionary<char, char>();
        for (var i = 0; i < alf.Length; i++)
        {
            decAlf.Add(alf[i], alf[(i * A + K) % alf.Length]);
        }
        var result = "";
        foreach (var ch in text)
        {
            result += decAlf[ch];
        }
        return result;
    }

    public string DecodeAthenianCaesarCipher(string text, int A, int K)
    {
        var decAlf = new Dictionary<char, char>();
        for (var i = 0; i < alf.Length; i++)
        {
            decAlf.Add(alf[(i * A + K) % alf.Length], alf[i]);
        }
        var result = text.Aggregate("", (current, ch) => current + decAlf[ch]);
        return result;
    }
}

public class Tables
{
    public static string Encode2(string text, string key, int rows)
    {
        Console.WriteLine($"Ключ {key}");
        var columns = key.Length;
        var count = (int)Math.Ceiling((double)text.Length / (columns * rows));
        var bloks = new char[count, rows, columns];
        for (var i = 0; i < text.Length; i++)
        {
            bloks[i / (columns * rows), i % rows, i / rows % columns] = text[i];
        }
        Console.WriteLine("Блоки до перестановки");
        Print(bloks, rows, columns, count, key);

        var head = new List<KeyValuePair<char, int>>();
        head.AddRange(key.Select((val, index) => new KeyValuePair<char, int>(val, index)));
        head = head.OrderBy(kv => kv.Key).ToList();

        var encodeBloks = new char[count, rows, columns];
        for (var i = 0; i < count; i++)
        {
            for (var k = 0; k < rows; k++)
            {
                for (var j = 0; j < head.Count; j++)
                {
                    encodeBloks[i, k, j] = bloks[i, k, head[j].Value];
                }
            }
        }
        Console.WriteLine("Блоки после перестановки");
        Print(encodeBloks, rows, columns, count, key);

        var result = "";
        for (var i = 0; i < count; i++)
        {
            for (var k = 0; k < rows; k++)
            {
                for (var j = 0; j < columns; j++)
                {
                    result += encodeBloks[i, k, j];
                }
            }
        }
        return result;
    }
    public static string Encode(string text, string key, int rows)
    {


        var tab = 1;
        var count = (int)Math.Ceiling((float)text.Length / key.Length / rows);
        var tables = new char[count, key.Length, rows];
        for (var i = 0; i < text.Length; i++)
        {
            tables[i / (key.Length * rows), i % key.Length, (i / key.Length) % rows] = text[i];
        }
        Print(tables, rows, key.Length, count, key);
        File.WriteAllText(Environment.CurrentDirectory + "/et.txt", tables.ToString());
        var head = new List<KeyValuePair<char, int>>();
        head.AddRange(key.Select((val, index) => new KeyValuePair<char, int>(val, index)));
        head = head.OrderBy(kv => kv.Key).ToList();
        var result = "";
        for (var i = 0; i < count; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                foreach (var kv in head)
                {
                    result += tables[i, kv.Value, j];
                }
                result += "\n";
            }
            result += "\n";
        }
        return result;
    }

    public static string Decode2(string text, string key, int rows)
    {
        var columns = key.Length;
        var count = (int)Math.Ceiling((double)text.Length / (columns * rows));
        var bloks = new char[count, rows, columns];

        for (var i = 0; i < text.Length; i++)
        {
            bloks[i / (columns * rows), i / columns % rows, i % columns] = text[i];
        }
        Console.WriteLine("Закодированные блоки до перестановки");
        Print(bloks, rows, columns, count, key);

        var head = new List<KeyValuePair<char, int>>();
        head.AddRange(key.Select((val, index) => new KeyValuePair<char, int>(val, index)));
        head = head.OrderBy(kv => kv.Key).ToList();

        var decodeBloks = new char[count, rows, columns];
        for (var i = 0; i < count; i++)
        {
            for (var k = 0; k < rows; k++)
            {
                for (var j = 0; j < head.Count; j++)
                {
                    decodeBloks[i, k, head[j].Value] = bloks[i, k, j];
                }
            }
        }
        Console.WriteLine("Закодированные блоки после перестановки");
        Print(decodeBloks, rows, columns, count, key);

        var result = "";
        for (var i = 0; i < count; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                for (var k = 0; k < rows; k++)
                {
                    result += decodeBloks[i, k, j];
                }
            }
        }
        return result.TrimEnd('\0');
    }

    public static string Decode(string[] text, string key, int rows)
    {
        var count = (int)Math.Ceiling((float)text.Length / key.Length / rows);
        var head = new List<KeyValuePair<char, int>>();
        head.AddRange(key.Select((val, index) => new KeyValuePair<char, int>(val, index)));
        head = head.OrderBy(kv => kv.Key).ToList();
        var result = "";
        foreach (var t in text)
        {
            var str = new char[key.Length];
            for (var j = 0; j < key.Length; j++)
            {
                str[head[j].Value] = t[j];
            }
            result += new string(str);
        }
        return result.TrimEnd('\0');
    }

    public static void Print(char[,,] tables, int rows, int columns, int count, string key)
    {
        for (var i = 0; i < count; i++)
        {
            Console.WriteLine($"Таблица номер {i}");
            for (var j = 0; j < rows; j++)
            {
                for (var k = 0; k < columns; k++)
                {
                    Console.Write($"{tables[i, j, k]} ");
                }
                Console.WriteLine("");
            }
        }
    }
}


