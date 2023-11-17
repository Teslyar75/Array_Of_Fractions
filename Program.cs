/*Задание 1:
Создайте программу для работы с массивом дробей (числитель
и знаменатель). Она должна иметь такую функциональность:
1. Ввод массива дробей с клавиатуры
2. Сериализация массива дробей
3. Сохранение сериализованного массива в файл
4. Загрузка сериализованного массива из файла. После
загрузки нужно произвести десериализацию
Выбор конкретного формата сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


public class Fraction
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }

    public Fraction() { } 

    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }
}

class Program
{
    static void Main()
    {
        List<Fraction> fractions = new List<Fraction>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Fraction>));

        while (true)
        {
            Console.WriteLine("\nВыберите операцию:");
            Console.WriteLine("1. Ввод массива дробей");
            Console.WriteLine("2. Сериализация и сохранение в файл");
            Console.WriteLine("3. Загрузка из файла и десериализация");
            Console.WriteLine("4. Десериализация и вывод массива дробей на экран");
            Console.WriteLine("5. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InputFractions(fractions);
                    break;
                case "2":
                    SerializeAndSave(fractions, serializer);
                    break;
                case "3":
                    DeserializeFromFile(fractions, serializer);
                    break;
                case "4":
                    PrintDeserializedFractions(fractions);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Что-то пошло не так. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void InputFractions(List<Fraction> fractions)
    {
        Console.WriteLine("Введите значения дробей по принципу: " +
            "числитель/пробел/знаменатель, " +
            "для завершения введите 'goout':");
        string input;
        while (true)
        {
            input = Console.ReadLine();
            if (input.ToLower() == "goout")
                break;

            string[] parts = input.Split(' ');
            if (parts.Length == 2 && int.TryParse(parts[0], out int numerator) && int.TryParse(parts[1], out int denominator))
            {
                fractions.Add(new Fraction(numerator, denominator));
            }
            else
            {
                Console.WriteLine("Что-то пошло не так. Повторите попытку.");
            }
        }
    }

    static void SerializeAndSave(List<Fraction> fractions, XmlSerializer serializer)
    {
        using (TextWriter writer = new StreamWriter("fractions.xml"))
        {
            serializer.Serialize(writer, fractions);
            Console.WriteLine("Массив дробейсохранен в файл fractions.xml");
        }
    }

    static void DeserializeFromFile(List<Fraction> fractions, XmlSerializer serializer)
    {
        try
        {
            using (TextReader reader = new StreamReader("fractions.xml"))
            {
                List<Fraction> loadedFractions = (List<Fraction>)serializer.Deserialize(reader);
                fractions.Clear();
                fractions.AddRange(loadedFractions);
                Console.WriteLine("Массив дробей загружен из файла и десериализован.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    static void PrintDeserializedFractions(List<Fraction> fractions)
    {
        Console.WriteLine("\nДесериализованный массив дробей:");
        foreach (var fraction in fractions)
        {
            Console.WriteLine(fraction);
        }
    }
}
