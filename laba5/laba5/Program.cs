using MyArrayList;
using System.Data;

namespace Lab5
{
    public class Lab5
    {
        static string file = "C:/viper/laba5.txt";
        static StreamReader sr = new StreamReader(file);
        static MyArrayList<string> Teg()
        {
            string line = sr.ReadLine();
            if (line == null) { throw new Exception("Пустая строка"); }
            var result = new MyArrayList<string>(10);
            while (line != null)
            {
                bool isOpen = false;
                bool isTeg = false;
                string teg = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && line[i + 1] != null)
                    {
                        if (line[i + 1] == '/' || char.IsLetter(line[i + 1]) && !isOpen) { isOpen = true; }
                        if (line[i + 1] == '>') { isOpen = true; }
                    }
                    if (line[i] == '>' && isOpen == true) { teg += line[i]; isTeg = true; isOpen = false; }
                    if (isOpen && (line[i] == '<' || line[i] == '/' || char.IsLetter(line[i]) || char.IsDigit(line[i]))) teg += line[i];
                    if (isTeg) { result.Add(teg); teg = ""; isTeg = false; }
                }
                line = sr.ReadLine();
            }
            return result;
        }
        static MyArrayList<string> Delete(MyArrayList<string> Array)
        {
            MyArrayList<string> result = new MyArrayList<string>(10);
            MyArrayList<string> allLowerTegs = new MyArrayList<string>(10);
            string dublicate;
            string dublicateLower;
            for (int i = 0; i < Array.Size(); i++)
            {
                dublicate = Array.Get(i);
                dublicateLower = dublicate.ToLower();
                allLowerTegs.Add(dublicateLower);
            }

            for (int i = 0; i < allLowerTegs.Size(); i++)
            {
                string teg1 = allLowerTegs.Get(i);
                for (int j = i + 1; j < allLowerTegs.Size(); j++)
                {
                    bool flag = true;
                    string teg2 = allLowerTegs.Get(j);
                    if (Math.Abs(teg2.Length - teg1.Length) > 1) continue;
                    if (teg2.Length > teg1.Length && teg2[1] == '/')
                    {
                        for (int k = 1; k < teg1.Length; k++)
                            if (teg1[k] != teg2[k + 1]) { flag = false; break; }
                    }
                    else if (teg1.Length > teg2.Length && teg1[1] == '/')
                    {
                        for (int k = 1; k < teg2.Length; k++)
                            if (teg2[k] != teg1[k + 1]) { flag = false; break; }
                    }
                    else if (teg2.Length == teg1.Length)
                    {
                        for (int k = 1; k < teg1.Length; k++)
                            if (teg1[k] != teg2[k]) { flag = false; break; }
                    }
                    else continue;
                    if (flag) allLowerTegs.Set(j, "false");
                }
            }
            for (int i = 0; i < allLowerTegs.Size(); i++)
            {
                if (allLowerTegs.Get(i) == "false") continue;
                result.Add(Array.Get(i));
            }
            return result;
        }
        static void Main(string[] args)
        {
            var array = new MyArrayList<string>(10);
            var teg = new MyArrayList<string>(10);
            array = Teg();
            teg = Delete(array);
            for (int i = 0; i < teg.Size(); i++)
            {
                Console.WriteLine(teg.Get(i));
            }
        }
    }
}