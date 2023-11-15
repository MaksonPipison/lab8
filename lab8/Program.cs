using System;

public class Calculator
{
    private double _result;

    private void SetResult(double value)
    {
        _result = value;
    }

    public double GetResult()
    {
        return _result;
    }

    public double Add(double a, double b) => a + b;

    public double Subtract(double a, double b) => a - b;

    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Помилка: Ділення на нуль неможливе.");
            SetResult(0);
        }
        else
        {
            SetResult(a / b);
        }

        return GetResult();
    }

    public double Modulus(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Помилка: Операція Modulus з нульовим дільником.");
            SetResult(0);
        }
        else
        {
            SetResult(a % b);
        }

        return GetResult();
    }

    public double Power(double a, double b)
    {
        SetResult(Math.Pow(a, b));
        return GetResult();
    }

    public double Logarithm(double a, double b)
    {
        if (a <= 0 || b <= 0)
        {
            Console.WriteLine("Помилка: Логарифм може бути обчислений тільки для додатніх чисел.");
            SetResult(0);
        }
        else
        {
            SetResult(Math.Log(a, b));
        }

        return GetResult();
    }

    public double Round(double a)
    {
        SetResult(Math.Round(a));
        return GetResult();
    }
}

public enum Operation
{
    Add = 1,
    Subtract,
    Multiply,
    Divide,
    Modulus,
    Power,
    Logarithm,
    Round
}

public class UserInterface
{
    private static Operation GetChoice()
    {
        Console.WriteLine("Виберіть операцію:");
        foreach (var operation in Enum.GetValues(typeof(Operation)))
        {
            Console.WriteLine($"{(int)operation}. {operation}");
        }

        Operation choice;
        while (!Enum.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(Operation), choice))
        {
            Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
        }

        return choice;
    }

    private static double GetOperand(string operandName)
    {
        Console.Write($"Введіть {operandName}: ");
        double operand;
        while (!double.TryParse(Console.ReadLine(), out operand))
        {
            Console.WriteLine("Некоректне значення. Введіть число.");
            Console.Write($"Введіть {operandName}: ");
        }

        return operand;
    }

    public static void ShowResult(double result)
    {
        Console.WriteLine($"Результат: {result}");
    }

    public static void PerformOperation(Calculator calculator)
    {
        Operation choice = GetChoice();
        double operand1 = GetOperand("перше число");
        double operand2 = choice != Operation.Round && choice != Operation.Logarithm ? GetOperand("друге число") : 0;

        double result = 0;

        switch (choice)
        {
            case Operation.Add:
            case Operation.Subtract:
            case Operation.Multiply:
            case Operation.Divide:
            case Operation.Modulus:
            case Operation.Power:
            case Operation.Logarithm:
                result = CalculatorHelper.PerformCalculation(calculator, operand1, operand2, choice);
                break;
            case Operation.Round:
                result = calculator.Round(operand1);
                break;
            default:
                Console.WriteLine("Неправильний вибір операції.");
                break;
        }

        ShowResult(result);
    }
}

public static class CalculatorHelper
{
    public static double PerformCalculation(Calculator calculator, double operand1, double operand2, Operation operation)
    {
        switch (operation)
        {
            case Operation.Add:
                return calculator.Add(operand1, operand2);
            case Operation.Subtract:
                return calculator.Subtract(operand1, operand2);
            case Operation.Multiply:
                return calculator.Multiply(operand1, operand2);
            case Operation.Divide:
                return calculator.Divide(operand1, operand2);
            case Operation.Modulus:
                return calculator.Modulus(operand1, operand2);
            case Operation.Power:
                return calculator.Power(operand1, operand2);
            case Operation.Logarithm:
                return calculator.Logarithm(operand1, operand2);
            default:
                Console.WriteLine("Неправильний вибір операції.");
                return 0;
        }
    }
}

public class Program
{
    public static void Main()
    {
        Calculator myCalculator = new Calculator();
        UserInterface.PerformOperation(myCalculator);
    }
}
