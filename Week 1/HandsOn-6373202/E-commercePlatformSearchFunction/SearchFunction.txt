using System;

public class Product
{
    public int ProductId;
    public string ProductName;
    public string Category;

    public Product(int id, string name, string category)
    {
        ProductId = id;
        ProductName = name;
        Category = category;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        
        Product[] products = new Product[]
        {
            new Product(101, "Laptop", "Electronics"),
            new Product(102, "Shampoo", "Personal Care"),
            new Product(103, "Table", "Furniture"),
            new Product(104, "Mobile", "Electronics"),
            new Product(105, "Book", "Stationery")
        };

        Console.WriteLine("Linear Search ");
        
        LinearSearch(products, "Mobile");
        
        Console.WriteLine();
        
        BubbleSortByName(products);
        
        Console.WriteLine("Binary Search Result:");
        
        BinarySearch(products, "Mobile");
    }

    public static void LinearSearch(Product[] products, string item)
    {
        bool found = false;

        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].ProductName == item)
            {
                Console.WriteLine("Product found: " + products[i].ProductName + " (" + products[i].Category + ")");
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Product not found.");
        }
    }

    public static void BinarySearch(Product[] products, string item)
    {
        int low = 0;
        int high = products.Length - 1;
        bool found = false;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            int result = string.Compare(products[mid].ProductName, item);

            if (result == 0)
            {
                Console.WriteLine("Product found: " + products[mid].ProductName + " (" + products[mid].Category + ")");
                found = true;
                break;
            }
            else if (result < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        if (!found)
        {
            Console.WriteLine("Product not found.");
        }
    }

    public static void BubbleSortByName(Product[] products)
    {
        for (int i = 0; i < products.Length - 1; i++)
        {
            for (int j = 0; j < products.Length - i - 1; j++)
            {
                if (string.Compare(products[j].ProductName, products[j + 1].ProductName) > 0)
                {
                    Product temp = products[j];
                    products[j] = products[j + 1];
                    products[j + 1] = temp;
                }
            }
        }
    }
}