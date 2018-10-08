// Copyright (c) 2018, BrandonT42, The TurtleCoin Developers
//
// Please see the included LICENSE file for more information.

using System;
using System.Diagnostics;
using System.Threading;

namespace RelaunchUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sleeping 5 seconds.");
            Thread.Sleep(5000);
            Console.WriteLine("Relaunching application");
            Process.Start(args[0]);
        }
    }
}
