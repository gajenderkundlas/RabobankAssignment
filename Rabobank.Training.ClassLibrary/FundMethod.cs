namespace Rabobank.Training.ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using System.Linq;
    public class FundMethod : IFundsOfMandates
    {
        public async Task<FundsOfMandatesData> ReadFundOfMandatesFile()
        {
            try
            {
                var filePath = Directory.GetCurrentDirectory() + "\\TestData\\FundsOfMandatesData.xml";
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                FundsOfMandatesData fundOfMandatesObj = new FundsOfMandatesData();
                XmlSerializer serializer = new XmlSerializer(typeof(FundsOfMandatesData));
                MemoryStream memStream = new MemoryStream(file);
                fundOfMandatesObj = (FundsOfMandatesData)serializer.Deserialize(memStream);
                return await Task.Run(() => { return fundOfMandatesObj; });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<PortfolioVM> GetPortfolio()
        {
            try
            {
                PortfolioVM portVMObj = new PortfolioVM();
                List<PositionVM> positionObj = new List<PositionVM>();
                positionObj.Add(new PositionVM
                {
                    Code = "NL0000009165",
                    Name = "Heineken",
                    Value = 12345
                });
                positionObj.Add(new PositionVM
                {
                    Code = "NL0000287100",
                    Name = "Optimix Mix Fund",
                    Value = 23456
                });
                positionObj.Add(new PositionVM
                {
                    Code = "LU0035601805",
                    Name = "DP Global Strategy L High",
                    Value = 34567
                });
                positionObj.Add(new PositionVM
                {
                    Code = "NL0000292332",
                    Name = "Rabobank Core Aandelen Fonds T2",
                    Value = 45678

                });
                positionObj.Add(new PositionVM
                {
                    Code = "LU0042381250",
                    Name = "Morgan Stanley Invest US Gr Fnd",
                    Value = 56789
                });
                portVMObj.Positions = positionObj;
                return await Task.Run(() => { return portVMObj; });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<PositionVM> GetCalculatedMandates(PositionVM posObj, FundOfMandates fundobj)
        {
            try
            {
                if (posObj.Code == fundobj.InstrumentCode)
                {
                    List<MandateVM> MandateListObj = new List<MandateVM>();
                    foreach (var mandate in fundobj.Mandates)
                    {
                        MandateVM MandateObj = new MandateVM();
                        MandateObj.Name = mandate.MandateName;
                        MandateObj.Allocation = mandate.Allocation;
                        MandateObj.Value = Math.Round(posObj.Value * (mandate.Allocation / 100));
                        MandateListObj.Add(MandateObj);
                    }
                    posObj.Mandates = MandateListObj;
                    if (fundobj.LiquidityAllocation > 0)
                    {
                        MandateVM Liquidity = new MandateVM();
                        Liquidity.Name = "Liquidity";
                        Liquidity.Allocation = fundobj.LiquidityAllocation;
                        Liquidity.Value = Math.Round((posObj.Value - MandateListObj.Select(x => x.Value).Sum()) * (fundobj.LiquidityAllocation));
                        MandateListObj.Add(Liquidity);
                    }
                }
                return await Task.Run(() => { return posObj; });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
