

namespace Rabobank.Training.ClassLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;
    using Rabobank.Training.ClassLibrary;
    using System.IO;
    using System.Threading.Tasks;
    using System.Reflection;

    [TestClass]
    public class UnitTest
    {
        IFundsOfMandates mandateMethod;
        [TestMethod]
        public async Task TestMethod()
        {
            mandateMethod = new FundMethod();
            var result = await mandateMethod.ReadFundOfMandatesFile();
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task TestMethod1()
        {
            mandateMethod = new FundMethod();
            var result = await mandateMethod.GetPortfolio();
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task TestMethod2()
        {
            mandateMethod = new FundMethod();
            var fund = await mandateMethod.ReadFundOfMandatesFile();
            var portFolio = await mandateMethod.GetPortfolio();
            if (fund != null && portFolio != null)
            {
                for (int i = 0; i < portFolio.Positions.Count; i++)
                {
                    foreach (var mandate in fund.FundsOfMandates)
                    {
                        portFolio.Positions[i] = await mandateMethod.GetCalculatedMandates(portFolio.Positions[i], mandate);
                    }
                }
            }
            portFolio.Should().NotBeNull();
        }
    }
}
