using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

public class MyLinkedList<T> 
{
    private class Node // класс узла 
    {
        public T Value; // значение
        public Node Next; // следующее
        public Node Prev; // предыдущее

        public Node(T value) // конструктор инициализации
        {
            Value = value;
        }
    }

    private Node first;
    private Node last;
    private int size;

    public MyLinkedList() // конструтктор дял создания списка
    {
        first = null;
        last = null;
        size = 0;
    }

    public MyLinkedList(T[] a) : this() // метод для заполения его элементами из массива a
    {
        AddAll(a);
    }

    public void Add(T e) // добавления элемента в конец списка
    {
        Node newNode = new Node(e);
        if (last == null)
        {
            first = last = newNode; // Если список пуст, новый узел становится первым и последним
        }
        else
        {
            last.Next = newNode; // Связываем последний узел с новым
            newNode.Prev = last; // Связываем новый узел с последним
            last = newNode; // Обновляем указатель на последний узел
        }
        size++;
    }

    public void AddAll(T[] a) // добавление всех эл-ов
    {
        foreach (var item in a)
        {
            Add(item);
        }
    }

    public void Clear() // удаление всех 
    {
        first = last = null;
        size = 0;
    }

    public bool Contains(object o) // проверка наличия объекта в списке
    {
        Node current = first;
        while (current != null)
        {
            if (current.Value.Equals(o))
                return true;
            current = current.Next;
        }
        return false;
    }

    public bool ContainsAll(T[] a) // проверка наличия всех обхектов в списке
    {
        foreach (var item in a)
        {
            if (!Contains(item))
                return false;
        }
        return true;
    }

    public bool IsEmpty() // проверка пустоты двунаправленного списка
    {
        return size == 0;
    }

    public bool Remove(object o) // удаление объекта
    {
        Node current = first;
        while (current != null)
        {
            if (current.Value.Equals(o))
            {
                if (current.Prev != null) current.Prev.Next = current.Next; // Связываем предыдущий узел с следующим
                if (current.Next != null) current.Next.Prev = current.Prev; // Связываем следующий узел с предыдущим

                if (current == first) first = current.Next; // Если удаляем первый элемент, обновляем указатель на первый узел
                if (current == last) last = current.Prev; // Если удаляем последний элемент, обновляем указатель на последний узел

                size--;
                return true; 
            }
            current = current.Next; // Переходим к следующему узлу
        }
        return false; 
    }

    public void RemoveAll(T[] a) // удаление всех указанных объектов
    {
        foreach (var item in a)
        {
            Remove(item);
        }
    }

    public void RetainAll(T[] a) // для оставления в списке только указанных объектов
    {
        HashSet<T> set = new HashSet<T>(a);
        Node current = first;

        while (current != null)
        {
            if (!set.Contains(current.Value))
            {
                Remove(current.Value); // Удаляем элемент, если его нет в множестве
            }
            current = current.Next;
        }
    }

    public int Size() // для получения размера списка в элементах
    {
        return size;
    }

    public T[] ToArray() // для возвращения массива объектов, содержащего все элементы списка
    {
        T[] array = new T[size];
        Node current = first;
        for (int i = 0; i < size; i++)
        {
            array[i] = current.Value;
            current = current.Next;
        }
        return array;
    }

    public T[] ToArray(T[] a) // для возвращения массива объектов, содержащего все элементы списка с учетом не определенности объекта (null)
    {
        if (a == null || a.Length < size)
            a = new T[size];

        Node current = first;
        for (int i = 0; i < size; i++)
        {
            a[i] = current.Value;
            current = current.Next;
        }

        return a;
    }

    public void Add(int index, T e) // для добавления элемента в указанную позицию
    {
        if (index < 0 || index > size) throw new ArgumentOutOfRangeException();

        if (index == size)
        {
            Add(e); // Добавляем в конец
            return;
        }

        Node newNode = new Node(e);

        if (index == 0)
        {
            newNode.Next = first; // Связываем новый узел с первым
            if (first != null) first.Prev = newNode; // Обновляем предыдущий указатель у первого
            first = newNode; // Обновляем указатель на первый узел
            if (last == null) last = newNode; // Если список был пуст, обновляем указатель на последний узел
            size++;
            return;
        }

        Node current = GetNode(index - 1);

        newNode.Next = current.Next; // Связываем новый узел с следующим
        newNode.Prev = current; // Связываем новый узел с текущим
        if (current.Next != null) current.Next.Prev = newNode; // Обновляем предыдущий указатель у следующего узла
        current.Next = newNode; // Связываем текущий узел с новым

        if (newNode.Next == null) last = newNode; // Если добавили в конец, обновляем указатель на последний узел

        size++;
    }

    public void AddAll(int index, T[] a) // для добавления элементов в указанную позицию
    {
        foreach (var item in a)
        {
            Add(index++, item);
        }
    }

    public T Get(int index) //  для возвращения элемента в указанной позиции
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();

        return GetNode(index).Value;
    }

    private Node GetNode(int index) // получения узла из двусвязного списка по заданному индексу
    {
        Node current;

        if (index < (size / 2))
        {
            current = first;
            for (int i = 0; i < index; i++)
                current = current.Next;
        }
        else
        {
            current = last;
            for (int i = size - 1; i > index; i--)
                current = current.Prev;
        }

        return current;
    }

    public int IndexOf(object o) // для возвращения индекса указанного объекта 
    {
        Node current = first;

        for (int index = 0; current != null; index++)
        {
            if (current.Value.Equals(o)) return index;
            current = current.Next;
        }

        return -1; // если его нет
    }

    public int LastIndexOf(object o) // для нахождения последнего вхождения объекта
    {
        Node current = last;

        for (int index = size - 1; current != null; index--)
        {
            if (current.Value.Equals(o)) return index;
            current = current.Prev;
        }

        return -1; 
    }

    public T Remove(int index) // для удаления и возвращения элемента 
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();

        Node toRemove = GetNode(index);

        T valueToReturn = toRemove.Value;

        if (toRemove.Prev != null) toRemove.Prev.Next = toRemove.Next; // Связываем предыдущий узел с следующим
        if (toRemove.Next != null) toRemove.Next.Prev = toRemove.Prev; // Связываем следующий узел с предыдущим

        if (toRemove == first) first = toRemove.Next; // Если удаляем первый элемент, обновляем указатель на первый узел
        if (toRemove == last) last = toRemove.Prev; // Если удаляем последний элемент, обновляем указатель на последний узел

        size--;

        return valueToReturn;
    }

    public void Set(int index, T e) // для замены элемента в указанной позиции новым элементов
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException();

        GetNode(index).Value = e; // Заменяем значение узла
    }

    public MyLinkedList<T> SubList(int fromIndex, int toIndex) // для возвращении части списка
    {
        if (fromIndex < 0 || toIndex > size || fromIndex > toIndex) throw new ArgumentOutOfRangeException();

        MyLinkedList<T> sublist = new MyLinkedList<T>();

        for (int i = fromIndex; i < toIndex; i++)
            sublist.Add(Get(i));

        return sublist;
    }

    public T Element() // для возвращения элемента из головы списка без удаления
    {
        if (first == null) throw new InvalidOperationException("List is empty");

        return first.Value; 
    }

    public bool Offer(T obj) // для попытки добавления элемента в список
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

    public T Peek() // для возврата элемента из головы без удаления
    {
        return first != null ? first.Value : default; 
    }

    public T Poll() // для удаления и возврата элемента 
    {
        if (first == null) return default; 

        T valueToReturn = first.Value; // Запоминаем значение первого узла
        Remove(0); // Удаляем первый узел
        return valueToReturn; 
    }

    public void AddFirst(T obj) // для добавления в голову
    {
        Add(0, obj); // Добавляем в начало списка
    }

    public void AddLast(T obj) // для добавление в хвост
    {
        Add(obj); // Добавляем в конец списка
    }

    public T GetFirst() // первый эл
    {
        return Get(0); // Возвращаем первый элемент без удаления
    }

    public T GetLast() // последний эл
    {
        return Get(size - 1); // Возвращаем последний элемент без удаления
    }

    public bool OfferFirst(T obj) // для попытки добалвления в голову
    {
        try
        {
            AddFirst(obj);
            return true;
        }
        catch
        {
            return false; 
        }
    }

    public bool OfferLast(T obj) // для попытки добавления в хвост
    {
        try
        {
            AddLast(obj);
            return true; 
        }
        catch
        {
            return false; 
        }
    }

    public T Rop() // 
    {
        return Poll(); // Удаляем и возвращаем элемент из головы списка
    }

    public void Push(T obj) //
    {
        AddFirst(obj); // Добавляем элемент в голову списка
    }

public T PeekFirst() // возвращение элемента из головы
{
    return first != null ? first.Value : default(T);
}


public T PeekLast() // для возвращения из хвоста
{
    return last != null ? last.Value : default(T);
}


public T PollFirst() // 
{
    return Poll(); // Используем уже реализованный метод Poll для получения и удаления первого элемента.
}


public T PollLast() //
{
    if (last == null) return default(T); // Если список пуст, возвращаем null.

    T valueToReturn = last.Value;

    Remove(size - 1); // Удаляем последний узел.

    return valueToReturn;
}


public T RemoveFirst() //
{
    return Remove(0); // Используем уже реализованный метод Remove для удаления первого элемента.
}


public T RemoveLast() // Используем уже реализованный метод Remove для удаления ласт элемента.
    {
    return Remove(size - 1); 
}


public bool RemoveLastOccurrence(object obj) // для удаления последнего вхождения из списка
{
    for (Node current = last; current != null; current = current.Prev)
    {
        if (current.Value.Equals(obj))
        {
            Remove(current); // Удаляем текущее вхождение.
            return true;
        }
    }
    return false;
}


public bool RemoveFirstOccurrence(object obj) // для удаления  первого вхождения из списка
    {
    for (Node current = first; current != null; current = current.Next)
    {
        if (current.Value.Equals(obj))
        {
            Remove(current); // Удаляем текущее вхождение.
            return true; 
        }
    }
    return false; 
}
}
public class programm
{
    public static void Main(string[] args)
    {
        // Создаем новый связный список
        MyLinkedList<int> list = new MyLinkedList<int>();

        // Добавляем элементы
        list.Add(1);
        list.Add(2);
        list.Add(3);

        // Выводим элементы списка
        Console.WriteLine("Элементы списка:");
        foreach (var item in list.ToArray())
        {
            Console.WriteLine(item);
        }

        // Проверка размера списка
        Console.WriteLine($"Размер списка: {list.Size()}"); // Ожидаем: 3

        // Удаляем элемент
        list.Remove(2);
        Console.WriteLine("После удаления элемента 2:");
        foreach (var item in list.ToArray())
        {
            Console.WriteLine(item);
        }

        // Проверка размера списка после удаления
        Console.WriteLine($"Размер списка: {list.Size()}"); // Ожидаем: 2
    }
}