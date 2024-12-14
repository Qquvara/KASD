using MyArrayList;
using System;

namespace laba10
{
    public class Heap<T> where T : IComparable<T>
    {
        private int size;
        private MyArrayList<T> heap;

        public Heap(T[] array)
        {
            size = array.Length;
            heap = new MyArrayList<T>(size);
            for (int i = 0; i < size; i++)
            {
                heap.Add(array[i]);
            }

            for (int i = (size / 2) - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Heapify(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;

            if (left < size && heap.Get(left).CompareTo(heap.Get(largest)) > 0)
            {
                largest = left;
            }

            if (right < size && heap.Get(right).CompareTo(heap.Get(largest)) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(index, largest);
                Heapify(largest);
            }
        }

        public void Swap(int index1, int index2)
        {
            T temp = heap.Get(index1);
            heap.Set(index1, heap.Get(index2));
            heap.Set(index2, temp);
        }

        public T Search()
        {
            return heap.Get(0);
        }

        public T Extract()
        {
            if (size == 0)
                throw new InvalidOperationException("Heap is empty");

            T root = Search();
            heap.Set(0, heap.Get(size - 1));
            heap.Remove(size - 1); // Удаляем последний элемент
            size--;
            Heapify(0);
            return root;
        }

        public void IncreaseKey(int index, T newKey)
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            heap.Set(index, newKey);
            while (index > 0 && heap.Get(Parent(index)).CompareTo(heap.Get(index)) < 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        public void AddElement(T element)
        {
            heap.Add(element);
            size++;
            IncreaseKey(size - 1, element); // Исправлено, чтобы корректно восстанавливать кучу
        }

        public static Heap<T> MergeHeaps(Heap<T> heap1, Heap<T> heap2)
        {
            MyArrayList<T> mergedList = new MyArrayList<T>(heap1.size + heap2.size);
            for (int i = 0; i < heap1.size; i++)
            {
                mergedList.Add(heap1.heap.Get(i));
            }
            for (int i = 0; i < heap2.size; i++)
            {
                mergedList.Add(heap2.heap.Get(i));
            }

            return new Heap<T>(mergedList.ToArray());
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(heap.Get(i));
            }
            Console.WriteLine();
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите элементы для первой кучи, разделяя их пробелами:");
            int[] array1 = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Console.WriteLine("Введите элементы для второй кучи, разделяя их пробелами:");
            int[] array2 = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Heap<int> heap1 = new Heap<int>(array1);
            Heap<int> heap2 = new Heap<int>(array2);

            Console.WriteLine("\nКуча 1:");
            heap1.Print();
            Console.WriteLine("Куча 2:");
            heap2.Print();
            Console.WriteLine($"Максимальный элемент в Куче 1: {heap1.Search()}");
            Console.WriteLine($"\nИзвлечение из Кучи 2: {heap2.Extract()}\n");
            heap2.Print();

            Console.WriteLine("Введите элемент для добавления в Кучу 1:");
            int newElement = int.Parse(Console.ReadLine());
            heap1.AddElement(newElement);

            Console.WriteLine("\nКуча 1 после добавления нового элемента:");
            heap1.Print();

            Heap<int> mergedHeap = Heap<int>.MergeHeaps(heap1, heap2);
            Console.WriteLine("Слияние куч:");
            mergedHeap.Print();
        }
    }
}
