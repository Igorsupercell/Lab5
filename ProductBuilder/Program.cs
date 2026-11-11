using Lab5.ProductBuilder.Models;
using Lab5.ProductBuilder.Services;
using System.Globalization;

namespace ProductBuilder;

class Program
{
    static void Main(string[] args)
    {
        string file;
        if (args.Length > 0)
        {
            file = args[0];
        }
        else file = "sweet.txt";

        var gift = new Gift();
        LoadGiftFromTxt(file, gift);

        while (true)
        {
            Console.WriteLine("\n--- Подарок ---");
            Console.WriteLine("1. Показать состав и вес");
            Console.WriteLine("2. Сортировка по весу");
            Console.WriteLine("3. Сортировка по сахару");
            Console.WriteLine("4. Найти по сахару (диапазон)");
            Console.WriteLine("5. Добавить конфеты из другого файла");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            switch (Console.ReadLine())
            {
                case "1":
                    if (!gift.Sweets.Any()) Console.WriteLine("Подарок пуст.");
                    foreach (var s in gift.Sweets) Console.WriteLine(s.GetInfo());
                    Console.WriteLine($"Общий вес: {gift.GetTotalWeight()}г");
                    break;
                case "2":
                    gift.Sort(new WeightComparer());
                    Console.WriteLine("Отсортировано по весу.");
                    break;
                case "3":
                    gift.Sort(new SugarComparer());
                    Console.WriteLine("Отсортировано по сахару.");
                    break;
                case "4":
                    Console.Write("Мин сахар: ");
                    double min = double.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Макс сахар: ");
                    double max = double.Parse(Console.ReadLine() ?? "100");

                    var found = gift.FindBySugar(min, max).ToList();
                    if (found.Count == 0) Console.WriteLine("Ничего не найдено.");
                    foreach (var s in found) Console.WriteLine(s.GetInfo());
                    break;
                case "5":
                    Console.Write("Введите имя файла (например, extra.txt): ");
                    string fileName = Console.ReadLine() ?? "";
                    LoadGiftFromTxt(fileName, gift);
                    break;
                case "0": return;
                default: Console.WriteLine("Ошибка ввода."); break;
            }
        }
    }


    private static void LoadGiftFromTxt(string path, Gift gift)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"[Ошибка] Файл '{path}' не найден!");
            return;
        }


        string fullText = File.ReadAllText(path);
        string[] blocks = fullText.Split("---", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var format = CultureInfo.InvariantCulture;

        foreach (var block in blocks)
        {

            string type = "", name = "", manufacturer = "", chocolateType = "", flavor = "", shape = "", waffleType = "", filling = "";
            double weight = 0, sugar = 0;


            string[] lines = block.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            Sweet? newSweet = null;

            foreach (var line in lines)
            {
                var parts = line.Split(':', 2, StringSplitOptions.TrimEntries);
                if (parts.Length < 2) continue;

                string key = parts[0];
                string value = parts[1];


                if (key == "Тип")
                {
                    if (value == "Шоколад") newSweet = new ChocolateCandy();
                    else if (value == "Леденец") newSweet = new Lollipop();
                    else if (value == "Вафля") newSweet = new WaffleCandy();
                }

                else if (newSweet != null)
                {

                    if (key == "Название") newSweet.Name = value;
                    else if (key == "Производитель") newSweet.Manufacturer = value;
                    else if (key == "Вес") newSweet.Weight = double.Parse(value, format);
                    else if (key == "Сахар") newSweet.SugarPer100g = double.Parse(value, format);

                    else if (key == "Тип шоколада" && newSweet is ChocolateCandy choc)
                        choc.ChocolateType = value;

                    else if (key == "Начинка" && newSweet is ChocolateCandy chocy)
                        chocy.HasFilling = value.ToLower() == "да" || value.ToLower() == "true";

                    else if (key == "Вкус" && newSweet is Lollipop lolli)
                        lolli.Flavor = value;

                    else if (key == "Форма" && newSweet is Lollipop lollii)
                        lollii.Shape = value;

                    else if (key == "Тип вафли" && newSweet is WaffleCandy waf)
                        waf.WaffleType = value;
                }
            }

            if (newSweet != null)
            {
                gift.Add(newSweet);
            }

            Console.WriteLine($"Успешно загружена сладость из файла '{path}'.");
            
        }
    }
}
