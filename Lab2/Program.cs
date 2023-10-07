var t = new Task2();

t.Encode();
t.Decode();

public class Task2
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _numberKeys = Environment.CurrentDirectory + "/keys.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";
    Gronsfild gronsfild = new();

    public void Encode()
    {
        var text = File.ReadAllText(_originalPath);
        var keysText = File.ReadAllText(_numberKeys);
        var keys = keysText.Trim().Split(" ");
        var firstKey = keys.First();
        var secondKey = keys.Last();

        var result = gronsfild.Encode(text, firstKey);
        result = gronsfild.Decode(result, secondKey);
        result = gronsfild.Encode(result, firstKey);
        File.WriteAllText(_encodedPath, result);
    }
    public void Decode()
    {
        var text = File.ReadAllText(_encodedPath);
        var keysText = File.ReadAllText(_numberKeys);
        var keys = keysText.Trim().Split(" ");
        var firstKey = keys.First();
        var secondKey = keys.Last();

        var result = gronsfild.Decode(text, firstKey);
        result = gronsfild.Encode(result, secondKey);
        result = gronsfild.Decode(result, firstKey);
        File.WriteAllText(_decodedPath, result);
    }
}

public class Gronsfild
{
    public string Encode(string text, string key)
    {
        var result = "";
        while (text.Length > key.Length)
        {
            key += key;
        }
        for (var i = 0; i < text.Length; i++)
        {
            result += (char)(text[i] + (key[i] - '0'));
        }
        return result;
    }

    public string Decode(string text, string key)
    {
        var result = "";
        while (text.Length > key.Length)
        {
            key += key;
        }
        for (var i = 0; i < text.Length; i++)
        {
            result += (char)(text[i] - (key[i] - '0'));
        }
        return result;
    }
}



