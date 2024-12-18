using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPriorityQueue
{
    public class MyPriorityQueue<T>
    {
        protected T[] queue; // Массив для хранения элементов очереди
        protected int size; // Количество элементов в очереди
        protected IComparer<T> comparator; // Компаратор для сравнения элементов

        // 1. Конструктор по умолчанию
        public MyPriorityQueue() : this(11, Comparer<T>.Default) { }

        // 2. Конструктор с массивом
        public MyPriorityQueue(T[] a) : this(a.Length)
        {
            AddAll(a);
        }

        // 3. Конструктор с начальной ёмкостью
        public MyPriorityQueue(int initialCapacity) : this(initialCapacity, Comparer<T>.Default) { }

        // 4. Конструктор с ёмкостью и компаратором
        public MyPriorityQueue(int initialCapacity, IComparer<T> comparator)
        {
            this.queue = new T[initialCapacity];
            this.size = 0;
            this.comparator = comparator;
        }

        // 5. Метод для добавления элемента
        public void Add(T e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e)); // Проверка на null
            if (size >= queue.Length)
            {
                EnsureCapacity();
            }
            queue[size++] = e;
            HeapifyUp(size - 1);
        }

        // 6. Метод для добавления всех элементов из массива
        public void AddAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a)); // Проверка на null
            foreach (var item in a)
            {
                Add(item);
            }
        }

        // 7. Метод для очистки очереди
        public void Clear()
        {
            Array.Clear(queue, 0, size);
            size = 0;
        }

        // 8. Метод для проверки наличия элемента в очереди
        public bool Contains(T o)
        {
            for (int i = 0; i < size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(queue[i], o))
                {
                    return true; // Элемент найден
                }
            }
            return false; // Элемент не найден
        }

        // 9. Метод для проверки наличия всех указанных элементов
        public bool ContainsAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a)); // Проверка на null
            foreach (var item in a)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        // 10. Метод для проверки, пуста ли очередь
        public bool IsEmpty()
        {
            return size == 0;
        }

        // 11. Метод для удаления указанного элемента
        public bool Remove(T o)
        {
            for (int i = 0; i < size; i++)
            {
                if (EqualityComparer<T>.Default.Equals(queue[i], o))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false; // Элемент не найден
        }

        // 12. Метод для удаления всех указанных элементов
        public void RemoveAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a)); // Проверка на null
            foreach (var item in a)
            {
                Remove(item);
            }
        }

        // 13. Метод для оставления только указанных элементов
        public void RetainAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a)); // Проверка на null
            HashSet<T> set = new HashSet<T>(a);
            for (int i = 0; i < size; i++)
            {
                if (!set.Contains(queue[i]))
                {
                    RemoveAt(i);
                    i--; // Уменьшаем индекс, чтобы не пропустить следующий элемент
                }
            }
        }

        // 14. Метод восстановления кучи после добавления
        private void HeapifyUp(int index)
        {
            int parentIndex;
            T temp = queue[index];
            while (index > 0)
            {
                parentIndex = (index - 1) / 2;
                if (comparator.Compare(queue[parentIndex], temp) <= 0)
                    break;

                queue[index] = queue[parentIndex];
                index = parentIndex;
            }
            queue[index] = temp;
        }

        // 15. Метод для обеспечения достаточной ёмкости
        private void EnsureCapacity()
        {

            int newCapacity = queue.Length < 64 ? queue.Length + 2 : (int)(queue.Length * 1.5);
            Array.Resize(ref queue, newCapacity);
        }

        // 16. Метод для удаления элемента по индексу
        private void RemoveAt(int index)
        {
            // Реализация удаления элемента и восстановления порядка
            queue[index] = queue[--size];
            queue[size] = default; // для удаления ссылки
            HeapifyDown(index);
        }

        // 17. Метод для восстановления кучи после удаления
        private void HeapifyDown(int index)
        {
            int leftChild, rightChild, smallest;
            T temp = queue[index];

            while (index < size / 2) // пока есть хотя бы один потомок
            {
                leftChild = 2 * index + 1;
                rightChild = 2 * index + 2;

                smallest = leftChild;
                if (rightChild < size && comparator.Compare(queue[leftChild], queue[rightChild]) > 0)
                {
                    smallest = rightChild;
                }

                if (comparator.Compare(temp, queue[smallest]) <= 0)
                    break;

                queue[index] = queue[smallest];
                index = smallest;
            }
            queue[index] = temp;
        }

        // 18. Метод для получения размера очереди
        public int Size()
        {
            return size;
        }

        // 19. Метод для возврата массива с элементами
        public T[] ToArray()
        {
            T[] result = new T[size];
            Array.Copy(queue, result, size);
            return result;
        }

        // 20. Метод для возврата массива и создания нового, если передан null
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
            {
                return ToArray();
            }

            Array.Copy(queue, a, size);
            if (a.Length > size) a[size] = default; // Очистка лишних элементов
            return a;
        }

        // 21. Метод для получения элемента из головы очереди без удаления
        public T Element()
        {
            if (IsEmpty()) throw new InvalidOperationException("Очередь пуста.");
            return queue[0];
        }

        // 22. Метод для попытки добавления элемента
        public bool Offer(T obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 23. Метод для возврата элемента из головы без удаления (или null)
        public T Peek()
        {
            return IsEmpty() ? default : queue[0];
        }

        // 24. Метод для удаления и возврата элемента из головы (или null)
        public T Poll()
        {
            if (IsEmpty())
                return default;

            T result = queue[0];
            RemoveAt(0);
            return result;
        }
    }

}
