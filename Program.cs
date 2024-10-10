using System;
using Color = System.ConsoleColor;
class Program
{
    static void Main(string[] args)
    {
        Greeting();

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
        Print("3. Edit asset details\n", Color.White);
        Print("4. Remove an asset\n", Color.White);
        Print("5. Quit\n\n", Color.White);
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
        // else if (keyChar == '3' || keyChar == 'e')
        // {
        //     Asset.EditAsset();
        // }
        // else if (keyChar == '4' || keyChar == 'r')
        // {
        //     Asset.RemoveAsset();
        // }
        else if (keyChar == '5' || keyChar == 'q')
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
        Program.Print("Enter offices: ");
        string offices = Console.ReadLine();
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
        Program.Print("Enter currency (usd, eu, etc.): ");
        string currency = Console.ReadLine();
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

            }
        }


        assets.Add(new(type, brand, model, offices, price, currency, purchaseDate));
        Program.Print("Asset added successfully!");
    }
    public static void DisplayAssets()
    {
        Program.Print("\nAll assets:\n");
        foreach (Asset asset in assets)
        {
            Program.Print($"\n{asset.ReturnDetails()}\n");
        }
    }
    public static void createDummyAssets()
    {
        assets.Add(new Computers("Computers", "MacBook", "Pro 2021", "Office A", 1500, "usd", new DateTime(2021, 5, 15)));
        assets.Add(new Computers("Computers", "Asus", "ZenBook", "Office B", 1200, "eu", new DateTime(2020, 8, 20)));
        assets.Add(new Mobiles("Mobile Phones", "iPhone", "13", "Office C", 1000, "usa", new DateTime(2022, 11, 5)));
        assets.Add(new Mobiles("Mobile Phones", "Samsung", "Galaxy S21", "Office B", 900, "eu", new DateTime(2021, 9, 10)));
    }
    public string ReturnDetails()
    {
        return $"{Type} {Brand} {Model} located in {Offices}, purchased for {Price} {Currency} on {PurchaseDate.ToShortDateString()}";
    }

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
