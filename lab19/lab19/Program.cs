using MyHashMap;
using System;
using System.IO;

namespace Lab19
{
    public class Lab19
    {
        static string file = "Z:/INPUTIK.txt";
        static StreamReader sr = new StreamReader(file);

        static public MyHashMap<string, int> Teg()
        {
            string line = sr.ReadLine();
            if (line == null) { throw new Exception("Пустая строка"); }
            var result = new MyHashMap<string, int>();

            while (line != null)
            {
                bool isOpen = false;
                bool isTeg = false;
                string teg = "";

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && i + 1 < line.Length)
                    {
                        if (line[i + 1] == '/' || char.IsLetter(line[i + 1])) { isOpen = true; }
                    }

                    if (line[i] == '>' && isOpen)
                    {
                        teg += line[i];
                        isTeg = true;
                        isOpen = false;
                    }

                    if (isOpen && (char.IsLetter(line[i]) || char.IsDigit(line[i]) || line[i] == '/'))
                    {
                        teg += line[i];
                    }

                    if (isTeg)
                    {
                        // Удаляем символ '/' и '>' и приводим тег к нижнему регистру
                        string cleanedTeg = teg.Replace("/", "").Replace(">", "").ToLower();

                        // Используем метод Put для добавления или обновления значения
                        int cnt = result.Get(cleanedTeg);
                        result.Put(cleanedTeg, cnt + 1); // Если ключа нет, Get вернет 0, что корректно

                        teg = "";
                        isTeg = false;
                    }
                }

                line = sr.ReadLine();
            }

            return result;
        }

        static void Main(string[] args)
        {
            var array = Teg();
            var teg = array.KeySet().ToArray();
            for (int i = 0; i < teg.Length; i++)
            {
                Console.WriteLine($"{teg[i]} : {array.Get(teg[i])}");
            }
        }
    }
}
