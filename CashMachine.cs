using System;
using System.Collections.Generic;

namespace DojoTDDATM
{
	public class CashMachine
	{
		public Dictionary<Int32, Int32> RemainingCash { get; set; } = new Dictionary<Int32, Int32> {
				{500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };

		public void AddCash(Int32 bill, Int32 billAdd)
		{
			if (billAdd >= 0)
			{
				RemainingCash[bill] += billAdd;
			}
			else
			{
				throw new System.ArgumentException();
			}
		}

		public Dictionary<Int32, Int32> Withdraw(Int32 withdrawBill)
		{
			List<Int32> listBill = new List<Int32> { 500, 200, 100, 50, 20, 10, 5 };

			Dictionary<Int32, Int32> withdrawedCash = new Dictionary<Int32, Int32>  {
				{500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0},{10,0 }, {5, 0} };

			foreach (Int32 bill in listBill)
			{
				if (RemainingCash[bill] > 0)
				{
					while (withdrawBill >= bill && RemainingCash[bill] > 0)
					{
						withdrawBill -= bill;
						withdrawedCash[bill] += 1;
						RemainingCash[bill] -= 1;
					}
				}
			}
			foreach (Int32 bill in listBill)
			{
				if (withdrawedCash[bill] == 0)
				{
					withdrawedCash.Remove(bill);
				}
			}
			return withdrawedCash;
		}
	}
}