namespace Rabobank.Training.WebApp.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;
    using NSubstitute;
    using FluentAssertions;
    using Rabobank.Training.Controllers;
    using Rabobank.Training.ClassLibrary;


    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public async Task TestMethod()
        {

            var fundService = Substitute.For<FundMethod>();
            var portFolioController = Substitute.For<PortfolioController>(fundService);
            var result = await portFolioController.Get();
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task TestMethod1()
        {

            var fundService = Substitute.For<FundMethod>();
            var portFolioController = Substitute.For<PortfolioController>(fundService);
            var result = await portFolioController.Get();
            result.Should().BeOfType(typeof(PortfolioVM));
        }
    }
}
