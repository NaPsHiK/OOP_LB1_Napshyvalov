using System;

public class Phone
{
    public string ModelName { get; set; }
    public int ReleaseYear { get; set; }
    public int MemoryGB { get; set; }
    public PhoneColor Color { get; set; }
    public BodyMaterial Material { get; set; }
    public CpuType Cpu { get; set; }
    public bool Support5G { get; set; }
    public decimal LaunchPrice { get; set; }

    public int YearsSinceRelease()
    {
        return DateTime.Now.Year - ReleaseYear;
    }

    public void PrintShort()
    {
        Console.WriteLine($"{ModelName} • {MemoryGB}GB • {Color} • ${LaunchPrice}");
    }

    public bool IsFlagship()
    {
        return ModelName.Contains("Pro", StringComparison.OrdinalIgnoreCase);
    }

    public bool SupportsMagSafe()
    {
        return ReleaseYear >= 2020; // iPhone 12 і новіші
    }

    public void ShowTechnologyInfo()
    {
        Console.WriteLine("Технології пристрою:");
        Console.WriteLine($"- 5G: {(Support5G ? "Так" : "Ні")}");
        Console.WriteLine($"- SoC: {Cpu}");
        Console.WriteLine($"- Матеріал корпусу: {Material}");
        Console.WriteLine($"- Підтримка MagSafe: {(SupportsMagSafe() ? "Так" : "Ні")}");
    }
}
