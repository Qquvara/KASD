using System;
using System.Collections.Generic;
namespace MyHashMap
{


    public class MyHashMap<K, V>
    {
        // Внутренний класс для представления записи (элемента) в хеш-таблице
        private class Entry
        {
            public K Key; // Ключ записи
            public V Value; // Значение записи
            public Entry Next; // Ссылка на следующую запись в случае коллизий

            public Entry(K key, V value)
            {
                Key = key;
                Value = value;
                Next = null; // Изначально следующая запись отсутствует
            }
        }

        private Entry[] table; // Массив для хранения записей (корзин)
        private int size; // Текущий размер хеш-таблицы (количество элементов)
        private int threshold; // Порог загрузки для увеличения размера таблицы
        private float loadFactor; // Коэффициент загрузки

        // Конструктор хеш-таблицы с заданной начальной емкостью и коэффициентом загрузки
        public MyHashMap(int initialCapacity = 16, float loadFactor = 0.75f)
        {
            this.loadFactor = loadFactor;
            table = new Entry[initialCapacity]; // Инициализируем массив записей
            threshold = (int)(initialCapacity * loadFactor); // Устанавливаем порог загрузки
            size = 0; // Изначально размер равен нулю
        }
        public bool ContainsKey(K key)
        {
            int index = Math.Abs(key.GetHashCode()) % table.Length;
            Entry step = table[index];
            while (step != null)
            {
                if (Equals(step.Key, key)) return true;
                step = step.Next;
            }
            return false;
        }
        public bool ContainsValue(V value)
        {
            for (int index = 0; index < table.Length; index++)
            {
                Entry step = table[index];
                while (step != null)
                {
                    if (Equals(step.Value, value)) return true;
                    step = step.Next;
                }
            }
            return false;
        }
        // Метод для получения значения по ключу
        public V Get(K key)
        {
            int index = GetIndex(key); // Получаем индекс для данного ключа
            Entry entry = table[index]; // Получаем первую запись в корзине по индексу

            // Проходим по связанному списку в данной корзине
            while (entry != null)
            {
                // Проверяем, совпадает ли ключ текущей записи с искомым ключом
                if (EqualityComparer<K>.Default.Equals(entry.Key, key))
                    return entry.Value; // Возвращаем значение, если ключ найден

                entry = entry.Next; // Переходим к следующей записи
            }

            return default(V); // Возвращаем значение по умолчанию для типа V, если ключ не найден
        }

        // Метод для проверки, пустая ли хеш-таблица
        public bool IsEmpty()
        {
            return size == 0; // Возвращаем true, если размер равен нулю
        }

        // Метод для получения множества всех ключей в хеш-таблице
        public ICollection<K> KeySet()
        {
            var keySet = new HashSet<K>(); // Создаем множество для хранения ключей
            foreach (var entry in table) // Проходим по всем записям в таблице
            {
                var current = entry; // Начинаем с текущей записи
                while (current != null) // Проходим по связанному списку в корзине
                {
                    keySet.Add(current.Key); // Добавляем ключ в множество
                    current = current.Next; // Переходим к следующему элементу
                }
            }
            return keySet; // Возвращаем множество ключей
        }

        // Метод для добавления или обновления значения по ключу
        public V Put(K key, V value)
        {
            // Проверяем, нужно ли увеличить размер хеш-таблицы
            if (size >= threshold)
                Resize();

            int index = GetIndex(key); // Получаем индекс для данного ключа
            Entry entry = table[index]; // Получаем первую запись в корзине

            // Проходим по связанному списку в данной корзине
            while (entry != null)
            {
                // Проверяем, совпадает ли ключ текущей записи с искомым ключом
                if (EqualityComparer<K>.Default.Equals(entry.Key, key))
                {
                    V oldValue = entry.Value; // Сохраняем старое значение
                    entry.Value = value; // Обновляем значение
                    return oldValue; // Возвращаем старое значение
                }
                entry = entry.Next; // Переходим к следующей записи
            }

            // Добавляем новый элемент в корзину
            var newEntry = new Entry(key, value) { Next = table[index] }; // Создаем новую запись и устанавливаем ее следующим элементом
            table[index] = newEntry; // Устанавливаем новую запись как первую в корзине
            size++; // Увеличиваем размер
            return default(V); // Возвращаем значение по умолчанию для типа V, так как это новый элемент
        }

        // Метод для удаления значения по ключу и возвращения его значения
        public V Remove(K key)
        {
            int index = GetIndex(key); // Получаем индекс для данного ключа
            Entry entry = table[index]; // Получаем первую запись в корзине
            Entry previous = null; // Ссылка на предыдущую запись

            // Проходим по связанному списку в данной корзине
            while (entry != null)
            {
                // Проверяем, совпадает ли ключ текущей записи с искомым ключом
                if (EqualityComparer<K>.Default.Equals(entry.Key, key))
                {
                    if (previous == null)
                    {
                        table[index] = entry.Next; // Удаляем первый элемент в корзине
                    }
                    else
                    {
                        previous.Next = entry.Next; // Удаляем элемент из связанного списка
                    }
                    size--; // Уменьшаем размер хеш-таблицы
                    return entry.Value; // Возвращаем удалённое значение
                }
                previous = entry; // Обновляем ссылку на предыдущий элемент
                entry = entry.Next; // Переходим к следующему элементу
            }

            return default(V); // Возвращаем значение по умолчанию для типа V, если ключ не найден
        }

        // Метод для получения текущего размера хеш-таблицы
        public int Size()
        {
            return size; // Возвращаем текущий размер хеш-таблицы
        }

        // Метод для получения индекса в хеш-таблице по ключу
        private int GetIndex(K key)
        {
            return key == null ? 0 : Math.Abs(key.GetHashCode()) % table.Length;
        }

        // Метод для увеличения размера хеш-таблицы при превышении порога загрузки
        private void Resize()
        {
            int newCapacity = table.Length * 2; // Увеличиваем емкость вдвое
            var newTable = new Entry[newCapacity]; // Создаем новую таблицу

            foreach (var entry in table) // Проходим по всем записям в старой таблице
            {
                var current = entry;
                while (current != null) // Проходим по связанному списку в корзине
                {
                    int newIndex = Math.Abs(current.Key.GetHashCode()) % newCapacity; // Вычисляем новый индекс для элемента

                    var nextEntry = current.Next; // Сохраняем ссылку на следующий элемент
                    current.Next = newTable[newIndex]; // Устанавливаем новый следующий элемент в новой таблице
                    newTable[newIndex] = current; // Добавляем элемент в новую таблицу

                    current = nextEntry; // Переходим к следующему элементу
                }
            }

            table = newTable; // Обновляем ссылку на таблицу на новую таблицу
            threshold = (int)(newCapacity * loadFactor); // Пересчитываем порог загрузки
        }
    }
}