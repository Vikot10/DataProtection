using System.Text;

new Task5().Encode();

class Task5
{
    string keysPath = Environment.CurrentDirectory + "/keys.txt";
    string textPath = Environment.CurrentDirectory + "/text.txt";

    public void Encode()
    {
        string[] keys = File.ReadAllLines(keysPath);

        for (int i= 0;i<keys.Length;i++)
        {
            Console.WriteLine($"Ключ[{i}] = {keys[i]}");
        }

        string text = File.ReadAllText(textPath);
        Console.WriteLine($"Текст = {text}");

        string bitsText = ConvertTextToBits(text);
        Console.WriteLine($"Бинарный текст = {bitsText}");

        if (bitsText.Length % 64 != 0)
        {
            bitsText = bitsText.PadRight(bitsText.Length + 64 - bitsText.Length % 64, '0');
        }

        for (int i = 0; i < bitsText.Length; i += 64)
        {
            string A = bitsText.Substring(i, 16);
            Console.WriteLine($"A = {A}");

            string B = bitsText.Substring(i + 16, 16);
            Console.WriteLine($"B = {B}");

            string C = bitsText.Substring(i + 32, 16);
            Console.WriteLine($"C = {C}");

            string D = bitsText.Substring(i + 48, 16);
            Console.WriteLine($"D = {D}");

            Console.WriteLine("-----------------------------------------------------------------");

            for (int j = 0; j < 8; j++)
            {
                Console.WriteLine($"A = {A}");
                Console.WriteLine($"keys[{j * 6}] = {keys[j * 6]}");
                A = mul(A, keys[j * 6]);
                Console.WriteLine($"A = {A}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"B = {B}");
                Console.WriteLine($"keys[{j * 6+1}] = {keys[j * 6+1]}");
                B = sum(B, keys[j * 6 + 1]);
                Console.WriteLine($"B = {B}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"C = {C}");
                Console.WriteLine($"keys[{j * 6 + 2}] = {keys[j * 6 + 2]}");
                C = sum(C, keys[j * 6 + 2]);
                Console.WriteLine($"C = {C}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"D = {D}");
                Console.WriteLine($"keys[{j * 6 + 3}] = {keys[j * 6 + 3]}");
                D = mul(D, keys[j * 6 + 3]);
                Console.WriteLine($"D = {D}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"A = {A}");
                Console.WriteLine($"C = {C}");
                string E = xor(A, C);
                Console.WriteLine($"E = {E}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"A = {B}");
                Console.WriteLine($"C = {D}");
                string F = xor(B, D);
                Console.WriteLine($"F = {F}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"A = {E}");
                Console.WriteLine($"keys[{j * 6 + 4}] = {keys[j * 6 + 4]}");
                E = mul(E, keys[j * 6 + 4]);
                Console.WriteLine($"E = {E}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"F = {F}");
                Console.WriteLine($"E = {E}");
                F = sum(F, E);
                Console.WriteLine($"F = {F}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"F = {F}");
                Console.WriteLine($"keys[{j * 6 + 5}] = {keys[j * 6 + 5]}");
                F = mul(F, keys[j * 6 + 5]);
                Console.WriteLine($"F = {F}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"E = {E}");
                Console.WriteLine($"F = {F}");
                E = sum(E, F);
                Console.WriteLine($"E = {E}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"A = {A}");
                Console.WriteLine($"F = {F}");
                A = xor(A, F);
                Console.WriteLine($"A = {A}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"C = {C}");
                Console.WriteLine($"F = {F}");
                C = xor(C, F);
                Console.WriteLine($"C = {C}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"B = {B}");
                Console.WriteLine($"E = {E}");
                B = xor(B, E);
                Console.WriteLine($"B = {B}");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.WriteLine($"D = {D}");
                Console.WriteLine($"E = {E}");
                D = xor(D, E);
                Console.WriteLine($"D = {D}");
                Console.WriteLine("-----------------------------------------------------------------");
            }
            var kl = keys.Length;

            Console.WriteLine($"A = {A}");
            Console.WriteLine($"keys[{kl - 4}] = {keys[kl - 4]}");
            A = mul(A, keys[kl - 4]);
            Console.WriteLine($"A = {A}");
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine($"B = {B}");
            Console.WriteLine($"keys[{kl - 3}] = {keys[kl - 3]}");
            B = sum(B, keys[kl - 3]);
            Console.WriteLine($"B = {B}");
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine($"C = {C}");
            Console.WriteLine($"keys[{kl - 2}] = {keys[kl - 2]}");
            C = sum(C, keys[kl - 2]);
            Console.WriteLine($"C = {C}");
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine($"D = {D}");
            Console.WriteLine($"keys[{kl - 1}] = {keys[kl - 1]}");
            D = mul(D, keys[kl - 1]);
            Console.WriteLine($"D = {D}");
            Console.WriteLine("-----------------------------------------------------------------");
        }
    }
    public string ConvertTextToBits(string text)
    {
        byte[] byteArray = Encoding.ASCII.GetBytes(text);

        StringBuilder binarySequence = new StringBuilder();
        foreach (byte b in byteArray)
        {
            string binaryString = Convert.ToString(b, 2).PadLeft(8, '0');
            binarySequence.Append(binaryString);
        }

        return binarySequence.ToString();
    }
    public string sum(string s1, string s2)
    {
        int x = BinaryToInt(s1,16);
        int y = BinaryToInt(s2,16);
        return IntToBinaryString((x + y) & 0xFFFF, 16);
    }
    public int Pow(int number, int degree)
    {
        for (int i = 0; i < degree; i++)
        {
            number *= 2;
        }
        return number;
    }
    public int BinaryToInt(string binary, int rate)
    {
        int res = 0;
        for (int i = 0; i < rate; i++)
        {
            res += Pow(Int32.Parse(binary[rate - 1 - i].ToString()), i);
        }
        return res;
    }
    public string IntToBinaryString(int number, int rate)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rate; i++)
        {
            sb.Append(number >> i & 1);
        }
        var chars = sb.ToString().ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
    public string mul(string s1, string s2)
    {
        int x = BinaryToInt(s1, 16);
        int y = BinaryToInt(s2, 16);
        long m = (long)x * y;
        if (m != 0)
        {
            return IntToBinaryString((int)(m % 0x10001) & 0xFFFF, 16);
        }
        else
        {
            if (x != 0 || y != 0)
            {
                return IntToBinaryString((1 - x - y) & 0xFFFF, 16);
            }
            return IntToBinaryString(1, 16);
        }
    }
    public char XorOne(char ch1, char ch2)
    {
        return ch1 == ch2 ? '0' : '1';
    }
    public string xor(string s1, string s2)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = s1.Length - 1; i >= 0; i--)
        {
            sb.Append(XorOne(s1[i], s2[i]));
        }
        return sb.ToString();
    }
}

