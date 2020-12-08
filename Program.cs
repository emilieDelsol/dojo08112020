using DojoTDDAtmTest;
using System;
using System.Collections.Generic;

namespace DojoTDDATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CashMachine atm = new CashMachine();
            atm.AddCash(500, 10);
            atm.AddCash(200, 10);
            atm.AddCash(100, 10);
            atm.AddCash(50, 10);
            atm.AddCash(20, 10);
            atm.AddCash(10, 10);
            atm.AddCash(5, 10);

            atm.Withdraw(895);
            foreach(var kvp in atm.Withdraw(895))
			{
                Console.WriteLine($"{kvp.Key} {kvp.Value}");
			}
        

	    }
    }
}
