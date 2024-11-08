using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using SortingLibrary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Laba3
{
    public partial class Form1 : Form
    {

        int flag = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива,млрд шт";
            pane.YAxis.Title.Text = "Время выполнения,мс";
            pane.Title.Text = "Зависимость времени выполнения от размера массива";

        }
        public void InsertPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\array.txt";
            pathToTime = appDir + @"\time.txt";
        }
        string pathToArray;
        string pathToTime;
    
        private void SpeedTime(Func<int, int[]> Generate, int length, bool Order, params Func<int[], bool, int[]>[] SortMethods)
        {
            InsertPath();
            double[] sumsp = new double[SortMethods.Length];

            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(length);

                try
                {
                 
                    using (StreamWriter sw = File.AppendText(pathToArray))
                    {
                        sw.WriteLine("Unsorted array: ");
                        sw.WriteLine(string.Join(" ", array));

                        int index = 0;
                        foreach (Func<int[], bool, int[]> Method in SortMethods)
                        {
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            int[] sortedArray = Method(array, Order);
                            timer.Stop();
                            sumsp[index] += timer.ElapsedMilliseconds;
                            index++;
                            sw.WriteLine($"Sorted array by {Method.Method.Name}: ");
                            sw.WriteLine(string.Join(" ", sortedArray));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            try
            {
                using (StreamWriter sw = File.AppendText(pathToTime))
                {
                    for (int i = 0; i < sumsp.Length; i++)
                    {
                        sw.Write(((double)(sumsp[i] / 20)).ToString());
                        if (i < sumsp.Length - 1)
                        {
                            sw.Write(" ");
                        }
                    }
                    sw.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    flag = 1;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 1000; length *= 10)
                                SpeedTime(SortingLib.Array1, length, true, SortingLib.BubbleSort, SortingLib.InsertionSort, SortingLib.SelectionSort, SortingLib.ShakerSort, SortingLib.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(SortingLib.Array2, length, true, SortingLib.BubbleSort, SortingLib.InsertionSort, SortingLib.SelectionSort, SortingLib.ShakerSort, SortingLib.GnomeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(SortingLib.Array3, length, true, SortingLib.BubbleSort, SortingLib.InsertionSort, SortingLib.SelectionSort, SortingLib.ShakerSort, SortingLib.GnomeSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(SortingLib.Array4, length, true, SortingLib.BubbleSort, SortingLib.InsertionSort, SortingLib.SelectionSort, SortingLib.ShakerSort, SortingLib.GnomeSort);
                            break;
                    }
                    break;
                case 1:
                    flag = 2;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(SortingLib.Array1, length, true, SortingLib.ShellSort, SortingLib.BitonicSort, SortingLib.TreeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(SortingLib.Array2, length, true, SortingLib.ShellSort, SortingLib.BitonicSort, SortingLib.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(SortingLib.Array3, length, true, SortingLib.ShellSort, SortingLib.BitonicSort, SortingLib.TreeSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(SortingLib.Array4, length, true, SortingLib.ShellSort, SortingLib.BitonicSort, SortingLib.TreeSort);
                            break;
                    }
                    break;
                case 2:
                    flag = 3;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(SortingLib.Array1, length, true, SortingLib.CombSort, SortingLib.HeapSort, SortingLib.QuickSort, SortingLib.MergeSort, SortingLib.CountSort, SortingLib.RadixSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(SortingLib.Array2, length, true, SortingLib.CombSort, SortingLib.HeapSort, SortingLib.QuickSort, SortingLib.MergeSort, SortingLib.CountSort, SortingLib.RadixSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(SortingLib.Array3, length, true, SortingLib.CombSort, SortingLib.HeapSort, SortingLib.QuickSort, SortingLib.MergeSort, SortingLib.CountSort, SortingLib.RadixSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(SortingLib.Array4, length, true, SortingLib.CombSort, SortingLib.HeapSort, SortingLib.QuickSort, SortingLib.MergeSort, SortingLib.CountSort, SortingLib.RadixSort);
                            break;
                    }
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование скорости работы сортировок";
            InsertPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray)
                    {
                        speed.Add(Convert.ToDouble(str));
                    }
                    time.Add(speed);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

            for (int i = 0; i < time[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j-1 < time[i].Count(); j++)
                {

                    pointList.Add(x, time[j][i]);
                    x *= 10;
                }
                switch (i)
                {
                    case 0:
                        if (flag == 1)
                        {
                            pane.AddCurve("Пузырек: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Ракушка: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Расческа: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1)
                        {
                            pane.AddCurve("Вставками: " + i, pointList, Color.Red, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Деревом: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Кучей: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 2:
                        if (flag == 1)
                        {
                            pane.AddCurve("Выбором: " + i, pointList, Color.Pink, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Битон: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Быстрая: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 3:
                        if (flag == 1)
                        {
                            pane.AddCurve("Шейкерная: " + i, pointList, Color.Yellow, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Слияние: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 4:
                        if (flag == 1)
                        {
                            pane.AddCurve("Гномья: " + i, pointList, Color.Green, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Подсчетом: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 5:
                        pane.AddCurve("Разрядная: " + i, pointList, Color.Aquamarine, SymbolType.Default);
                        break;
                }
            }

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
