using System;
using MyStack;
using MyVector;

namespace Lab9
{
    class Program
    {
            // Метод для определения приоритета операций
            static public int PriorityOp(string operation)
            {
                // Используем switch для проверки операции
                switch (operation)
                {
                    // приоритет 1
                    case "+":
                    case "-":
                        return 1;
                    // приоритет 2
                    case "*":
                    case "/":
                    case "//":
                        return 2;
                    // приоритет 3
                    case "^":
                        return 3;
                    // приоритет 4
                    case "sqrt": // Квадратный корень
                    case "ln": // Натуральный логарифм
                    case "cos": // Косинус
                    case "sin": // Синус
                    case "tg": // Тангенс
                    case "ctg": // Котангенс
                    case "abs": // Абсолютное значение
                    case "log": // Логарифм
                    case "min": // Минимум
                    case "max": // Максимум
                    case "mod": // Остаток от деления
                    case "exp": // Экспонента
                    case "trunc": // Убирает дробную часть числа
                    case "pi": // Константа π 
                        return 4;
                    default: return 0;
                }
            }


        static private double InitOp(string operation, params double[] x)
        {
            return operation switch
            {
                "+" when x.Length == 2 => x[0] + x[1],
                "-" when x.Length == 2 => x[0] - x[1],
                "*" when x.Length == 2 => x[0] * x[1],
                "/" when x.Length == 2 => x[0] / x[1],
                "//" when x.Length == 2 => Math.Floor(x[0] / x[1]),
                "^" when x.Length == 2 => Math.Pow(x[0], x[1]),
                "exp" when x.Length == 1 => Math.Exp(x[0]),
                "sqrt" when x.Length == 1 => Math.Sqrt(x[0]),
                "ln" when x.Length == 1 => Math.Log(x[0]),
                "log" when x.Length == 1 => Math.Log10(x[0]),
                "cos" when x.Length == 1 => Math.Cos(x[0]),
                "sin" when x.Length == 1 => Math.Sin(x[0]),
                "tg" when x.Length == 1 => Math.Tan(x[0]),
                "ctg" when x.Length == 1 => 1 / Math.Tan(x[0]),
                "abs" when x.Length == 1 => Math.Abs(x[0]),
                "min" when x.Length == 2 => x[0] < x[1] ? x[0] : x[1],
                "max" when x.Length == 2 => x[0] > x[1] ? x[0] : x[1],
                "mod" when x.Length == 2 => (int)x[1] % (int)x[0],
                "trunc" when x.Length == 1 => Math.Truncate(x[0]),
                "pi" when x.Length == 0 => Math.PI,
                _ => throw new Exception("Unknown operation or invalid parameters")
            };
        }

        // Статический приватный метод Tmp, который принимает строку выражения
        static private string Tmp(string expression)
        {
            MyVector<string> vectorOfItem = new MyVector<string>();


            for (int i = 0; i < expression.Length; i++)
            {
                string item = "";

                // Считываем последовательность буквенных символов
                while (i < expression.Length && Char.IsLetter(expression[i]))
                {
                    item += expression[i]; 
                    i++;
                }

             
                if (item.Length > 0)
                {
                    // Проверяем, является ли имя одним из предопределенных операторов/функций
                    switch (item)
                    {
                        // Если это одно из допустимых имен, ничего не делаем
                        case "sqrt":
                        case "ln":
                        case "cos":
                        case "sin":
                        case "tg":
                        case "ctg":
                        case "abs":
                        case "log":
                        case "min":
                        case "max":
                        case "mod":
                        case "exp":
                        case "trunc":
                        case "pi":
                            break;
                        // Иначе добавляем имя в вектор
                        default:
                            vectorOfItem.Add(item);
                            break;
                    }
                }
            }

        
            for (int i = 0; i < vectorOfItem.Size(); i++)
            {
                Console.WriteLine($"Input tmp {vectorOfItem.Get(i)}: "); // Выводим приглашение ко вводу
                                                                         // Заменяем найденный элемент на введенное значение
                expression = expression.Replace(vectorOfItem.Get(i), Console.ReadLine());
            }

        
            return expression;
        }


        static public MyVector<string> Polska(string expression)
        {
            // Преобразуем входное выражение с помощью метода Tmp
            expression = Tmp(expression);
            MyStack<string> stack = new MyStack<string>();
            MyVector<string> result = new MyVector<string>();

            // Массив базовых операций
            char[] basicOperations = new char[] { '+', '-', '*', '^', '/' };

            
            for (int i = 0; i < expression.Length; i++)
            {
                string number = ""; 

                
                if ((result.IsEmpty() && expression[i] == '-') || (i > 0 && expression[i] == '-' && expression[i - 1] == '('))
                {
                    number += expression[i]; 
                    i++;
                }

              
                while (i < expression.Length && (Char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number += expression[i]; 
                    i++;
                }

                
                if (number.Length > 0) { result.Add(number); }

                string func = ""; 
                                 
                while (i < expression.Length && Char.IsLetter(expression[i]))
                {
                    func += expression[i];
                    i++;
                }

                // Обработка специального случая для числа π
                if (func.Length > 0 && func == "pi")
                {
                    result.Add(Math.PI.ToString()); // Заменяем 'pi' на его числовое значение
                }
                else if (func.Length > 0)
                {
                    stack.Push(func);
                }

                // Обработка операторов
                if (i < expression.Length && basicOperations.Contains(expression[i]))
                {
                    if (stack.Empty())
                    {
                        stack.Push(expression[i].ToString()); 
                    }
                    else
                    {
                        
                        while (!stack.Empty() && (PriorityOp(stack.Peek()) > PriorityOp(expression[i].ToString())))
                        {
                            
                            string b = stack.Peek();
                            result.Add(b.ToString());
                            stack.Pop();
                        }
                       
                        stack.Push(expression[i].ToString());
                    }
                }
                else if (i < expression.Length && expression[i] == '(')
                {
                    stack.Push(expression[i].ToString()); 
                }
                else if (i < expression.Length && expression[i] == ')')
                {
                    
                    while (!stack.Empty())
                    {
                        string b = stack.Peek();
                        if (b == "(")
                        {
                            stack.Pop(); 
                            break; 
                        }
                        result.Add(b.ToString()); // Добавляем операторы в результат
                        stack.Pop();
                    }
                }
            }

            // Извлекаем оставшиеся операторы из стека
            while (!stack.Empty())
            {
                string b = stack.Peek();
                if (b != "(") result.Add(b.ToString()); 
                if (b == ")") throw new Exception("Count of ( )"); 
                stack.Pop(); 

            }

            return result;
        }

        static public double Calc(string expression)
        {
            // Проверка на пустое выражение
            if (expression == null) throw new Exception("Empty");

            
            MyVector<string> postfixForm = Polska(expression);
            MyStack<double> stack = new MyStack<double>(); 

           
            char[] basicOperator = new char[] { '+', '-', '*', '^', '/' };


            for (int i = 0; i < postfixForm.Size(); i++)
            {
                string element = postfixForm.Get(i);

                
                if (Char.IsDigit(element[0]) || (element.Length > 1 && Char.IsDigit(element[1])))
                {
                    
                    element = element.Replace('.', ',');
                    double number = Convert.ToDouble(element);
                    stack.Push(number); 
                }
                
                else if (Char.IsLetter(element[0]))
                {
                    if (element == "max" || element == "min" || element == "mod")
                    {
                        
                        double number1 = stack.Peek();
                        stack.Pop();
                        double number2 = stack.Peek();
                        stack.Pop();
                        stack.Push(InitOp(element, number1, number2)); 
                    }
                    else
                    {
                        
                        double number = stack.Peek();
                        stack.Pop();
                        stack.Push(InitOp(element, number)); 
                    }
                }
                // Если элемент - оператор
                else if (basicOperator.Contains(element[0]))
                {
                    if (i < postfixForm.Size() - 1 && element == "/" && postfixForm.Get(i + 1) == "/")
                    {
                        // Обработка двойного деления '//' как специального случая, если требуется
                        i++;
                        element += postfixForm.Get(i); 
                    }
                    double number2 = stack.Peek(); 
                    stack.Pop();
                    double number1 = stack.Peek(); 
                    stack.Pop();
                    stack.Push(InitOp(element, number1, number2)); 
                }
            }

            // Берем окончательный результат из стека
            double answer = stack.Peek();
            stack.Pop();
            return answer; // Возвращаем результат
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение:");
            Console.WriteLine();
            string example = Console.ReadLine();
            Console.WriteLine(Calc(example));
        }
    }
}