using System;
using System.IO;
using System.Text.RegularExpressions;
using MyHashMap;

public class Var
{
    static void Main(string[] args)
    {
        MyHashMap<string, string> variableAndType = new MyHashMap<string, string>();
        MyHashMap<string, string> variableAndValue = new MyHashMap<string, string>();
        string pattern = @"^(double|int|float)\s+(\w+)\s*=\s*([\d\.]+[f]?)\s*;\s*$";
        string path = "Z:/input20.txt";

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine("Обрабатывается строка: " + line.Trim());  // Отладочное сообщение

                    Match match = Regex.Match(line.Trim(), pattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string type = match.Groups[1].Value.Trim();
                        string name = match.Groups[2].Value.Trim();
                        string value = match.Groups[3].Value.Trim();

                        if (variableAndType.ContainsKey(name))
                        {
                            Console.WriteLine("Ошибка: переменная " + name + " уже определена.");
                        }
                        else
                        {
                            variableAndType.Put(name, type);
                            variableAndValue.Put(name, value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Предупреждение: некорректная строка: " + line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка чтения файла: " + ex.Message);
        }

        foreach (var key in variableAndType.KeySet())
        {
            Console.WriteLine(variableAndType.Get(key) + " => " + key + $"({variableAndValue.Get(key)})");
        }
    }
}
