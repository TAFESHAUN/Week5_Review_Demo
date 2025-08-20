using System;
using System.Collections.Generic;

class Program
{
    // Data
    static readonly List<string> products = new() { "Laptop", "Mouse", "Keyboard" };
    // stock[productIndex, storeIndex] (Store0, Store1)
    static readonly int[,] stock = new int[,]
    {
        { 5, 2 },
        { 10, 0 },
        { 7, 4 }
    };
    static readonly double[] prices = { 1200.0, 25.0, 55.0 };
    const int StoreCount = 2;

    static void Main()
    {
        bool running = true;
        while (running)
        {
            try
            {
                int choice = ShowMenuAndReadChoice();
                switch (choice)
                {
                    case 1: ListProductsAndTotals(); break;
                    case 2: SellOneItem(); break;
                    case 3: ShowInventoryValue(); break;
                    case 4: running = false; break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Pause();
            }
        }
    }

    // Menu
    static int ShowMenuAndReadChoice()
    {
        Console.Clear();
        Console.WriteLine("=== Bob Jobs IT Store (MODULAR & FIXED) ===");
        Console.WriteLine("1) List products & total stock");
        Console.WriteLine("2) Sell one item");
        Console.WriteLine("3) Show total inventory value");
        Console.WriteLine("4) Exit");
        Console.Write("Choose (1-4): ");
        string input = Console.ReadLine() ?? string.Empty;
        if (!int.TryParse(input, out int choice))
            throw new FormatException("Menu choice must be a number between 1 and 4.");
        return choice;
    }

    // Features
    static void ListProductsAndTotals()
    {
        Console.WriteLine("Products and total stock:");
        for (int i = 0; i < products.Count; i++)
            Console.WriteLine($" - {products[i]} : {GetTotalStock(i)} units");
        Pause();
    }

    static void SellOneItem()
    {
        Console.Write("Enter product name to sell: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (!TryGetProductIndex(name, out int idx))
        {
            Console.WriteLine("Product not found.");
            Pause();
            return;
        }
        // Sell from Store0, then Store1
        if (stock[idx, 0] > 0) stock[idx, 0]--;
        else if (stock[idx, 1] > 0) stock[idx, 1]--;
        else Console.WriteLine("Out of stock!");
        Pause();
    }

    static void ShowInventoryValue()
    {
        double value = 0;
        for (int i = 0; i < products.Count; i++)
        {
            int total = 0;
            for (int store = 0; store < StoreCount; store++)
                total += stock[i, store];
            value += total * prices[i];
        }
        Console.WriteLine($"Total inventory value: ${value:0.00}");
        Pause();
    }

    // Helpers
    static int GetTotalStock(int productIndex)
    {
        int total = 0;
        for (int store = 0; store < StoreCount; store++)
            total += stock[productIndex, store];
        return total;
    }

    static bool TryGetProductIndex(string name, out int index)
    {
        index = products.FindIndex(p => p.Equals(name, StringComparison.OrdinalIgnoreCase));
        return index >= 0;
    }

    static void Pause()
    {
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }
}
