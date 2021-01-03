using ConsoleTables;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleShopManagement
{
    class Program
    {
        public static List<Product> ProductList = new List<Product>();
        public static List<Customers> CuList = new List<Customers>();

        static void Main(string[] args)
        {
            // Saving property in a local varible for ref
            var _product = new Product();
            int id = _product.Id;

            var _Customer = new Customers();
            int _id = _Customer.Id;

            int Quantity = 0, TotalSell = 0, TotalBuy = 0;

            // ConsoleMenu Framework
            var subMenu_Pro = new ConsoleMenu(args, level: 1)
            .Add("Back To Main Menu", ConsoleMenu.Close)
            .Add("Show Current Products", Product.ShowList)
            .Add("Add New Product", () => Product.AddProduct(ref id))
            .Add("Edit Products", () => Product.EditProduct())
           .Configure(config =>
           {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Products Management";
               config.EnableWriteTitle = true;
           });

            var subMenu_Cu = new ConsoleMenu(args, level: 1)
            .Add("Back To Main Menu", ConsoleMenu.Close)
             .Add("Show Current Customers", Customers.ShowList)
            .Add("Add Customer", () => Customers.AddCustomer(ref _id))
            .Add("Remove Customer", () => Customers.RmvCustomer())
            .Add("Add Balance For Customer", () => Customers.AddBalance())

           .Configure(config =>
           {
               config.Selector = "--> ";
               config.EnableFilter = true;
               config.Title = "Customers Management";
               config.EnableWriteTitle = true;
           });

            var menu = new ConsoleMenu(args, level: 0)
              .Add("Exit", () => Environment.Exit(0))
              .Add("Manage Products", () => subMenu_Pro.Show())
              .Add("Manage Customers", () => subMenu_Cu.Show())
              .Add("Sell To Customer", () => SellToCustomer(ref Quantity,ref TotalSell,ref TotalBuy))
              .Add("Sales Report", () => Report(Quantity, TotalSell, TotalBuy))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "---Shop Management System--- \n";
                  config.EnableWriteTitle = true;
                  config.SelectedItemBackgroundColor = ConsoleColor.Gray;

              });
            menu.Show();

        }

        private static void SellToCustomer (ref int Count, ref int TotalSell, ref int TotalBuy)
        {
            Console.Clear();

            ConsoleTable.From<Customers>(CuList).Write(Format.MarkDown);
            Console.WriteLine("Enter Customer Id: ");
            int CU_Id = Convert.ToInt32(Console.ReadLine());

            if (CuList.Any(key => key.Id == CU_Id))
            {
                ConsoleTable.From<Product>(ProductList).Write(Format.MarkDown);

                Console.WriteLine("Now Enter Product Id: ");
                int PR_Id = Convert.ToInt32(Console.ReadLine());

                if (ProductList.Any(key => key.Id == PR_Id))
                {
                    Console.WriteLine("Enter Product Ammount For Selling: ");
                    int Ammount = Convert.ToInt32(Console.ReadLine());

                    if (Ammount <= ProductList[PR_Id - 1].Ammount)
                    {
                        Count += Ammount;
                        TotalSell += ProductList[PR_Id - 1].SellPrice;
                        TotalBuy += ProductList[PR_Id - 1].BuyPrice;
                        CuList[CU_Id - 1].Debt -= Ammount * ProductList[PR_Id - 1].SellPrice;
                        ProductList[PR_Id - 1].Ammount -= Ammount;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You Have Successfully Sold {0} Ammount Of {1}  For Total Price Of ${2}",
                            Ammount, ProductList[PR_Id -1].Name, Ammount * ProductList[PR_Id - 1].SellPrice);
                    }
                    else
                        Console.WriteLine("You Can't Sell More Than You Have!");
                }
                else
                    Console.WriteLine("Coudn't Find This Product!");
            }
            else
                Console.WriteLine("Coudn't Find This Customer!");

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();

        }

        private static void Report(int Ammount, int Sell, int Buy)
        {
            Console.Clear();

            Console.WriteLine("Total Products Sold:" + Ammount);
            Console.WriteLine("Total Money Earned:" + Ammount * Sell);

            int Total = Ammount * (Sell - Buy);

            if (Total >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Total Profit: $" + Total);

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Total Loss: $" + Total);
            }

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }
    }
}