using ConsoleTables;
using System;
using System.Linq;

namespace SimpleShopManagement
{
    class Customers : Program
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public int Debt { get; set; }

        public static void ShowList()
        {
            Console.Clear();

            ConsoleTable.From<Customers>(CuList).Write(Format.Alternative);

            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        public static void AddCustomer(ref int id)
        {
            Console.Clear();

            var customer = new Customers();
            Console.WriteLine("Enter Name Of The Customer: ");
            customer.Name = Console.ReadLine();

            Console.WriteLine("Enter The Address Of The Customer: ");
            customer.Address = Console.ReadLine();

            CuList.Add(customer);

            id++;
            customer.Id = id;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Customer [{0}] Has Been Add Successfully!", customer.Name);

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();

        }

        public static void RmvCustomer()
        {
            Console.Clear();

            var customer = new Customers();

            Console.WriteLine("Enter The Id Of The Customer That You Want To Remove: ");
            int Search = Convert.ToInt32(Console.ReadLine());

            if (CuList.Any(key => key.Id == Search))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Customer [{0}] Has Been Removed Successfully!", customer.Name);
                CuList.RemoveAt(Search - 1);

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couldn't Find This Id!");
            }

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        public static void AddBalance()
        {
            Console.Clear();

            ConsoleTable.From<Customers>(CuList).Write(Format.MarkDown);
            Console.Write("Enter Customer Id: ");

            int CU_Id = Convert.ToInt32(Console.ReadLine());
            var customer = new Customers();

            if (CuList.Any(key => key.Id == CU_Id))
            {
                Console.Write("Now Enter Customer's Payment: ");
                int Pay = Convert.ToInt32(Console.ReadLine());

                CuList[CU_Id - 1].Debt += Pay;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Total Ammount Of ${0} Has Benn Add To {1}'s Balance Successfully!", Pay, CuList[CU_Id - 1].Name);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couldn't Find This Id!");
            }

            Console.ResetColor();
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }
    }
}
