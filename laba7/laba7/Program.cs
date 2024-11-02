using MyVector;
using System.IO;
using System.Numerics;

namespace Laba7
{
    class Program
    {
        const string InputFile = "input.txt";
        const string OutputFile = "output.txt";
        static readonly StreamReader sr = new StreamReader(InputFile);
        static readonly StreamWriter sw = new StreamWriter(OutputFile);

        static MyVector<string> ReadValidIpsFromFile()
        {
            var validIps = new MyVector<string>(10);

            string line = sr.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                string[] ipArray = line.Split(' ');
                foreach (string ip in ipArray)
                {
                    string[] ipParts = ip.Split('.');
                    if (ipParts.Length != 4) continue;

                    bool isIpValid = true;
                    foreach (string ipPart in ipParts)
                    {
                        if (!byte.TryParse(ipPart, out byte ipByte) || ipByte > 255)
                        {
                            isIpValid = false;
                            break;
                        }
                    }

                    if (isIpValid) validIps.Add(ip);
                }

                line = sr.ReadLine();
            }

            return validIps;
        }

        static void WriteIpsToFile(MyVector<string> ips)
        {
            foreach (string ip in ips)
            {
                sw.WriteLine(ip);
            }

            sw.Close();
        }

        static void Main(string[] args)
        {
            MyVector<string> validIps = ReadValidIpsFromFile();
            WriteIpsToFile(validIps);
        }
    }
}
