string text = "C:/zad1.txt";
StreamReader sr = new StreamReader(text);

int n;
n = Convert.ToInt32(sr.ReadLine());

double[,] matG = new double[n, n];
for (int i = 0; i < n; i++)

{
    string[] l1 = sr.ReadLine().Split(" ");
    for (int j = 0; j < n; j++)

    {
        matG[i, j] = double.Parse(l1[j]);
    }
}

double[] x = new double[n];
string[] l2 = sr.ReadLine().Split(" ");
for (int i = 0; i < n; i++)

{
    x[i] = double.Parse(l2[i]);
}

bool symetric = true;

for (int i = 0; i < n; i++)

{
    for (int j = i + 1; j < n; j++)

    {
        if (matG[i, j] != matG[j, i]) { symetric = false; break; }
    }
    if (!symetric) break;
}

double length = 0;

if (symetric)

{
    for (int i = 0; i < n; i++)

    {
        for (int j = 0; j < n; j++)

        {
            length += x[i] * matG[i, j] * x[j];
        }
    }
    length = Math.Sqrt(length);
}
else Console.WriteLine("матрица не симметрична.");

Console.WriteLine($"длина вектора: {length}");