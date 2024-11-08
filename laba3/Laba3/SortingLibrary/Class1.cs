using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLibrary
{
    public static class SortingLib
    {
        //сортировка пузырьком
        public static int[] BubbleSort(int[] array, bool Order = false)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (Order)
                    {
                        if (array[i] > array[j])
                        {
                            int temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                        continue;
                    }
                    if (array[i] < array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }
        //сортировка вставками
        public static int[] InsertionSort(int[] array, bool Order = false)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i;
                while ((j > 0) && ((Order && array[j] < array[j - 1]) || (!Order && array[j] > array[j - 1])))
                {
                    int temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }
            return array;
        }
        //сортировка выбором
        public static int[] SelectionSort(int[] array, bool Order = false)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int maxMinIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if ((!Order && array[j] > array[maxMinIndex]) || (Order && array[j] < array[maxMinIndex]))
                    {
                        maxMinIndex = j;
                    }
                }
                int temp = array[maxMinIndex];
                array[maxMinIndex] = array[i];
                array[i] = temp;
            }
            return array;
        }
        //шейкерная сортировка
        public static int[] ShakerSort(int[] array, bool Order = false)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end - 1; i++)
                {
                    if ((Order && array[i] > array[i + 1]) || (!Order && array[i] < array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;
                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if ((Order && array[i] > array[i + 1]) || (!Order && array[i] < array[i + 1]))
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }
                start++;
            }
            return array;
        }
        //гномья сортировка
        public static int[] GnomeSort(int[] array, bool Order = false)
        {
            var index = 0;
            var nextIndex = index + 1;

            while (index < array.Length)
            {
                if (index == 0)
                {
                    index = nextIndex;
                    nextIndex++;
                }
                if ((Order && array[index] >= array[index - 1]) || (!Order && array[index] <= array[index - 1]))
                {
                    index = nextIndex;
                    nextIndex++;
                }
                else
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }

            return array;
        }
        //сортировка расческой
        private static int GetNextStep(int s)
        {
            s = s * 1000 / 1247;
            return s > 1 ? s : 1;
        }
        public static int[] CombSort(int[] array, bool Order = false)
        {
            int arrayLength = array.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < arrayLength; i++)
                {
                    if (array[i] < array[i + currentStep])
                    {
                        int temp = array[i];
                        array[i] = array[i + currentStep];
                        array[i + currentStep] = temp;
                    }
                }
                currentStep = GetNextStep(currentStep);
            }
            BubbleSort(array, Order);
            return array;
        }
        //пирамидальная сортировка
        public static void Heapify(int[] array, int i, int n)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left] > array[largest]) largest = left;
            if (right < n && array[right] > array[largest]) largest = right;

            if (largest != i)
            {
                int temp = array[largest];
                array[largest] = array[i];
                array[i] = temp;

                Heapify(array, largest, n);
            }
        }
        public static int[] HeapSort(int[] array, bool Order = false)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (!Order)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    int temp = (int)array[i];
                    array[i] = array[n - i - 1];
                    array[n - i - 1] = temp;
                }
            }
            return array;
        }
        //быстрая сортировка
        private static int Partition(int[] array, int minIndex, int maxIndex, bool Order = false)
        {
            int pivot = array[minIndex];
            int pivotIndex = 0;
            int left = minIndex;
            if (minIndex + 1 >= maxIndex) return left;

            for (int i = minIndex + 1; i < maxIndex; i++)
            {
                if ((Order && array[i] < pivot) || (!Order && array[i] > pivot))
                {
                    int temp = array[i];
                    array[i] = array[left];
                    array[left] = temp;
                    left++;
                }
                if (array[i] == pivot)
                {
                    pivotIndex = i;
                }
                else if (array[left] == pivot)
                {
                    pivotIndex = left;
                }
            }

            array[pivotIndex] = array[left];
            array[left] = pivot;
            return left;
        }
        public static void QuickSort(this int[] array, int minIndex, int maxIndex, bool Order = false)
        {
            if (minIndex < maxIndex)
            {
                int point = Partition(array, minIndex, maxIndex, Order);
                QuickSort(array, minIndex, point, Order);
                QuickSort(array, point + 1, maxIndex, Order);
            }
        }
        public static int[] QuickSort(int[] array, bool Order = false)
        {
            QuickSort(array, 0, array.Length, Order);
            return array;
        }
        //сортировка слиянием
        private static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex, bool Order = false)
        {
            int length1 = middleIndex - lowIndex + 1;
            int length2 = highIndex - middleIndex;

            int[] arrayLeft = new int[length1];
            int[] arrayRight = new int[length2];

            for (int i = 0; i < length1; i++) arrayLeft[i] = array[lowIndex + i];
            for (int i = 0; i < length2; i++) arrayRight[i] = array[middleIndex + i + 1];

            int indexLeft = 0, indexRight = 0, index = lowIndex;

            while (indexLeft < length1 && indexRight < length2)
            {
                if ((Order && arrayLeft[indexLeft] < arrayRight[indexRight]) || (!Order && arrayLeft[indexLeft] > arrayRight[indexRight]))
                {
                    array[index] = arrayLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    array[index] = arrayRight[indexRight];
                    indexRight++;
                }
                index++;
            }

            while (indexLeft < length1)
            {
                array[index] = arrayLeft[indexLeft];
                indexLeft++;
                index++;
            }
            while (indexRight < length2)
            {
                array[index] = arrayRight[indexRight];
                indexRight++;
                index++;
            }

        }
        public static void MergeSort(this int[] array, int lowIndex, int highIndex, bool Order = false)
        {
            if (lowIndex >= highIndex) return;

            int middleIndex = lowIndex + (highIndex - lowIndex) / 2;
            array.MergeSort(lowIndex, middleIndex, Order);
            array.MergeSort(middleIndex + 1, highIndex, Order);
            Merge(array, lowIndex, middleIndex, highIndex, Order);
        }
        public static int[] MergeSort(int[] array, bool Order)
        {
            int lowIndex = 0;
            int highIndex = array.Length - 1;
            array.MergeSort(lowIndex, highIndex, Order);
            return array;
        }
        //сорнтировка подсчетом
        public static int[] CountSort(int[] array, bool Order = false)
        {

            int max = array[0];
            foreach (int item in array) max = Math.Max(max, item);

            int[] countArray = new int[max + 1];
            foreach (int item in array) countArray[item]++;

            for (int i = 1; i <= max; i++) countArray[i] += countArray[i - 1];

            int[] answerArray = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                answerArray[countArray[array[i]] - 1] = array[i];
                countArray[array[i]]--;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (!Order)
                {
                    array[i] = answerArray[array.Length - i - 1];
                    continue;
                }
                array[i] = answerArray[i];
            }
            return countArray;
        }
        //битонная сортировка
        static void Swap<T>(ref T current, ref T next)
        {
            T temp;
            temp = current;
            current = next;
            next = temp;
        }
        public static void ComparisonAndSwap(int[] array, int i, int j, bool dir)
        {
            bool flag;
            if (array[i] > array[j]) flag = true;
            else flag = false;
            if (dir == flag) Swap(ref array[i], ref array[j]);
        }
        public static void BitonicMerge(int[] array, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++) ComparisonAndSwap(array, i, i + k, dir);
                BitonicMerge(array, low, k, dir);
                BitonicMerge(array, low + k, k, dir);
            }
        }
        public static void BitonicSortPart(int[] array, int low, int count, bool dir)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortPart(array, low, k, true);
                BitonicSortPart(array, low + k, k, false);
                BitonicMerge(array, low, count, dir);
            }
        }
        public static int[] BitonicSort(int[] array, bool Order = false)
        {
            double size = array.Length;
            int pow = 0;
            while (size > 1)
            {
                size /= 2;
                pow++;
            }
            if (size != 1) pow++;

            int[] newArray = new int[(int)Math.Pow(2, pow)];
            for (int i = 0; i < newArray.Length; i++)
            {
                if (i < array.Length)
                {
                    newArray[i] = array[i];
                    continue;
                }
                newArray[i] = -1;
            }

            BitonicSortPart(newArray, 0, newArray.Length, Order);

            for (int i = 0; i < newArray.Length; i++)
            {
                if (!Order && newArray[i] != -1) array[i] = newArray[i];
                if (Order && newArray[newArray.Length - 1 - i] != -1) array[array.Length - 1 - i] = newArray[newArray.Length - 1 - i];
            }
            return array;
        }
        //сортировка Шелла
        public static int[] ShellSort(int[] array, bool Order = false)
        {
            for (int interval = array.Length / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < array.Length; i++)
                {
                    int j = i;
                    while (j >= interval && ((Order && array[j - interval] > array[j]) || (!Order && array[j - interval] < array[j])))
                    {
                        Swap(ref array[j], ref array[j - interval]);
                        j -= interval;
                    }
                }
            }
            return array;
        }
        //сортировка деревом
        public class TreeNode
        {
            public int Data { get; set; }
            public TreeNode(int data)
            {
                Data = data;
            }

            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public void Insert(TreeNode Root)
            {
                if (Root.Data < Data)
                {
                    if (Left == null) Left = Root;
                    else Left.Insert(Root);
                }
                else
                {
                    if (Right == null) Right = Root;
                    else Right.Insert(Root);
                }
            }


            public int[] Transform(List<int> elements = null)
            {
                if (elements == null) elements = new List<int>();
                if (Left != null) Left.Transform(elements);
                elements.Add(Data);
                if (Right != null) Right.Transform(elements);
                return elements.ToArray();
            }
        }

        public static int[] TreeSort(int[] array, bool Order = false)
        {
            TreeNode Root = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++) Root.Insert(new TreeNode(array[i]));
            int[] newArray = Root.Transform();
            for (int i = 0; i < array.Length; i++)
            {
                if (!Order)
                {
                    array[i] = newArray[array.Length - 1 - i];
                    continue;
                }
                array[i] = newArray[i];
            }
            return array;
        }
        //поразрядная сортировка
        public static int GetMaximum(int[] array)
        {
            var max = array[0];
            for (int i = 1; i < array.Length; i++) if (array[i] > max) max = array[i];
            return max;
        }
        public static void CountingSort(int[] array, int exponent, bool Order = false)
        {
            int[] outputArray = new int[array.Length];
            int[] countArray = new int[10];

            for (int i = 0; i < 10; i++) countArray[i] = 0;

            for (int i = 0; i < array.Length; i++) countArray[(array[i] / exponent) % 10]++;

            for (int i = 1; i < 10; i++) countArray[i] += countArray[i - 1];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                outputArray[countArray[(array[i] / exponent) % 10] - 1] = array[i];
                countArray[(array[i] / exponent) % 10]--;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (!Order)
                {
                    array[i] = outputArray[array.Length - i - 1];
                    continue;
                }
                array[i] = outputArray[i];
            }
        }
        public static int[] RadixSort(int[] array, bool Order = false)
        {
            var max = GetMaximum(array);
            for (int exponent = 1; max / exponent > 0; exponent *= 10) CountingSort(array, exponent, Order);
            return array;
        }

        //генерация массивов
        public static Random random = new Random();
        public static int[] GenerateRandomArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1000); //генерация случайных чисел по модулю 1000
            }
            return array;
        }
        public static int[] GenerateSortedSubarrays(int totalSize)
        {
            int maxSubarraySize = random.Next(10, totalSize);
            int[] fullArray = new int[totalSize];
            int currentIndex = 0;
            while (totalSize > 0)
            {
                //генерация случайного размера подмассива
                int subarraySize = random.Next(1, Math.Min(totalSize, maxSubarraySize) + 1);
                int[] subarray = GenerateRandomArray(subarraySize);
                Array.Sort(subarray); //сортировка подмассива

                //добавление отсортированного подмассива в полный массив
                for (int j = 0; j < subarray.Length; j++)
                {
                    fullArray[currentIndex++] = subarray[j];
                }

                totalSize -= subarraySize; //уменьшение оставшегося объема
            }
            return fullArray;
        }
        public static int[] GenerateSwappedSortedArray(int size)
        {
            int[] array = GenerateRandomArray(size);
            Array.Sort(array); //сортировка массива
            int k = random.Next(array.Length);
            //перестановка двух случайных элементов
            for (int i = 0; i < k; i++)
            {
                if (size >= 2)
                {
                    int index1 = random.Next(size);
                    int index2 = random.Next(size);
                    int temp = array[index1];
                    array[index1] = array[index2];
                    array[index2] = temp;
                }
            }

            return array;
        }
        public static int[] RandomBySwapAndRepeat(int length)
        {
            int[] array = GenerateSwappedSortedArray(length);
            Random random = new Random();
            int indexOfRepeat = random.Next(0, length - 1);
            int countOfRepeat = random.Next(0, length / 3);

            while (countOfRepeat > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[indexOfRepeat])
                {
                    array[randomIndex] = array[indexOfRepeat];
                    countOfRepeat--;
                }

            }
            return array;
        }

        //массив случайных чисел
        public static int[] Array1(int size)
        {
            int[] array = new int[size];
            array = GenerateRandomArray(size);
            return array;
        }
        //массивы разбитые на отсортированные подмассивы
        public static int[] Array2(int size)
        {
            int[] sortedSubarrays = GenerateSortedSubarrays(size);
            return sortedSubarrays;
        }
        //изначально отсортированные массивы с перестановками
        public static int[] Array3(int size)
        {
            int[] swappedSortedArray = GenerateSwappedSortedArray(size);
            return swappedSortedArray;
        }
        //полностью отсортированные массивы
        public static int[] Array4(int size)
        {
            int[] sortedArrays = RandomBySwapAndRepeat(size);
            return sortedArrays;
        }
    }
}
