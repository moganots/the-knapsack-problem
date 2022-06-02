using App.utilities;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace Unit.TestsUsingNUnitAndMoq
{
    public class Tests
    {
        private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SharedFiles", "knapsack-items.csv");
        private Mock<IFileIOWrapper> fileIOWrapper;

        [SetUp]
        public void Setup()
        {
            fileIOWrapper = new Mock<IFileIOWrapper>();
            fileIOWrapper.Setup(fiow => fiow.FilePath).Returns(filePath);
            Assert.IsNotNull(fileIOWrapper);
        }

        [Test]
        public void success_Test_when_FileIOWrapper_FileExists_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.Object.FileExists());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_FileInUse_IsCalled()
        {
            Assert.IsFalse(fileIOWrapper.Object.FileInUse());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_ReadAllText_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.Object.ReadAllText().IsSet());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_ReadAllLines_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.Object.ReadAllLines().HasItems());
        }
    }
}