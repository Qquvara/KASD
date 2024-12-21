using MyArrayDeque;
using System;
using System.IO;

namespace Lab15
{
    class Program
    {
        // Метод для подсчета цифр в строке
        public static int CountDigit(string line)
        {
            int count = 0;
            foreach (char c in line)
            {
                if (char.IsDigit(c)) count++;
            }
            return count;
        }

        // Метод для подсчета пробелов в строке
        public static int CountSpace(string line)
        {
            int count = 0;
            foreach (char c in line)
            {
                if (c == ' ') count++;
            }
            return count;
        }

        static void Main(string[] args)
        {
            string file1 = "Z:/input15.txt"; // Путь к входному файлу
            string file2 = "Z:/sorted.txt"; // Путь к выходному файлу

            MyArrayDeque<string> deque = new MyArrayDeque<string>(); // Объявление deque вне using

            // Используем using для автоматического закрытия потока
            using (StreamReader sr = new StreamReader(file1))
            {
                string line;

                // Читаем первую строку и добавляем в дек
                if ((line = sr.ReadLine()) != null)
                {
                    deque.Add(line);
                }

                // Читаем остальные строки и добавляем их в дек в зависимости от количества цифр
                while ((line = sr.ReadLine()) != null)
                {
                    if (CountDigit(line) > CountDigit(deque.GetFirst()))
                    {
                        deque.AddLast(line); // Добавляем в конец
                    }
                    else
                    {
                        deque.AddFirst(line); // Добавляем в начало
                    }
                }
            } // Поток автоматически закрывается здесь

            // Записываем содержимое дека в выходной файл
            using (StreamWriter sw = new StreamWriter(file2))
            {
                for (int i = deque.IndexOfHead(); i < deque.Size(); i++)
                {
                    sw.WriteLine(deque.Get(i));
                }
            } // Поток автоматически закрывается здесь

            Console.WriteLine("Введите количество пробелов:");
            int N;
            while (!int.TryParse(Console.ReadLine(), out N)) // Проверка на корректный ввод числа
            {
                Console.WriteLine("Пожалуйста, введите корректное число!");
            }

            // Удаляем строки, где количество пробелов больше N
            for (int i = deque.IndexOfHead(); i < deque.Size(); i++)
            {
                if (CountSpace(deque.Get(i)) > N)
                {
                    deque.Remove(deque.Get(i));
                    i--; // Уменьшаем индекс, чтобы не пропустить следующую строку после удаления
                }
            }

            // Выводим оставшиеся строки на экран
            for (int i = deque.IndexOfHead(); i < deque.Size(); i++)
            {
                Console.WriteLine(deque.Get(i));
            }
        }
    }
}
