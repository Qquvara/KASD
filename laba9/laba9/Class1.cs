using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyVector;

namespace MyStack
{
    public class MyStack<T> : MyVector<T>
    {
        public MyStack() : base()
        {
        }
        public void Push(T item)
        {
            Add(item);
        }
        public T Pop()
        {
            if (elmCount == 0) throw new ArgumentOutOfRangeException("Stack is empty");
            return Remove(elmCount - 1);
        }
        public T Peek()
        {
            if (elmCount == 0) throw new ArgumentOutOfRangeException("Stack is empty");
            return LastElement();
        }
        public bool Empty()
        {
            return IsEmpty();
        }
        public int Search(T item)
        {
            if (IndexOF(item) == -1) return -1;
            return elmCount - IndexOF(item);
        }
    }
}

