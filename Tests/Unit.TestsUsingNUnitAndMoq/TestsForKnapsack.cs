using App.models;
using App.utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Unit.TestsUsingNUnitAndMoq
{
    public class TestsForKnapsack
    {
        private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SharedFiles", "knapsack-items.csv");
        private Mock<IFileIOWrapper> fileIOWrapper;
        private Mock<IKnapsack> knapsack;

        [SetUp]
        public void Setup()
        {
            fileIOWrapper = new Mock<IFileIOWrapper>();
            fileIOWrapper.Setup(fiow => fiow.FilePath).Returns(filePath);
            Assert.IsNotNull(fileIOWrapper.Object);

            knapsack = new Mock<IKnapsack>();
            knapsack.Setup(knapsack => knapsack.MaximumWeight).Returns(4000);
            knapsack.Setup(knapsack => knapsack.KnapsackItems).Returns(fileIOWrapper.Object.ReadAllLines().Skip(1).Select(knapsackItem => new KnapsackItem(knapsackItem)).Cast<IKnapsackItem>().ToList());
            knapsack.Setup(knapsack => knapsack.KnapsackContents).Returns(new List<IKnapsackItem>());
            Assert.IsNotNull(knapsack.Object);
        }

        [Test]
        public void success_Test_when_Knapsack_AddItems_IsCalled()
        {
            knapsack.Object.AddItems();
            Assert.IsTrue(knapsack.Object.KnapsackContents.HasItems());
        }
        public void success_Test_when_Knapsack_EmptyKnapsack_IsCalled()
        {
            knapsack.Object.EmptyKnapsack();
            Assert.IsFalse(knapsack.Object.KnapsackContents.HasItems());
        }
    }
}