using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
class Program
{
    static void PrintProduct(Product p)
    {
        Console.Write(p.Name + " , ");
        Console.Write(p.Code + " , ");
        Console.Write(p.Quantity + " " + p.Unit + ", ");
        Console.Write(p.Date + " , ");
        Console.Write(p.Position + " , ");
        Console.Write(p.Shelflife + " days");
        if(p.Group == 'S')
        {
            Console.Write(", °C = " + p.Temperature.ToString("F1"));
        }
        Console.WriteLine();
        
    }
    static string ProductToString(Product p)
    {
    return p.Name + ";" +
           p.Code + ";" +
           p.Quantity + ";" +
           p.Unit + ";" +
           p.Shelflife + ";" +
           p.Date + ";" +
           p.Position + ";" +
           p.Group + ";" +
           p.Temperature;
    }
    static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Warehouse Management System ");
        Console.WriteLine("1. Add products");
        Console.WriteLine("2. Show products");
        Console.WriteLine("3. Show S products");
        Console.WriteLine("4. Search by code");
        Console.WriteLine("5. Delete products");
        Console.WriteLine("6. Save to files");
        Console.WriteLine("7. Load from files");
        Console.WriteLine("8. Edit products");
        Console.WriteLine("9. Exit");
    }
    static void EditMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Choose an option to edit: ");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Code");
        Console.WriteLine("3. Quantity");
        Console.WriteLine("4. Unit");
        Console.WriteLine("5. Shelflife");
        Console.WriteLine("6. Date");
        Console.WriteLine("7. Position");
        Console.WriteLine("8. Group");
        Console.WriteLine("9. Temperature");
        Console.WriteLine("10. Exit");
    }
    static void ShowProducts(List<Product> products)
    {
        List<Product> sortedProducts =
        products.OrderBy(p => p.Name).ToList();

        foreach(Product p in sortedProducts)
        {
            PrintProduct(p);
        }
    }
    static string ConvertDate(string date)
    {
        string day = date.Substring(0,2);
        string month = date.Substring(3,2);
        string year = date.Substring(6,4);
        return day + "/" + month + "/" + year;
    }
    static void ShowSpecialProducts(List<Product> products)
    {
        List<Product>specialProducts = new List<Product>();
        foreach(Product p in products)
        {
            if(p.Group == 'S')
            {
                specialProducts.Add(p);
            }
        }
        specialProducts.Sort((a,b)=>
        {
            string dateA = ConvertDate(a.Date);
            string dateB = ConvertDate(b.Date);
            if(dateA!=dateB)
            {
                return dateA.CompareTo(dateB);
            }
            return b.Shelflife.CompareTo(a.Shelflife);
        });
        foreach(Product p in specialProducts)
        {
            PrintProduct(p);
        }
    }
    static void SearchByCode(List<Product> products)
    {
        string searchCode;
        Console.Write("Enter code: ");
        searchCode = Console.ReadLine().ToUpper();
        bool found = false;
        bool hasSpacial = false;
        double TotalQuantity = 0;
        double minTemp = double.MaxValue;
        foreach(Product p in products)
        {
            if(p.Code == searchCode)
            {
                found = true;
                PrintProduct(p);
                TotalQuantity += p.Quantity;
                if(p.Group == 'S')
                {
                    hasSpacial = true;
                    if(p.Temperature < minTemp)
                    {
                        minTemp = p.Temperature;
                    }
                }
            }
        }
        if(!found)
        {
            Console.WriteLine("No product found");
        }
        else
        {
            Console.WriteLine("Total quantity: " + TotalQuantity + " kg");
            if(hasSpacial)
            {
                Console.WriteLine("Minimum temperature: " + minTemp.ToString("F1") + " °C");
            }
        }
    }
    static void DeleteProducts(List<Product> products)
    {
        string searchCode;
        Console.Write("Enter code: ");
        searchCode = Console.ReadLine().ToUpper();
        bool found = false;
        int deleteCount = 0;
        for(int i=products.Count-1; i>=0; i--)
        {
            if(string.Equals(products[i].Code,searchCode,StringComparison.OrdinalIgnoreCase))
            {
                found = true;
                products.RemoveAt(i);
                deleteCount++;
            }
        }
        if(!found)
        {
            Console.WriteLine("No product found");
        }
        else
        {
            Console.WriteLine(deleteCount + " products deleted");
        }
    }
    static void SaveToFile(List<Product> products)
    {
        using(StreamWriter writer = new StreamWriter("products.txt"))
        {
            foreach(Product p in products)
        {
            writer.WriteLine(ProductToString(p));
        }
        Console.WriteLine("Products saved to products.txt");
        }
    }
    static void LoadFromFile(List<Product> products)
    {
        if(File.Exists("products.txt"))
        {
            using(StreamReader reader = new StreamReader("products.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if(parts.Length == 9)
                    {
                        Product p = new Product();
                        p.Name = parts[0];
                        p.Code = parts[1];
                        p.Quantity = double.Parse(parts[2]);
                        p.Unit = parts[3];
                        p.Shelflife = int.Parse(parts[4]);
                        p.Date = parts[5];
                        p.Position = parts[6];
                        p.Group = char.Parse(parts[7]);
                        p.Temperature = double.Parse(parts[8]);
                        products.Add(p);
                    }
                }
            }
            Console.WriteLine("Products loaded from products.txt");
        }
        else
        {
            Console.WriteLine("No saved file found");
        }
    }
    static void EditProduct(Product p)
    {
        bool editing = true;
        while(editing)
        {
            EditMenu();
            Console.Write("Choose an option to edit: ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    p.Name = ReadString("Enter new name: ").ToUpper();
                    break;
                case "2":
                    p.Code = ReadString("Enter new code: ").ToUpper();
                    break;
                case "3":
                    p.Quantity = ReadDouble("Enter new quantity: ");
                    break;
                case "4":
                    p.Unit = ReadString("Enter new unit (kg/l): ").ToUpper();
                    while(p.Unit != "KG" && p.Unit != "L")
                    {
                        Console.Write("Invalid unit. Try again: ");
                        p.Unit = ReadString("Enter new unit (kg/l): ").ToUpper();
                    }
                    break;
                case "5":
                    p.Shelflife = ReadInt("Enter new shelflife (days): ");
                    break;
                case "6":
                    p.Date = ReadString("Enter new date: ");
                    break;
                case "7":
                    p.Position = ReadString("Enter new position: ");
                    break;
                case "8":
                    char newGroup = char.ToUpper(ReadString("Enter new group (S/N): ")[0]);
                    while(newGroup != 'S' && newGroup != 'N')
                    {
                        Console.WriteLine("Invalid group.");
                        newGroup = char.ToUpper(ReadString("Enter new group (S/N): ")[0]);
                    }
                    if(newGroup == 'S' && p.Group != 'S')
                    {
                        p.Temperature = ReadDouble("Enter temperature (°C): ");
                    }
                    else if(newGroup != 'S')
                    {
                        p.Temperature = 0;
                    }
                    p.Group = newGroup;
                    break;
                case "9":
                    if(p.Group == 'S')
                    {
                        p.Temperature = ReadDouble("Enter new temperature (°C): ");
                    }
                    else
                    {
                        Console.WriteLine("Product is not in group S. Cannot edit temperature.");
                    }
                    break;
                case "10":
                    editing = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
    static string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
    static int ReadInt(string message)
    {
       
        int value;

        Console.Write(message);

        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid number. Try again: ");
        }

        return value;
    }
    static double ReadDouble(string message)
    {
    
        double value;

        Console.Write(message);

        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Invalid number. Try again: ");
        }
        return value;

    }
    static Product ReadProduct()
    {
        Product p = new Product();
        p.Name = ReadString("Enter name: ").ToUpper();
        p.Code = ReadString("Enter code: ").ToUpper();
        p.Unit = ReadString("Enter unit (kg/l): ").ToUpper();
        while(p.Unit != "KG" && p.Unit != "L")
        {
            Console.Write("Invalid unit. Try again: ");
            p.Unit = ReadString("Enter unit (kg/l): ").ToUpper();
        }
        p.Quantity = ReadDouble("Enter quantity: ");
        p.Shelflife = ReadInt("Enter shelflife (days): ");
        p.Date = ReadString("Date: ");
        p.Position = ReadString("Enter position: ").ToUpper();
        p.Group = char.ToUpper(ReadString("Enter group (S/N): ")[0]);
        while(p.Group != 'S' && p.Group != 'N')
        {
            Console.WriteLine("Invalid group.");
            p.Group = char.ToUpper(ReadString("Enter group (S/N): ")[0]);
        }
        if(p.Group == 'S')
        {
            p.Temperature = ReadDouble("Enter temperature (°C): ");
        }
        return p;
    }
    static void Main()
    {
       
        List<Product> products = new List<Product>();
        bool ProgramRunning = true;
        while(ProgramRunning)
        {
            ShowMenu();
            Console.Write("Choose an option: ");
            string choiceFromMenu = Console.ReadLine();
            switch(choiceFromMenu)
            {
                case "1":
                    products.Add(ReadProduct());
                    break;
                case "2":
                    ShowProducts(products);
                    break;
                case "3":
                    ShowSpecialProducts(products);
                    break;
                case "4":
                    SearchByCode(products);
                    break;
                case "5":
                    DeleteProducts(products);
                    break;
                case "6":
                    SaveToFile(products);
                    break;
                case "7":
                    LoadFromFile(products);
                    break;
                case "8":
                    EditProduct(products[0]);
                    break;
                case "9":                    
                    ProgramRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
       
    }
}
