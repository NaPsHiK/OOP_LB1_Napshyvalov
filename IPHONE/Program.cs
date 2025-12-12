using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int maxCount = ReadInt("Введіть максимальну кількість об’єктів (N > 0): ", 1, 10000);

        List<Phone> phones = new List<Phone>();

        while (true)
        {
            Console.WriteLine("\n===== МЕНЮ =====");
            Console.WriteLine("1 – Додати об’єкт");
            Console.WriteLine("2 – Переглянути всі об’єкти");
            Console.WriteLine("3 – Знайти об’єкт");
            Console.WriteLine("4 – Продемонструвати поведінку");
            Console.WriteLine("5 – Видалити об’єкт");
            Console.WriteLine("0 – Вийти з програми");
            Console.Write("Ваш вибір → ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (phones.Count >= maxCount)
                    {
                        Console.WriteLine("❗ Достигнута максимальна кількість об’єктів!");
                    }
                    else
                    {
                        AddPhone(phones);
                    }
                    break;

                case "2":
                    PrintTable(phones);
                    break;

                case "3":
                    SearchPhones(phones);
                    break;

                case "4":
                    Demonstrate(phones);
                    break;

                case "5":
                    DeletePhones(phones);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("❗ Невірний пункт меню!");
                    break;
            }
        }
    }

    static void AddPhone(List<Phone> list)
    {
        Console.WriteLine("1 – Ручний режим");
        Console.WriteLine("2 – Автоматичний режим");
        Console.Write("Вибір → ");

        string mode = Console.ReadLine();

        Phone p = new Phone();

        if (mode == "1")
        {
            // --- ВАЛІДАЦІЯ ВСІХ ПОЛІВ ---

            // Model name
            while (true)
            {
                Console.Write("Назва моделі (5–30 латинських символів): ");
                string s = Console.ReadLine();

                if (s.Length >= 5 &&
                    s.Length <= 30 &&
                    System.Text.RegularExpressions.Regex.IsMatch(s, @"^[A-Za-z0-9 ]+$"))
                {
                    p.ModelName = s;
                    break;
                }
                Console.WriteLine("❗ Некоректна назва!");
            }

            // Release year
            p.ReleaseYear = ReadInt("Рік релізу (2007 – сьогодні): ", 2007, DateTime.Now.Year);

            // Memory
            int[] allowedMem = { 64, 128, 256, 512, 1024 };
            while (true)
            {
                Console.Write("Обʼєм памʼяті (64/128/256/512/1024): ");
                if (int.TryParse(Console.ReadLine(), out int v) && Array.Exists(allowedMem, x => x == v))
                {
                    p.MemoryGB = v;
                    break;
                }
                Console.WriteLine("❗ Некоректне значення!");
            }

            // Color
            p.Color = (PhoneColor)ReadEnum("Оберіть колір:", typeof(PhoneColor));

            // Material
            p.Material = (BodyMaterial)ReadEnum("Оберіть матеріал корпусу:", typeof(BodyMaterial));

            // CPU
            p.Cpu = (CpuType)ReadEnum("Оберіть процесор:", typeof(CpuType));

            // 5G
            p.Support5G = ReadBool("Підтримка 5G (1 – так, 0 – ні): ");

            // Price
            p.LaunchPrice = ReadDecimal("Ціна на релізі (0 < price ≤ 3000): ", 0.01m, 3000);

        }
        else
        {
            // === Автоматичний режим ===
            Random rnd = new Random();

            string[] models = { "iPhone 12", "iPhone 13 Pro", "iPhone 14 Pro Max", "iPhone 15", "iPhone SE" };

            p.ModelName = models[rnd.Next(models.Length)];
            p.ReleaseYear = rnd.Next(2018, DateTime.Now.Year + 1);
            int[] mem = { 64, 128, 256, 512, 1024 };
            p.MemoryGB = mem[rnd.Next(mem.Length)];

            p.Color = (PhoneColor)rnd.Next(1, 6);
            p.Material = (BodyMaterial)rnd.Next(1, 4);
            p.Cpu = (CpuType)rnd.Next(1, 8);
            p.Support5G = rnd.Next(0, 2) == 1;

            p.LaunchPrice = rnd.Next(699, 2001);
        }

        list.Add(p);
        Console.WriteLine("✔ Телефон успішно додано!");
    }

    static void PrintTable(List<Phone> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Console.WriteLine("\n==============================================================================================");
        Console.WriteLine("|  # |        Модель        | Рік | Памʼять |  Колір  | Матеріал |    CPU    |  5G  |  Ціна  |");
        Console.WriteLine("==============================================================================================");

        for (int i = 0; i < list.Count; i++)
        {
            Phone p = list[i];
            Console.WriteLine(
                $"| {i + 1,2} | {p.ModelName,-20} | {p.ReleaseYear} | {p.MemoryGB,7} | {p.Color,-7} | {p.Material,-9} | {p.Cpu,-8} | {(p.Support5G ? "Так " : "Ні  ")} | {p.LaunchPrice,6}$ |");
        }

        Console.WriteLine("==============================================================================================\n");
    }

    static void SearchPhones(List<Phone> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній. Пошук неможливий.");
            return;
        }

        Console.WriteLine("Пошук за:");
        Console.WriteLine("1 – Колір");
        Console.WriteLine("2 – Ціна");
        Console.Write("Ваш вибір → ");

        string choice = Console.ReadLine();

        List<Phone> results = new List<Phone>();

        if (choice == "1")
        {
            PhoneColor c = (PhoneColor)ReadEnum("Оберіть колір:", typeof(PhoneColor));

            foreach (var p in list)
                if (p.Color == c) results.Add(p);
        }
        else if (choice == "2")
        {
            decimal price = ReadDecimal("Введіть максимальну ціну: ", 1, 3000);

            foreach (var p in list)
                if (p.LaunchPrice <= price) results.Add(p);
        }
        else
        {
            Console.WriteLine("❗ Невірний пункт!");
            return;
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Нічого не знайдено.");
            return;
        }

        PrintTable(results);
    }

    static void Demonstrate(List<Phone> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        PrintTable(list);

        int idx = ReadInt("Введіть номер об’єкта: ", 1, list.Count) - 1;

        Phone p = list[idx];

        Console.WriteLine("\n=== Демонстрація методів ===");
        Console.WriteLine($"Років з релізу: {p.YearsSinceRelease()}");
        Console.Write("Коротка інформація: ");
        p.PrintShort();
        Console.WriteLine($"Флагман: {(p.IsFlagship() ? "Так" : "Ні")}");
        Console.WriteLine($"MagSafe: {(p.SupportsMagSafe() ? "Так" : "Ні")}");
        p.ShowTechnologyInfo();
    }

    static void DeletePhones(List<Phone> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Console.WriteLine("1 – Видалити за номером");
        Console.WriteLine("2 – Видалити за кольором");
        Console.Write("Ваш вибір → ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            PrintTable(list);
            int index = ReadInt("Введіть номер об’єкта: ", 1, list.Count) - 1;
            list.RemoveAt(index);
            Console.WriteLine("✔ Видалено!");
        }
        else if (choice == "2")
        {
            PhoneColor c = (PhoneColor)ReadEnum("Оберіть колір: ", typeof(PhoneColor));
            int removed = list.RemoveAll(ph => ph.Color == c);
            Console.WriteLine($"✔ Видалено {removed} об’єктів.");
        }
        else
        {
            Console.WriteLine("❗ Невірний пункт!");
        }
    }
    
    static int ReadInt(string msg, int min, int max)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                return n;
            Console.WriteLine("❗ Некоректне значення!");
        }
    }

    static decimal ReadDecimal(string msg, decimal min, decimal max)
    {
        while (true)
        {
            Console.Write(msg);
            if (decimal.TryParse(Console.ReadLine(), out decimal n) && n >= min && n <= max)
                return n;
            Console.WriteLine("❗ Некоректне значення!");
        }
    }

    static bool ReadBool(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            string s = Console.ReadLine();
            if (s == "1") return true;
            if (s == "0") return false;
            Console.WriteLine("❗ Введіть 1 або 0!");
        }
    }

    static int ReadEnum(string title, Type t)
    {
        Console.WriteLine(title);

        foreach (var v in Enum.GetValues(t))
            Console.WriteLine($"{(int)v} – {v}");

        while (true)
        {
            Console.Write("Ваш вибір: ");
            if (int.TryParse(Console.ReadLine(), out int n) && Enum.IsDefined(t, n))
                return n;

            Console.WriteLine("❗ Некоректне значення!");
        }
    }
}
