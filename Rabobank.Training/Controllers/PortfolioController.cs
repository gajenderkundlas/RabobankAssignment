

namespace Rabobank.Training.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Rabobank.Training.ClassLibrary;


    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        IFundsOfMandates fundMethod;
        
        public PortfolioController(IFundsOfMandates fundObj) {
            fundMethod = fundObj;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<PortfolioVM> Get()
        {
            try
            {
                var fund = await fundMethod.ReadFundOfMandatesFile();
                var portFolio = await fundMethod.GetPortfolio();
                if (fund != null && portFolio != null)
                {
                    for (int i = 0; i < portFolio.Positions.Count; i++)
                    {
                        foreach (var mandate in fund.FundsOfMandates)
                        {
                            portFolio.Positions[i] = await fundMethod.GetCalculatedMandates(portFolio.Positions[i], mandate);
                        }
                    }
                }
                return portFolio;
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
    }
}
