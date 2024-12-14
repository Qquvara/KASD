using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayList
{
    public class MyArrayList<T>
    {
        private T[] elementData; // массив универсального типа Т для хранения элементов массива
        private int size; //их количество

        public MyArrayList() // 1. конструктор для создания пустого динамического массива
        {
            size = 0;
            elementData = new T[0];
        }
        public MyArrayList(T[] a) // 2. конструктор для создания динамического массива и заполнения элементами из переданного массива
        {
            elementData = new T[a.Length];

            for (int i = 0; i < size; i++)
            {
                elementData[i] = a[i];
            }
            size = a.Length;
        }
        public MyArrayList(int capacity) // 3. конструктор для создания пустого динамического массива с внутренним массивом
        {
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T e) // 4. метод для добавления элемента в конец динамического массива
        {
            if (size == elementData.Length)
            {
                T[] NewElementData = new T[(size / 2) + size + 1];

                for (int i = 0; i < size; i++)
                {
                    NewElementData[i] = elementData[i];
                }
                elementData = NewElementData;
            }
            elementData[size] = e;
            size++;
        }
        public void AddAll(T[] a) // 5. метод для добавления элементов из массива
        {
            foreach (var smth in a)
            {
                Add(smth);
            }
        }
        public void Clear() // 6. метод для очистки массива
        {
            size = 0;
        }
        public bool Contains(object o) // 7. метод для проверки находится ли указанный элемента в массиве
        {
            for (int i = 0; i < size; i++)
            {
                if (elementData[i].Equals(o)) return true;
            }
            return false;
        }
        public bool ContainsAll(T[] a) // 8. метод для проверки содержатся ли указанные объекты в динамическом массиве
        {
            foreach (var smth in a)
            {
                if (!Contains(smth)) return false;
            }

            return true;
        }
        public bool isEmpty() // 9 метод для проверки пустой ли массив
        {
            if (size == 0) return true;

            else return false;
        }
        public void Remove(object o) // 10. метод для удаления указанного объектов из массива
        {
            for (int i = 0; i < size; i++)

                if (Equals(elementData[i], o))
                {
                    for (int j = i; j < size - 1; j++)

                        elementData[j] = elementData[j + 1];

                    size--;
                }
        }
        public void RemoveAll(T[] a) // 11. метод для удаления  указанных объектов из массива
        {
            for (int k = 0; k < a.Length; k++)
            {
                Remove(a[k]);
            }
        }
        public void RetainAll(T[] a) // 12. метод для оставления в массиве только указанных объектов
        {
            int newSize = 0;
            T[] newElementData = new T[size];

            for (int k = 0; k < a.Length; k++)
            {
                for (int i = 0; i < size; i++)

                    if (Equals(a[k], elementData[i]))
                    {
                        newElementData[newSize] = a[k];
                        newSize++;
                    }
            }
            size = newSize;
            elementData = newElementData;
        }
        public int Size() // 13. метод для получения размера массива
        {
            return size;
        }
        public T[] ToArray() // 14. метод для возвращения массива объектов, содержащего все элементы массива
        {
            T[] newArray = new T[size];

            for (int i = 0; i < size; i++)
            {
                newArray[i] = elementData[i];
            }
            return newArray;
        }
        public T[] ToArray(T[] a) // 15. метод для возвращения массива объектов, содержащего все элементы массива, если элемент null, то создается новый
        {
            if (a == null || a.Length < size) return ToArray();

            for (int i = 0; i < size; i++) a[i] = elementData[i];
            return a;
        }
        public void Add(int index, T e) // 16. метод для добавления элемента в указанную позицию
        {
            if (index > size) { Add(e); return; }

            T[] NewElementData = new T[size + 1];
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            NewElementData[index] = e;
            for (int i = index + 1; i < size; i++)
            {
                NewElementData[i] = elementData[i - 1];
            }
            elementData = NewElementData;
            size++;
        }
        public void AddAll(int index, T[] a) // 17. метод для добавления элементов в указанную позицию
        {
            if (index > size) { AddAll(a); return; }
            T[] NewElementData = new T[size + a.Length];
            for (int i = 0; i < index; i++)
            {
                NewElementData[i] = elementData[i];
            }
            for (int i = 0; i < a.Length; i++)
            {
                NewElementData[i + index] = a[i];
            }
            for (int i = index; i < size; i++)
            {
                NewElementData[i + index] = elementData[i];
            }
            elementData = NewElementData;
            size += a.Length;
        }
        public T Get(int index) // 18. метод для возвращения элемента в указанную позицию
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return elementData[index];
        }
        public int IndexOf(object o) // 19. метод для возвращения индекса указанного объекта
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOf(object o) // 20. метод для нахождения последнего вхождения указанного объекта
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (o.Equals(elementData[i])) return i;
            }
            return -1;
        }
        public T Remove(int index) // 21. метод для удаления и возвращения жлемента в укаазанной позиции
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("index");

            T element = elementData[index];

            for (int i = index + 1; i < size; i++)
            {
                elementData[i - 1] = elementData[i];
            }
            size--;
            return element;
        }
        public void Set(int index, T e) // 22. метод для замены элемента в указанной позиции новым
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("index");

            elementData[index] = e;
        }
        public T[] SubList(int fromindex, int toindex) //23. метод для возвращения части днамичесокго массива
        {
            if ((fromindex < 0 || fromindex >= size) || (toindex < 0 || toindex >= size)) throw new ArgumentOutOfRangeException("fromindex", "toindex");

            T[] Result = new T[toindex - fromindex];
            for (int i = fromindex; i < toindex; i++)
                Result[i - fromindex] = elementData[i];
            return Result;
        }
    }
}
