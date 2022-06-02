using App.models;
using App.utilities;
using NUnit.Framework;
using System;
using System.IO;

namespace Unit.TestsUsingNUnit
{
    public class TestsForKnapsack
    {
        private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SharedFiles", "knapsack-items.csv");
        private IFileIOWrapper fileIOWrapper;
        private IKnapsack knapsack;

        [SetUp]
        public void Setup()
        {
            fileIOWrapper = new FileIOWrapper(filePath);
            Assert.IsNotNull(fileIOWrapper);

            knapsack = new Knapsack(4000, fileIOWrapper);
            Assert.IsNotNull(knapsack);
        }

        [Test]
        public void success_Test_when_Knapsack_AddItems_IsCalled()
        {
            knapsack.AddItems();
            Assert.IsTrue(knapsack.KnapsackContents.HasItems());
        }
        public void success_Test_when_Knapsack_EmptyKnapsack_IsCalled()
        {
            knapsack.EmptyKnapsack();
            Assert.IsFalse(knapsack.KnapsackContents.HasItems());
        }
    }
}