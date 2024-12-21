using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace MyArrayDeque
    {
        public class MyArrayDeque<T>
        {
            private T[] elements; // Массив для хранения элементов очереди 
            private int head;     // Индекс начала очереди 
            private int tail;     // Индекс конца очереди 

            // Конструктор по умолчанию, инициализирует массив фиксированного размера 
            public MyArrayDeque()
            {
                elements = new T[16];
                head = 0;
                tail = 0;
            }

            // Конструктор, инициализирующий очередь элементами из массива 
            public MyArrayDeque(T[] array)
            {
                elements = new T[array.Length];
                head = 0;
                int i = 0;
                foreach (T item in array)
                {
                    elements[i] = item;
                    i++;
                }
                tail = i;
            }

            // Конструктор, задающий размер очереди при инициализации 
            public MyArrayDeque(int numElements)
            {
                head = 0;
                tail = 0;
                if (numElements > 0)
                    elements = new T[numElements];
                else throw new Exception("Отрицательная емкость");
            }

            // Метод добавляет элемент в конец очереди 
            public void Add(T e)
            {
                if (tail == elements.Length - 1)
                {
                    T[] newElements = new T[elements.Length * 2];
                    for (int i = head; i < elements.Length; i++)
                        newElements[i] = elements[i];
                    elements = newElements;
                    elements[tail++] = e;
                }
                else elements[tail++] = e;
            }

            // Метод добавляет все элементы из указанного массива в очередь 
            public void AddAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                    Add(a[i]);
            }

            // Метод очищает очередь, сбрасывая индексы 
            public void Clear()
            {
                tail = 0;
                head = 0;
            }

            // Метод проверяет наличие элемента в очереди 
            public bool Contains(T o)
            {
                for (int i = head; i <= tail; i++)
                    if (Equals(elements[i], o)) return true;
                return false;
            }

            // Метод проверяет, содержатся ли все элементы из указанного массива в очереди 
            public bool ContainsAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                    if (!Contains(a[i])) return false;
                return true;
            }

            // Метод проверяет, пуста ли очередь 
            public bool IsEmpty() => tail == 0;

            // Метод удаления указанного элемента из очереди 
            public void Remove(T o)
            {
                if (Contains(o))
                {
                    int index = 0;
                    for (int i = head; i < tail; i++)
                    {
                        if (Equals(elements[i], o)) index = i;
                    }
                    if (index == head)
                    {
                        head++; return; // Удаление элемента из начала 
                    }
                    else if (index == tail)
                    {
                        tail--; return; // Удаление элемента из конца 
                    }
                    else
                    {
                        T[] newElements = new T[--tail];
                        for (int i = head; i < index; i++)
                            newElements[i] = elements[i];
                        for (int i = index; i < tail; i++)
                            newElements[i] = elements[i + 1];
                        elements = newElements;
                    }
                }
                else throw new Exception("Элемент отсутствует"); // Ошибка, если элемент не найден 
            }

            // Метод удаления всех указанных элементов из очереди 
            public void RemoveAll(T[] array)
            {
                for (int i = 0; i < array.Length; i++)
                    Remove(array[i]);
            }

            // Метод оставляет только те элементы, которые содержатся в указанном массиве 
            public void RetainAll(T[] array)
            {
                int newTail = 0;
                T[] newElements = new T[elements.Length];
                for (int i = 0; i < array.Length; i++)
                    for (int j = head; j <= tail; j++)
                        if (Equals(elements[j], array[i]))
                        {
                            newElements[newTail] = elements[j];
                            newTail++;
                        }
                elements = newElements;
                tail = newTail;
            }

            // Метод возвращает индекс начала очереди 
            public int IndexOfHead() => head;

            // Метод возвращает количество элементов в очереди 
            public int Size() => tail - head;

            // Метод преобразует очередь в массив 
            public T[] ToArray()
            {
                T[] array = new T[tail + 1];
                for (int i = 0; i <= tail; i++) array[i] = elements[i];
                return array;
            }

            // Метод копирует элементы очереди в указанный массив 
            public void ToArray(ref T[] array)
            {
                if (array == null) array = ToArray(); // Если массив пустой, создаем новый 
                Array.Copy(elements, array, tail + 1); // Копирование элементов 
            }

            // Метод возвращает элемент, находящийся в начале очереди 
            public T Element() => elements[head];

            // Метод добавляет элемент в очередь, если возможно
            public bool Offer(T obj)
            {
                try
                {
                    Add(obj);
                    return true; // Возвращает true, если добавление прошло успешно 
                }
                catch
                {
                    return false; // Возвращает false в случае ошибки при добавлении 
                }

                {
                    // Метод добавляет элемент и проверяет, был ли он добавлен, возвращая true или false 
                    Add(obj);
                    if (Contains(obj)) return true;
                    return false;
                }
            }
            // Метод возвращает первый элемент очереди, не удаляя его 
            public T Peek()
            {
                // Если очередь пуста, возвращает значение по умолчанию 
                if (tail == 0) return default(T);
                else return elements[head];
            }

            // Метод возвращает и удаляет первый элемент из очереди 
            public T Poll()
            {
                // Если очередь пуста, возвращает значение по умолчанию 
                if (tail == 0) return default(T);
                else
                {
                    T item = elements[head];
                    head++; // Обновление индекса начала очереди 
                    return item;
                }
            }

            // Метод добавляет элемент в начало очереди 
            public void AddFirst(T obj)
            {
                if (head == 0)
                {
                T[] newElements = new T[elements.Length + 1];
                newElements[head] = obj; // Установка нового элемента на начало 
                for (int i = head; i <= tail; i++)
                    newElements[i + 1] = elements[i]; // Сдвигаем остальные элементы 
                elements = newElements;
                tail++; // Обновляем индекс конца очереди
                }
                else {
                head--; // иначе назад
                elements[head] = obj; }
            }

            // Метод добавляет элемент в конец очереди 
            public void AddLast(T obj)
            {
                Add(obj); // Использует существующий метод Add для добавления в конец 
            }

            // Метод возвращает первый элемент без удаления 
            public T GetFirst() => elements[head];

            // Метод возвращает последний элемент 
            public T GetLast() => elements[tail];

            // Метод добавляет элемент в начало и проверяет его добавление 
            public bool OfferFirst(T obj)
            {
                AddFirst(obj);
                if (Contains(obj)) return true;
                else return false;
            }

            // Метод добавляет элемент в конец и проверяет его добавление 
            public bool OfferLast(T obj)
            {
                AddLast(obj);
                if (Contains(obj)) return true;
                else return false;
            }

            // Метод удаляет и возвращает первый элемент 
            public T Pop()
            {
                T item = elements[head];
                head++; // Обновление индекса начала 
                return item;
            }

            // Метод добавляет элемент в начало, имитируя стек 
            public void Push(T obj)
            {
                AddFirst(obj);
            }

            // Метод возвращает первый элемент без удаления 
            public T PeekFirst()
            {
                // Если очередь пуста, возвращает значение по умолчанию 
                if (tail == 0) return default(T);
                else return elements[head];
            }

            // Метод возвращает последний элемент без удаления 
            public T PeekLast()
            {
                // Если очередь пуста, возвращает значение по умолчанию 
                if (tail == 0) return default(T);
                else return elements[tail];
            }

            // Метод удаляет и возвращает первый элемент 
            public T PollFirst()
            {
                if (tail == 0) return default(T);
                else
                {
                    T item = elements[head];
                    head++; // Обновление индекса 
                    return item;
                }
            }

            // Метод удаляет и возвращает последний элемент 
            public T PollLast()
            {
                if (tail == 0) return default(T);
                else
                {
                    T item = elements[tail];
                    tail--; // Обновление индекса конца 
                    return item;
                }
            }

            // Метод удаляет и возвращает последний элемент 
            public T RemoveLast()
            {
                T item = elements[tail];
                tail--; // Обновление индекса конца 
                return item;
            }

            // Метод удаляет и возвращает первый элемент с помощью метода Pop 
            public T RemoveFirst()
            {
                return Pop();
            }

            // Метод удаляет последнее вхождение элемента 
            public bool RemoveLastOccurrence(T obj)
            {
                for (int i = tail; i >= head; i--)
                    if (Equals(elements[i], obj))
                    {
                        Remove(obj);
                        return true;
                    }
                return false;
            }

            // Метод удаляет первое вхождение элемента 
            public bool RemoveFirstOccurrence(T obj)
            {
                for (int i = head; i <= tail; i++)
                    if (Equals(elements[i], obj))
                    {
                        Remove(obj);
                        return true;
                    }
                return false;
            }

            // Метод возвращает элемент по указанному индексу 
            public T Get(int index)
            {
                return elements[index]; // Возвращает элемент по индексу 
            }
        }
    }

