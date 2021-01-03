using ConsoleTables;
using System;
using System.Linq;


namespace SimpleShopManagement
{
    class Product : Program
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Ammount { get; set; }
        public int BuyPrice { get; set; }
        public int SellPrice { get; set; }

        public static void ShowList()
        {
            Console.Clear();

            ConsoleTable.From<Product>(ProductList).Write(Format.Alternative);

            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        public static void AddProduct(ref int id)
        {
            Console.Clear();
            
            var product = new Product();
            Console.WriteLine("Enter Name Of The Product: ");
            product.Name = Console.ReadLine();

            Console.WriteLine("Enter The Ammount Of The Product: ");
            product.Ammount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter The Buy Price Of The Product: ");
            product.BuyPrice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter The Sale Price The Product: ");
            product.SellPrice = Convert.ToInt32(Console.ReadLine());

            ProductList.Add(product);

            id++;
            product.Id = id;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product Named [{0}] Has Been Add Successfully!", product.Name);

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        public static void EditProduct()
        {
            Console.Clear();

            Console.WriteLine("Enter The Id Of The Product That You Want To Edit: ");
            int Search = Convert.ToInt32(Console.ReadLine());

            if (ProductList.Any(key => key.Id == Search))
            {
                Console.WriteLine("You Are About To Edit Product {0}: ",
                    ProductList[Search - 1].Name);

                Console.WriteLine("Enter New Name Of The Product: ");
                ProductList[Search - 1].Name = Console.ReadLine();

                Console.WriteLine("Enter The Ammount Of The Product: ");
                ProductList[Search - 1].Ammount = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter The Buy Price Of The Product: ");
                ProductList[Search - 1].BuyPrice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter The Sale Price The Product: ");
                ProductList[Search - 1].SellPrice = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couldn't Find This Id!");
                Console.ResetColor();
            }
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();

        }

    }
}
