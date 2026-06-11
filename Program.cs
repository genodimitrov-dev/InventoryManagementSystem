using System;
using System.Text.RegularExpressions;

class Product
{
    public string Code;
    public string Name;

    public string Unit;
    public double Quantity;

    public int Shelflife;
    public char Group;
    public string Date;
    public string Position;
    public double Temperature;

}
class Program
{
    static void PrintProduct(Product p)
    {
        Console.Write(p.Name + " , ");
        Console.Write(p.Code + " , ");
        Console.Write(p.Quantity + " " + p.Unit + ", ");
        Console.Write(p.Date + " , ");
        Console.Write(p.Position + " , ");
        Console.Write(p.Shelflife + " days, ");
        if(p.Group == 'S')
        {
            Console.Write(", °C = " + p.Temperature.ToString("F1"));
        }
        Console.WriteLine();
    }
    static string ConvertDate(string date)
    {
        string day = date.Substring(0,2);
        string month = date.Substring(3,2);
        string year = date.Substring(6,4);
        return day + "/" + month + "/" + year;
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
        p.Code = ReadString("Enter code: ");
        p.Name = ReadString("Enter name: ");
        p.Unit = ReadString("Enter unit (kg/l): ").ToLower();
         while(p.Unit != "kg" && p.Unit != "l")
        {
            Console.Write("Invalid unit. Try again: ");
            p.Unit = ReadString("Enter unit (kg/l): ").ToLower();
        }
        p.Quantity = ReadDouble("Enter quantity: ");
        p.Shelflife = ReadInt("Enter shelflife (days): ");
        p.Date = ReadString("Date: ");
        p.Position = ReadString("Enter position: ");
        p.Group = char.ToUpper(ReadString("Enter group (S/N): ")[0]);
        if(p.Group == 'S')
        {
            p.Temperature = ReadDouble("Enter temperature (°C): ");
        }
        return p;
    }
    static void Main()
    {
        int n;
        Console.Write("Enter number of products: ");
        n = int.Parse(Console.ReadLine());
        while(n<0||n>1000)
        {
            Console.Write("The number doesn't meet the requirments. Please enter again: ");
            n = int.Parse(Console.ReadLine());
        }
        List<Product> products = new List<Product>();
        for(int i=0; i<n; i++)
        {
            products.Add(ReadProduct());
        }
        products.Sort((a,b)=> a.Position.CompareTo(b.Position));
        foreach(Product p in products)
        {
            PrintProduct(p);
        }
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
        string searchCode;
        Console.Write("Enter code: ");
        searchCode = Console.ReadLine();
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
}