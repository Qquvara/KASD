using System;
using System.Collections.Generic;
namespace MyHashMap
{


    public class MyHashMap<K, V>
    {
        // класс для представления записи в хеш-таблице
        private class Entry
        {
            public K Key; 
            public V Value;  
            public Entry Next; 

            public Entry(K key, V value)
            {
                Key = key;
                Value = value;
                Next = null; 
            }
        }

        private Entry[] table; 
        private int size; 
        private int threshold;
        private float loadFactor; 

        // Конструктор хеш-таблицы с заданной начальной емкостью и коэффициентом загрузки
        public MyHashMap(int initialCapacity = 16, float loadFactor = 0.75f)
        {
            this.loadFactor = loadFactor;
            table = new Entry[initialCapacity]; // Инициализируем массив записей
            threshold = (int)(initialCapacity * loadFactor); // Устанавливаем порог загрузки
            size = 0; // Изначально размер равен нулю
        }

        // Метод для получения значения по ключу
        public V Get(K key)
        {
            int index = GetIndex(key); // Получаем индекс для данного ключа
            Entry entry = table[index]; // Получаем первую запись в корзине по индексу

            
            while (entry != null)
            {
                
                if (EqualityComparer<K>.Default.Equals(entry.Key, key))
                    return entry.Value; 

                entry = entry.Next; // Переходим к следующей записи
            }

            return default(V); 
        }

        // Метод для проверки, пустая ли хеш-таблица
        public bool IsEmpty()
        {
            return size == 0; 
        }

        // Метод для получения множества всех ключей в хеш-таблице
        public ICollection<K> KeySet()
        {
            var keySet = new HashSet<K>(); // Создаем множество для хранения ключей
            foreach (var entry in table) 
            {
                var current = entry; // Начинаем с текущей записи
                while (current != null) 
                {
                    keySet.Add(current.Key); // Добавляем ключ в множество
                    current = current.Next; // Переходим к следующему элементу
                }
            }
            return keySet; 
        }

        // Метод для добавления или обновления значения по ключу
        public V Put(K key, V value)
        {
            // Проверяем, нужно ли увеличить размер хеш-таблицы
            if (size >= threshold)
                Resize();

            int index = GetIndex(key); 
            Entry entry = table[index]; // Получаем первую запись в корзине

            // Проходим по связанному списку в данной корзине
            while (entry != null)
            {
                // Проверяем, совпадает ли ключ текущей записи с искомым ключом
                if (EqualityComparer<K>.Default.Equals(entry.Key, key))
                {
                    V oldValue = entry.Value; // Сохраняем старое значение
                    entry.Value = value; 
                    return oldValue; 
                }
                entry = entry.Next; // Переходим к следующей записи
            }

            // Добавляем новый элемент в корзину
            var newEntry = new Entry(key, value) { Next = table[index] }; // Создаем новую запись и устанавливаем ее следующим элементом
            table[index] = newEntry; 
            size++;
            return default(V); 
        }

        // Метод для удаления значения по ключу и возвращения его значения
        public V Remove(K key)
        {
            int index = GetIndex(key); 
            Entry entry = table[index];
            Entry previous = null; 

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
                    size--;
                    return entry.Value; 
                }
                previous = entry; // Обновляем ссылку на предыдущий элемент
                entry = entry.Next; 
            }

            return default(V); 
        }

        // Метод для получения текущего размера хеш-таблицы
        public int Size()
        {
            return size; 
        }

        // Метод для получения индекса в хеш-таблице по ключу
        private int GetIndex(K key)
        {
            return key == null ? 0 : Math.Abs(key.GetHashCode()) % table.Length;
        }

        // Метод для увеличения размера хеш-таблицы при превышении порога загрузки
        private void Resize()
        {
            int newCapacity = table.Length * 2; 
            var newTable = new Entry[newCapacity]; 

            foreach (var entry in table) 
            {
                var current = entry;
                while (current != null) // Проходим по связанному списку в корзине
                {
                    int newIndex = Math.Abs(current.Key.GetHashCode()) % newCapacity; // Вычисляем новый индекс для элемента

                    var nextEntry = current.Next; // Сохраняем ссылку на следующий элемент
                    current.Next = newTable[newIndex];
                    newTable[newIndex] = current;

                    current = nextEntry; // Переходим к следующему элементу
                }
            }

            table = newTable; // Обновляем ссылку на таблицу на новую таблицу
            threshold = (int)(newCapacity * loadFactor); // Пересчитываем порог загрузки
        }
    }
}