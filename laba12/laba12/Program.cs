using System;
using System.Collections.Generic;
using System.IO;
using MyPriorityQueue;
public class laba12
{
    public static void Main()
    {
        // Шаг 1: Ввод количества шагов
        Console.Write("Введите количество шагов добавления заявок (N): ");
        int N = int.Parse(Console.ReadLine());

        MyPriorityQueue<PriorityRequest> queue = new MyPriorityQueue<PriorityRequest>();
        int requestNumber = 1;
        int maxWaitTime = 0;
        PriorityRequest maxWaitRequest = null;
        string logFilePath = "Z:/log.txt";

        using (StreamWriter logFile = new StreamWriter(logFilePath))
        {
            // Шаг 2: Генерация и обработка заявок
            Random random = new Random();

            for (int step = 1; step <= N; step++)
            {
                int numRequests = random.Next(1, 11); // Генерация от 1 до 10 заявок
                for (int i = 0; i < numRequests; i++)
                {
                    int priority = random.Next(1, 6); // Приоритет от 1 до 5
                    PriorityRequest request = new PriorityRequest(requestNumber, priority, step);

                    // Добавление заявки в очередь и запись в лог
                    queue.Add(request);
                    logFile.WriteLine($"ADD {request.Number} {request.Priority} {step}");

                    requestNumber++;
                }

                // Удаление заявки с наивысшим приоритетом
                if (!queue.IsEmpty())
                {
                    PriorityRequest removedRequest = queue.Poll();
                    int waitTime = step - removedRequest.Step; // Время ожидания
                    logFile.WriteLine($"REMOVE {removedRequest.Number} {removedRequest.Priority} {step}");

                    // Проверка на максимальное время ожидания
                    if (waitTime > maxWaitTime)
                    {
                        maxWaitTime = waitTime;
                        maxWaitRequest = removedRequest;
                    }
                }
            }

            // Шаг 3: Продолжение удаления заявок из очереди
            while (!queue.IsEmpty())
            {
                PriorityRequest removedRequest = queue.Poll();
                int waitTime = N + 1 - removedRequest.Step; // Время ожидания
                logFile.WriteLine($"REMOVE {removedRequest.Number} {removedRequest.Priority} {N + 1}");

                // Проверка на максимальное время ожидания
                if (waitTime > maxWaitTime)
                {

                    maxWaitTime = waitTime;
                    maxWaitRequest = removedRequest;
                }
            }
        }

        // Шаг 4: Вывод информации о заявке с максимальным временем ожидания
        if (maxWaitRequest != null)
        {
            Console.WriteLine($"Заявка с максимальным временем ожидания:");
            Console.WriteLine($"Номер заявки: {maxWaitRequest.Number}, Приоритет: {maxWaitRequest.Priority}, " +
                              $"Шаг поступления: {maxWaitRequest.Step}, Время ожидания: {maxWaitTime}");
        }
        else
        {
            Console.WriteLine("Не было заявок.");
        }
    }
}

// Класс для заявки с приоритетом
public class PriorityRequest : IComparable<PriorityRequest>
{
    public int Number { get; }
    public int Priority { get; }
    public int Step { get; }

    public PriorityRequest(int number, int priority, int step)
    {
        Number = number;
        Priority = priority;
        Step = step;
    }

    public int CompareTo(PriorityRequest other)
    {
        if (other == null) return 1;

        int priorityComparison = other.Priority.CompareTo(Priority);
        if (priorityComparison != 0)
            return priorityComparison;

        return Number.CompareTo(other.Number);
    }
}
