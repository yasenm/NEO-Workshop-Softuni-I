using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace NeoContract1
{
    public class Contract1 : SmartContract
    {
        public static readonly byte[] Owner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();

        public const int TotalSupply = 1000_0000;

        public static object Main(string method, params object[] args)
        {
            if (method == "totalSupply")
            {
                return TotalSupply;
            }
            if (method == "name")
            {
                return "Worshop Demo Token";
            }
            if (method == "symbol")
            {
                return "WDT";
            }
            if (method == "decimals")
            {
                return 4;
            }
            if (method == "initialize")
            {
                Init();
            }
            if (method == "balanceOf")
            {
                return BalanceOf((byte[])args[0]);
            }
            if (method == "transfer")
            {
                return Transfer((byte[])args[0], (byte[])args[1], (BigInteger)args[2]);
            }

            return false;
        }

        public static object Transfer(byte[] from, byte[] to, BigInteger amount)
        {
            if (from.Length != 20)
            {
                throw new Exception("From address check failed");
            }
            if (to.Length != 20)
            {
                throw new Exception("To address check failed");
            }
            if (amount < 0)
            {
                throw new Exception("Amount less than zero");
            }

            if (amount == 0 || from == to)
            {
                Runtime.Notify("transfer", from, to, amount);
                return false;
            }

            var fromBalance = Storage.Get(Storage.CurrentContext, from).AsBigInteger();

            if (fromBalance < amount)
            {
                return false;
            }

            if (Runtime.CheckWitness(from))
            {
                var toBalance = Storage.Get(Storage.CurrentContext, to).AsBigInteger();
                fromBalance -= amount;
                toBalance += amount;

                Storage.Put(Storage.CurrentContext, from, fromBalance);
                Storage.Put(Storage.CurrentContext, to, toBalance);
                Runtime.Notify("transfer", from, to, amount);
                return true;
            }

            return false;
        }

        public static BigInteger BalanceOf(byte[] address)
        {
            if (address.Length != 20)
            {
                throw new Exception("Address check failed BalanceOf");
            }

            return Storage.Get(Storage.CurrentContext, address).AsBigInteger();
        }

        public static void Init()
        {
            if (Runtime.CheckWitness(Owner))
            {
                var initialized = Storage.Get(Storage.CurrentContext, "initialized");
                if (initialized.Length == 0)
                {
                    Storage.Put(Storage.CurrentContext, "initialized", "true");
                    Storage.Put(Storage.CurrentContext, Owner, TotalSupply);
                    Runtime.Notify("transfer", null, Owner, TotalSupply);
                }
            }
        }
    }
}
