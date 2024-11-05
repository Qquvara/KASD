using System.Runtime.ConstrainedExecution;

namespace ConsoleApp
{
    public struct Complex
    {
        public double x;
        public double y;
        public Complex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return x.ToString() + " " + y.ToString() + "i";
        }
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.x + b.x, a.y + b.y);
        }
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.x - b.x, a.y - b.y);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.x * b.x - b.y * a.y, a.x * b.y + a.y * b.x);
        }
        public static Complex operator /(Complex a, Complex b)
        {
            return new Complex((a.x * b.x + b.y * a.y) / (Math.Pow(b.x, 2) + Math.Pow(a.y, 2)), (a.x * b.y + b.x * a.y) / (Math.Pow(b.x, 2) + Math.Pow(a.y, 2)));
        }
        public double Module()
        {
            return Math.Sqrt((Math.Pow(x, 2)) + (Math.Pow(y, 2)));
        }
        public double Argument()
        {
            if (x > 0) return Math.Atan(y / x);

            if ((x < 0) && (y >= 0)) return Math.PI + Math.Atan(y / x);

            if ((x < 0) && (y < 0)) return -Math.PI + Math.Atan(y / x);

            if ((x == 0) && (y > 0)) return Math.PI / 2;

            if ((x == 0) && (y < 0)) return -Math.PI / 2;

            return 0;
        }
        public double Valid()
        {
            return x;
        }
        public double Imaginary()
        {
            return y;
        }

        static void Main(string[] args)
        {
            string operation;
            do
            {

                Console.WriteLine("введите 1-ое комплексное число");
                Console.WriteLine("введите действительную часть: ");
                string X1 = Console.ReadLine();
                Console.WriteLine("введите мнимую часть:");
                string Y1 = Console.ReadLine();
                double x1 = Convert.ToDouble(X1);
                double y1 = Convert.ToDouble(Y1);
                Complex a = new Complex(x1, y1);

                Console.WriteLine("операция:");
                operation = Console.ReadLine();


                switch (operation)
                {
                    case "Mod":
                        double Mod = a.Module();
                        Console.WriteLine(Mod);
                        break;
                    case "Arg":
                        double Arg = a.Argument();
                        Console.WriteLine(Arg);
                        break;
                    case "Valid":
                        double x = a.Valid();
                        Console.WriteLine(x);
                        break;
                    case "Imaginary":
                        double y = a.Imaginary();
                        Console.WriteLine(y);
                        break;
                    case "Q":
                        Console.WriteLine("выход из программы");
                        break;
                    case "q":
                        Console.WriteLine("выход из программы");
                        break;
                    default:
                        Console.WriteLine("введите 2-ое комплексное число");
                        Console.WriteLine("введите действительную часть:");
                        string X2 = Console.ReadLine();
                        Console.WriteLine("Введите мнимую часть:");
                        string Y2 = Console.ReadLine();
                        double x2 = Convert.ToDouble(X2);
                        double y2 = Convert.ToDouble(Y2);
                        Complex b = new Complex(x2, y2);
                        switch (operation)
                        {
                            case "+":
                                Complex plus = a + b;
                                Console.WriteLine(plus);
                                break;
                            case "-":
                                Complex minus = a - b;
                                Console.WriteLine(minus);
                                break;
                            case "*":
                                Complex multi = a * b;
                                Console.WriteLine(multi);
                                break;
                            case "/":
                                Complex division = a / b;
                                Console.WriteLine(division);
                                break;
                            default:
                                Console.WriteLine("ERROR\n");
                                break;
                        }
                        break;
                }
            } while (operation != "Q" && operation != "q");
        }
    }
}