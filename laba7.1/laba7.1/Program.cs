using MyVector;
using System.Numerics;

namespace Laba7
{
    public class Laba7
    {
        static string Input = "input.txt";
        static string Output = "output.txt";
        static StreamReader sr = new StreamReader(Input);
        static StreamWriter sw = new StreamWriter(Output);

        static MyVector<string> Ip()
        {
            string l = sr.ReadLine();
            if (l == null) throw new Exception("Пустая строка");

            var res = new MyVector<string>(10);
            while (l != null)
            {
                string[] IParray = l.Split(' ');
                foreach (string IP in IParray)
                {
                    bool isIP = true;
                    string[] helpArray = IP.Split('.').ToArray();
                    int[] IPlim = new int[helpArray.Length];
                    for (int i = 0; i < helpArray.Length; i++)
                        IPlim[i] = Convert.ToInt32(helpArray[i]);
                    foreach (int lim in IPlim)
                    {
                        if (!(lim >= 0 && lim <= 255)) isIP = false;
                    }
                    if (isIP && IPlim.Length == 4) res.Add(IP);
                }
                l = sr.ReadLine();
            }
            return res;
        }
        static void WriteToFile(MyVector<string> res)
        {
            for (int i = 0; i < res.Size(); i++)
            {
                sw.WriteLine(res.Get(i));
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            MyVector<string> IP = new MyVector<string>(10);
            IP = Ip();
            WriteToFile(IP);
        }
    }
}