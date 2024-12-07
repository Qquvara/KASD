using MyVector;
using System;
using System.IO;
using System.Numerics;

namespace laba7new
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("Z:/input.txt");
            var validIPs = new MyVector<string>(10);

            foreach (string line in lines)
            {
                string ss = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '.' || (line[i] >= '0' && line[i] <= '9'))
                    {
                        ss += line[i];
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(ss))
                        {
                            if (CheckIP(ss)) validIPs.Add(ss);
                            ss = "";
                        }
                    }
                }


                if (!string.IsNullOrEmpty(ss) && CheckIP(ss)) validIPs.Add(ss);
            }


            File.WriteAllLines("Z:/output.txt", validIPs.ToArray());
        }

        static bool CheckIP(string ss)
        {
            bool isValid = true;
            string[] parts = ss.Split('.');
            if (parts.Length != 4) return false;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
    }
}