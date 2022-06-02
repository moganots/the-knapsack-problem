using App.models;
using App.utilities;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SharedFiles", "knapsack-items.csv");
            IFileIOWrapper fileIOWrapper = new FileIOWrapper(filePath);
            IKnapsack knapsack = new Knapsack(4000, fileIOWrapper);
            knapsack.AddItems();
        }
    }
}
