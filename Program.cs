using System;
using Color = System.ConsoleColor;
class Program
{
    static void Main(string[] args)
    {
        Greeting();
        DisplayMenuOptions();
        HandleInput();
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
        Print("5. Exit\n\n", Color.White);
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
        Program.Print("\nEnter asset type: ");
        string type = Console.ReadLine();
        Program.Print("Enter brand: ");
        string brand = Console.ReadLine();
        Program.Print("Enter model: ");
        string model = Console.ReadLine();
        Program.Print("Enter offices: ");
        string offices = Console.ReadLine();
        Program.Print("Enter price: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Program.Print("Enter currency: ");
        string currency = Console.ReadLine();
        Program.Print("Enter purchase date (yyyy-MM-dd): ");
        DateTime purchaseDate = Convert.ToDateTime(Console.ReadLine());

        assets.Add(new Computers(type, brand, model, offices, price, currency, purchaseDate));
        Program.Print("Asset added successfully!");
    }
    public string DisplayDetails()
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

    // You can extend or override methods here if needed
}