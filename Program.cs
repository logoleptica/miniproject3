using System;
using System.Diagnostics.Contracts;
using Color = System.ConsoleColor;
class Program
{
    static void Main(string[] args)
    {
        Greeting();
        Asset.createDummyAssets();

        while (true)
        {
            DisplayMenuOptions();
            HandleInput();
        }
    }
    public static void Print(string message, Color color = Color.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }
    static void DrawLine(string symbol, int count = 33, Color color = Color.White)
    {
        Print(string.Concat(Enumerable.Repeat(symbol, count)), Color.Red);
    }

    static void Greeting()
    {
        DrawLine("#", 32, Color.DarkCyan);
        Print("\n# # # Electronic Tracker! # # #\n", Color.Red);
        DrawLine("#", 32, Color.DarkCyan);
    }

    private static void DisplayMenuOptions()
    {
        Print("\n\n\tChoose an option:\n\n", Color.White);
        Print("1. Add a new asset\n", Color.White);
        Print("2. Display all assets\n", Color.White);
        Print(". Quit\n\n", Color.White);
        Print("\tEnter your choice: \n", Color.White);
    }
    private static void HandleInput()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        char keyChar = char.ToLower(key.KeyChar);
        if (keyChar == '1' || keyChar == 'a')
        {
            Asset.AddAsset();
        }
        else if (keyChar == '2' || keyChar == 'd')
        {
            Asset.DisplayAssets();
        }
        else if (keyChar == '3' || keyChar == 'q')
        {
            Environment.Exit(0);
        }
    }

}


class Asset
{
    public static List<Asset> assets = new List<Asset>();
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Offices { get; set; }
    public double Price { get; set; }
    public string Currency { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Asset(string type, string brand, string model,
    string offices, double price, string currency, DateTime purchaseDate)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Offices = offices;
        Price = price;
        Currency = currency;
        PurchaseDate = purchaseDate;
    }
    public static void AddAsset()
    {
        string type;
        while (true)
        {
            Program.Print("\nEnter asset type: ");
            type = Console.ReadLine();
            if (type.ToLower() == "computer" || type.ToLower() == "mobile")
            {
                break;
            }
            Program.Print("Invalid input. Please enter 'computer' or 'mobile'.\n");
        }
        Program.Print("Enter brand: ");
        string brand = Console.ReadLine();
        Program.Print("Enter model: ");
        string model = Console.ReadLine();

        double price;
        while (true)
        {
            Program.Print("Enter price: ");
            try
            {
                price = Convert.ToDouble(Console.ReadLine());
                break;
            }
            catch (Exception e)
            {
                Program.Print("Invalid input. Please enter a valid price.\n");

            }

        }
        string office = ValidateCountry();
        string currency = GetCurrency(office);
        DateTime purchaseDate;
        while (true)
        {
            Program.Print("Enter purchase date (yyyy-MM-dd): ");

            try
            {
                purchaseDate = Convert.ToDateTime(Console.ReadLine());
                break;
            }
            catch (Exception e)
            {
                Program.Print("Invalid input. Please enter a valid date.\n");
                Program.Print(e.Message);

            }
        }
        assets.Add(new(type, brand, model, office, price, currency, purchaseDate));
        Program.Print("Asset added successfully!");
    }

    public static string ValidateCountry()
    {

        while (true)
        {
            Program.Print("Enter country: ");
            string country = Console.ReadLine().ToLower();
            if (country == "usa")
            {
                return "USA";
            }
            else if (country == "uk")
            {
                return "UK";
            }
            else if (country == "china")
            {
                return "China";
            }
            else if (country == "eu")
            {
                return "EU";
            }
            Program.Print("\nValid regions are EU, USA, UK, China\n");

        }

    }
    public static string GetCurrency(string country)
    {
        if (country.ToLower() == "usa")
        {
            return ("USD");
        }
        else if (country.ToLower() == "uk")
        {
            return ("GBP");
        }
        else if (country.ToLower() == "china")
        {
            return ("CNY");
        }
        else
        {
            return ("EUR");
        }
    }
    public static void DisplayAssets()

    {

        Program.Print("\n   TYPE".PadRight(17) + "BRAND".PadRight(17) + "MODEL".PadRight(17) + "OFFICE".PadRight(17) +
                            "PRICE".PadRight(17) + "CURRENCY".PadRight(20) + "PURCHASE DATE".PadRight(17), Color.Magenta);
        TimeSpan timeSincePurchase;
        double threeYears = 365 * 3;
        double month = 30;

        foreach (Asset asset in assets)
        {
            timeSincePurchase = DateTime.Now - asset.PurchaseDate;
            if (timeSincePurchase.Days > threeYears)
            {
                Program.Print($"\n{asset.ReturnDetails(asset)}\n", Color.Red);

            }
            else if (timeSincePurchase.Days > threeYears - (month * 6))
            {
                Program.Print($"\n{asset.ReturnDetails(asset)}\n", Color.Yellow);
            }

            else
            {

                Program.Print($"\n{asset.ReturnDetails(asset)}\n", Color.Green);

            }
        }
    }
    public static void createDummyAssets()
    {
        assets.Add(new Computers("Computers", "MacBook", "Pro 2021", "Office A", 1500, "usd", new DateTime(2021, 5, 15)));
        assets.Add(new Computers("Computers", "Asus", "ZenBook", "Office B", 1200, "eur", new DateTime(2024, 8, 20)));
        assets.Add(new Mobiles("Mobiles", "iPhone", "13", "Office C", 1000, "cny", new DateTime(2021, 11, 11)));
        assets.Add(new Mobiles("Mobiles", "Samsung", "Galaxy S21", "Office B", 900, "gbp", new DateTime(2021, 9, 10)));
    }


    public string ReturnDetails(Asset asset)
    {
        return $"{asset.Type.PadRight(13)}{asset.Brand.PadRight(17)}{asset.Model.PadRight(17)} {asset.Offices.PadRight(17)}{asset.Price.ToString().PadRight(17)}{asset.Currency.PadRight(20)}{asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(17)}";


    }

    class Computers : Asset
    {
        // Constructor for Computers that calls the base class constructor
        public Computers(string type, string brand, string model,
        string offices, double price, string currency, DateTime purchaseDate)
        : base(type, brand, model, offices, price, currency, purchaseDate)
        {
        }

    }
    class Mobiles : Asset
    {
        public Mobiles(string type, string brand, string model, string office, double price, string currency, DateTime purchaseDate)
            : base(type, brand, model, office, price, currency, purchaseDate) { }
    }
}
