using System.Text;

var t = new Task3();

t.Encode();

public class Task3
{
    readonly string _originalPath = Environment.CurrentDirectory + "/original.txt";
    readonly string _encodedPath = Environment.CurrentDirectory + "/encoded.txt";
    readonly string _decodedPath = Environment.CurrentDirectory + "/decoded.txt";
    Caesar Caesar = new Caesar();

    public void Encode()
    {
        var text = File.ReadAllText(_originalPath);
        var k = Int32.Parse(Console.ReadLine()!);

        var result = Caesar.Encode(text, k);
        File.WriteAllText(_encodedPath, result);
    }
}

public class Caesar
{
    string alf = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя .,-!?";

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
            result.Append(decAlf[ch]);
        }
        return result.ToString();
    }
}



