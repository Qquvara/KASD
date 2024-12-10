using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStack
{
 
        public class MyVector<T>
        {
            protected T[] elmData;
            protected int elmCount;
            protected int capacityIncrement;

            public MyVector(int initialCapacity, int CapacityIncrement) // создание пустого вектора с начальной емкостью и значением приращения емкости
            {
                elmData = new T[initialCapacity];
                capacityIncrement = CapacityIncrement;
                elmCount = 0;
            }
            public MyVector(int initialCapacity) // конструктор для создания пустого вектора с емкостью и значением приращения емкости = 0
            {
                elmData = new T[initialCapacity];
                elmCount = 0;
                capacityIncrement = 0;
            }
            public MyVector() // для создания пустого вектора с начальной емкостью = 10 и значением приращения емкости = 0
            {
                elmData = new T[10];
                elmCount = 0;
                capacityIncrement = 0;
            }
            public MyVector(T[] a) //  для создания вектора и заполнения его элементами из передаваемого массива а
            {
                elmData = new T[a.Length];
                for (int i = 0; i < a.Length; i++)

                {
                    elmData[i] = a[i];
                }
                elmCount = a.Length;
                capacityIncrement = 0;
            }
            public void Add(T e) // метод для добавления элемента в конец вектора
            {
                if (elmCount == elmData.Length)
                {
                    T[] newElmData;
                    if (capacityIncrement == 0)

                    {
                        newElmData = new T[elmData.Length * 2];
                    }
                    else newElmData = new T[elmData.Length + capacityIncrement];

                    for (int i = 0; i < elmCount; i++)
                        newElmData[i] = elmData[i];
                    elmData = newElmData;
                }
                elmData[elmCount] = e;
                elmCount++;
            }
            public void AddAll(T[] a) // метод для добавления элементов из массива
            {
                for (int i = 0; i < a.Length; i++)
                    Add(a[i]);
            }
            public void Clear() // для удаления элементов из вектора ( всех )
            {
                elmCount = 0;
            }
            public bool Contains(object o) //
            {
                for (int i = 0; i < elmCount; i++)
                    if (Equals(elmData[i], o)) return true;
                return false;
            }
            public bool ContainsAll(T[] a) // для проверка, содержатся ли указанные объекты в векторе
            {
                for (int i = 0; i < a.Length; i++)
                    if (!Contains(a[i])) return false;
                return true;
            }
            public bool IsEmpty() // для проверки, является ли вектор пустым
            {
                return elmCount == 0;
            }
            public void Remove(object o) //
            {
                for (int i = 0; i < elmCount; i++)
                    if (Equals(elmData[i], o))
                    {
                        for (int j = i; j < elmCount - 1; j++)
                        {
                            elmData[i] = elmData[j + 1];
                        }
                        elmCount--;
                    }
            }
            public void RemoveAll(T[] a) // для удаления указанных объектов из вектора
            {
                for (int i = 0; i < a.Length; i++)
                    Remove(a[i]);
            }
            public void RetainAll(T[] a) // для оставления в векторе только указанные объекты
            {
                int newElmCount = 0;
                T[] newElmData = new T[elmCount];
                for (int i = 0; i < a.Length; i++)
                    for (int j = 0; j < elmCount; j++)
                        if (Equals(elmData[j], a[i]))
                        {
                            newElmData[newElmCount] = elmData[j];
                            newElmCount++;
                        }
                elmData = newElmData;
                elmCount = newElmCount;
            }
            public int Size() // для получения размера вектора в элементах
            {
                return elmCount;
            }
            public T[] ToArray() // для возвращения массива объектов, содержащего элементы вектора
            {
                T[] array = new T[elmCount];
                for (int i = 0; i < elmCount; i++) array[i] = elmData[i];
                return array;
            }
            public void ToArray(ref T[] a) // для возвращения массива объектов, содержащего элементы вектора, если null создается новый массив
            {
                if (a == null) a = ToArray();
                Array.Copy(elmData, a, elmCount);
            }
            public void Add(int index, T e) // для добавления элемента в указанную позицию
            {
                if (index > elmCount) { Add(e); return; }
                T[] NewElmData = new T[elmData.Length];
                if (elmCount == elmData.Length)
                {
                    if (capacityIncrement == 0) NewElmData = new T[elmCount * 2];
                    else NewElmData = new T[elmCount + capacityIncrement];
                }
                for (int i = 0; i < index; i++)
                {
                    NewElmData[i] = elmData[i];
                }
                NewElmData[index] = e;
                for (int i = index + 1; i < elmCount; i++)
                {
                    NewElmData[i] = elmData[i - 1];
                }
                elmData = NewElmData;
                elmCount++;
            }
            public void AddAll(int index, T[] a) // для добавления элементов в указанную позицию
            {
                if (index > elmCount) { AddAll(a); return; }
                T[] newElmData = new T[elmData.Length];
                while (newElmData.Length - a.Length < elmCount)
                {
                    if (capacityIncrement == 0) newElmData = new T[newElmData.Length * 2];
                    else newElmData = new T[newElmData.Length + capacityIncrement];
                }
                for (int i = 0; i < index; i++)
                {
                    newElmData[i] = elmData[i];
                }
                for (int i = 0; i < a.Length; i++)
                {
                    newElmData[i + index] = a[i];
                }
                for (int i = index; i < elmCount; i++)
                {
                    newElmData[i + index] = elmData[i];
                }
                elmData = newElmData;
                elmCount += a.Length;
            }
            public T Get(int index) // для возвращения элемента в указанной позиции. 
            {
                return elmData[index];
            }
            public int IndexOF(object o) // для возвращения индекса указанного объекта, или -1, если его нет в векторе
            {
                for (int i = 0; i < elmCount; i++)
                    if (Equals(elmData[i], o)) return i;
                return -1;
            }
            public int LastIndexOf(object o) // для нахождения последнего вхождения указанного объекта, или -1, если его нет в векторе
            {
                for (int i = elmCount - 1; i >= 0; i--)
                    if (Equals(elmData[i], o)) return i;
                return -1;
            }
            public T Remove(int index) // для удаления и возвращения элемента в указанной позиции
            {
                if (index < 0 || index > elmCount)
                    throw new ArgumentOutOfRangeException("index");
                Remove(elmData[index]);
                return elmData[index];
            }
            public void Set(int index, T e) // для замены элемента в указанной позиции новым элементом
            {
                if (index < 0 || index > elmCount)
                    throw new ArgumentOutOfRangeException("index");
                elmData[index] = e;
            }
            public T[] SubList(int fromindex, int toindex) // для возвращения части вектора ( элементов в диапазоне )
            {
                if ((fromindex < 0 || fromindex > elmCount) || (toindex < 0 || toindex > elmCount))
                    throw new ArgumentOutOfRangeException("fromindex", "toindex");
                T[] Result = new T[toindex - fromindex];
                for (int i = fromindex; i < toindex; i++)
                    Result[i - fromindex] = elmData[i];
                return Result;
            }
            public T FirstElement() { return elmData[0]; } // для обращения к first элементу
            public T LastElement() { return elmData[elmCount - 1]; } // для обращения к last элементу
            public void RemoveElementAt(int pos) // для удаления элемента в заданной позиции
            {
                if (pos < 0 || pos > elmCount)
                    throw new ArgumentOutOfRangeException("index");
                Remove(elmData[pos]);
            }
            public void RemoveRange(int begin, int end) // для удаления нескольких подряд идущих элементов
            {
                if ((begin < 0 || begin > elmCount) || (end < 0 || end > elmCount))
                    throw new ArgumentOutOfRangeException("begin", "end");
                T[] newArray = new T[end - begin + 1];
                int index = 0;
                for (int i = begin; i < end; i++)
                {
                    newArray[index] = elmData[i];
                    index++;
                }
                RemoveAll(newArray); // для  удаления всех объектов
            }
        }
    }
