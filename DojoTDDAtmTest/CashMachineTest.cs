using NUnit.Framework;
using System;
using System.Collections.Generic;
using DojoTDDATM;
namespace DojoTDDAtmTest
{
    public class Tests
    {
        [Test]
        public void TestEmpty()
        {
            CashMachine atm = new CashMachine();

            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                 "A new CashMachine should have 0 of every possible bills");
        }
        
        [Test]
        public void TestAddZero()
        {
            CashMachine atm = new CashMachine();

            atm.AddCash(200, 0);

            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };

            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding 0 of an existing bill should let the CashMachine unchanged");
        }

        [Test]
        public void TestAddNegative()
        {
            CashMachine atm = new CashMachine();

            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };

            Assert.Throws<ArgumentException>(delegate
            {
                atm.AddCash(100, -1); // 
            }, "An exception should be throwned when adding a negative number of an existing bill");


            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding a negative number of an existing bill should let the CashMachine unchanged"
            );
        }
        
        [Test]
        public void TestSuccessiveSameAdds()
        {
            CashMachine atm = new CashMachine();

            // Add a 500
            atm.AddCash(500, 1);
            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 1}, {200, 0},{ 100,0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 500 to an empty machine should change the remaining cash accordingly"
            );

            // Add two 500
            atm.AddCash(500, 2);
            remainingCash[500] = 3;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding two 500 to a one 500 machine should change the remaining cash accordingly"
            );

            // Add ten 500
            atm.AddCash(500, 10);
            remainingCash[500] = 13;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding ten 500 to a three 500 machine should change the remaining cash accordingly"
            );
        }
        
        [Test]
        public void TestSuccessiveDifferentAdds()
        {
            CashMachine atm = new CashMachine();

            // Add a 200
            atm.AddCash(200, 1);
            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 0}, {200, 1}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 200 to an empty machine should change the remaining cash accordingly"
            );

            // Add a 100
            atm.AddCash(100, 1);
            remainingCash[100] = 1;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 100 to a one 200 machine should change the remaining cash accordingly"
            );

            // Add a 50
            atm.AddCash(50, 1);
            remainingCash[50] = 1;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 50 to a one 200/100 machine should change the remaining cash accordingly"
            );

            // Add a 20
            atm.AddCash(20, 1);
            remainingCash[20] = 1;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 20 to a one 200/100/50 machine should change the remaining cash accordingly"
            );

            // Add a 10
            atm.AddCash(10, 1);
            remainingCash[10] = 1;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 10 to a one 200/100/50/20 machine should change the remaining cash accordingly"
            );

            // Add a 5
            atm.AddCash(5, 1);
            remainingCash[5] = 1;
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Adding one 5 to a one 200/100/50/20/10 machine should change the remaining cash accordingly"
            );
        }
        
        [Test]
        public void TestMultipleBillWithdraw()
        {
            CashMachine atm = new CashMachine();
            atm.AddCash(500, 10);
            atm.AddCash(200, 10);
            atm.AddCash(100, 10);
            atm.AddCash(50, 10);
            atm.AddCash(20, 10);
            atm.AddCash(10, 10);
            atm.AddCash(5, 10);

            Dictionary<Int32, Int32> withdrawedCash = new Dictionary<Int32, Int32> {
                {500, 1}, {200, 1}, {100, 1}, {50, 1}, {20, 2}, {5, 1} };
            Assert.AreEqual(withdrawedCash, atm.Withdraw(895),
                "Withdraw 895 should get you one of each 500/200/100/50/5 and two 20 if the machine have enough"
            );

            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 9}, {200, 9}, {100, 9}, {50, 9}, {20, 8}, {10,10 },{5, 9} };
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Withdrawing 895 from a machine with ten of each bill should change the machine accordingly"
            );
        }

        [Test]
        public void TestNotEnoughWithdraw()
        {
            CashMachine atm = new CashMachine();
            atm.AddCash(10, 1);
            atm.AddCash(5, 1);

            Dictionary<Int32, Int32> withdrawedCash = new Dictionary<Int32, Int32> {
                {10, 1}, {5, 1}};
            Assert.AreEqual(withdrawedCash, atm.Withdraw(40),
                "Withdrawing 40 from a machine with one 10 and one 5 should get you one 10 and one 5"
            );
            Dictionary<Int32, Int32> remainingCash = new Dictionary<Int32, Int32> {
                {500, 0}, {200, 0}, {100, 0}, {50, 0}, {20, 0}, {10, 0}, {5, 0} };
            Assert.AreEqual(remainingCash, atm.RemainingCash,
                "Withdrawing 40 from a machine with one 10 and one 5 should left the machine empty (0 of each bills)"
            );
        }
    }
}