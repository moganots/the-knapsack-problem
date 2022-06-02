using App.utilities;
using NUnit.Framework;
using System;
using System.IO;

namespace Unit.TestsUsingNUnit
{
    public class TestsForFileIOWrapper
    {
        private string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SharedFiles", "knapsack-items.csv");
        private IFileIOWrapper fileIOWrapper;

        [SetUp]
        public void Setup()
        {
            fileIOWrapper = new FileIOWrapper(filePath);
            Assert.IsNotNull(fileIOWrapper);
        }

        [Test]
        public void success_Test_when_FileIOWrapper_FileExists_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.FileExists());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_FileInUse_IsCalled()
        {
            Assert.IsFalse(fileIOWrapper.FileInUse());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_ReadAllText_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.ReadAllText().IsSet());
        }

        [Test]
        public void success_Test_when_FileIOWrapper_ReadAllLines_IsCalled()
        {
            Assert.IsTrue(fileIOWrapper.ReadAllLines().HasItems());
        }
    }
}