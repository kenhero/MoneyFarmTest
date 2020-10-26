using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace MoneyFarmTest
{


    [TestFixture]
    class HomeBanking
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            HomeBanking test = new HomeBanking();
            test.InitAccount();

            test.TestMoneyFarm();
            test.TestTearDown();

        }

        [SetUp]
        public void InitAccount()
        {
           
        }

        [Test]
        public void TestMoneyFarm()
        {
            try
            {
                Console.WriteLine("There are several test with NUnit framework and Log4net framework in order to save info about failed test and why \n\n");
                Console.WriteLine("Press Enter \n");
                Console.ReadLine();
                Assert.Multiple(() =>

                {

                    // Use ProcessStartInfo class
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = @"C:\Windows\System32\curl.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    Console.Clear();
                    //Curl 200
                    Console.WriteLine("Test 200 Response Code \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 200\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";


                    //Process.Start(startInfo);


                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }

                    //dynamic obj = JsonConvert.DeserializeObject("risposta.json");
                    //var res = JsonConvert.DeserializeObject(File.ReadAllText(@"D:\MoneyFarm\MoneyFarmTest\MoneyFarmTest\bin\x64\Debug\response.json"));
                    //Console.WriteLine(res);

                    Root root = JsonConvert.DeserializeObject<Root>(File.ReadAllText(@".\response.json"));


                    for (int i = 0; i < root.availablePortfolios.Count; i++)
                    {
                        Assert.That(root.availablePortfolios[i].modelPortfolioId, Does.Contain("mfm-it").IgnoreCase);
                        Assert.That(root.availablePortfolios[i].riskLevel, Is.InRange(1, 7));
                        Assert.That(root.availablePortfolios[i].status, Does.Contain("suitable").IgnoreCase);
                    }




                    Console.Clear();
                    Console.WriteLine("Test 400 Bad Request Response Code without productId Parameter inside the URL \n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    //Curl  400 Bad Request without productId
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    BadRequest badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);
                  
                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("productId"));



                    Console.Clear();
                    //Curl  400 Bad Request without goal
                    Console.WriteLine("Test 400 Bad Request Response Code without goal Parameter inside the URL \n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("goal"));



                    Console.Clear();
                    //Curl  400 Bad Request without timeHorizonYears
                    Console.WriteLine("Test 400 Bad Request Response Code without timeHorizonYears Parameter inside the URL \n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("timeHorizonYears"));




                    Console.Clear();
                    //Curl  400 Bad Request without contributionMonthlyAmount
                    Console.WriteLine("Test 400 Bad Request Response Code without contributionMonthlyAmount Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("contributionMonthlyAmount"));



                    Console.Clear();
                    //Curl  400 Bad Request without investmentType
                    Console.WriteLine("Test 400 Bad Request Response Code without investmentType Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("investmentType"));


                    Console.Clear();
                    //Curl  400 Bad Request without contributionOneOffAmount
                    Console.WriteLine("Test 400 Bad Request Response Code without contributionOneOffAmount Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("contributionOneOffAmount"));


                    Console.Clear();
                    //Curl  400 Bad Request without incomeSource
                    Console.WriteLine("Test 400 Bad Request Response Code without incomeSource Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("incomeSource"));


                    Console.Clear();
                    //Curl  400 Bad Request without annualIncome
                    Console.WriteLine("Test 400 Bad Request Response Code without annualIncome Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("annualIncome"));



                    Console.Clear();
                    //Curl  400 Bad Request without totalAssets
                    Console.WriteLine("Test 400 Bad Request Response Code without totalAssets Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("totalAssets"));


                    Console.Clear();
                    //Curl  400 Bad Request without annualSavingsPercentage
                    Console.WriteLine("Test 400 Bad Request Response Code without annualSavingsPercentage Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("annualSavingsPercentage"));


                    Console.Clear();
                    //Curl  400 Bad Request without financialExperienceScore
                    Console.WriteLine("Test 400 Bad Request Response Code without financialExperienceScore Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("financialExperienceScore"));


                    Console.Clear();
                    //Curl  400 Bad Request without riskProfileScore
                    Console.WriteLine("Test 400 Bad Request Response Code without riskProfileScore Parameter inside the URL\n");
                    Console.WriteLine("It should says me exactly what is the missed parameter \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth&productId=gia-discretionary\" -H \"x-mock-response-code: 400\"  " + "-H" + "  \"Authorization: Bearer bv\"  --output " + "response.json ";
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }



                    badRequest = JsonConvert.DeserializeObject<BadRequest>(File.ReadAllText(@".\response.json"));

                    Assert.That(badRequest.type, Is.EqualTo("invalid-parameters").IgnoreCase);
                    //Console.WriteLine(badRequest.title);
                    //Console.WriteLine(badRequest.type);

                    //Console.WriteLine(badRequest.invalidParams[0].name);
                    Assert.That(badRequest.invalidParams[0].name, Is.EqualTo("riskProfileScore"));


                    Console.Clear();
                    //Curl  401
                    Console.WriteLine("Test 401 Unauthorized Response Code  \n");
                    Console.WriteLine("Press Enter \n");
                    Console.ReadLine();
                    startInfo.Arguments = "--location --request GET \"https://8b1afad2-2593-457b-9050-e6896a6746f5.mock.pstmn.io/simulations?riskProfileScore=0.5&financialExperienceScore=0.5&annualSavingsPercentage=0.15&totalAssets=200000&annualIncome=27500.00&age=47&incomeSource=full-time&contributionOneOffAmount=100&investmentType=gia&contributionMonthlyAmount=10000&timeHorizonYears=40&goal=grow-wealth\" -H \"x-mock-response-code: 401\"    --output " + "response.json ";

                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }

                    Unauthorized unauthorized = JsonConvert.DeserializeObject<Unauthorized>(File.ReadAllText(@".\response.json"));

                    //Console.WriteLine(unauthorized.title);
                    Assert.That(unauthorized.title, Is.EqualTo("The requester credentials are invalid."));
                    //Console.WriteLine(unauthorized.type);
                    Assert.That(unauthorized.type, Is.EqualTo("https://api.moneyfarm.com/errors/invalid-credentials"));



                });

            }
            catch (Exception e)
            {
               
                log.Fatal("Test exception: ", e);

            }
        }

        [TearDown]
        public void TestTearDown()
        {
           

            Console.WriteLine("TEST FINISHED, PRESS ENTER TO EXIT");
            Console.ReadLine();
        }
    }

   
    public class AvailablePortfolio
    {
        public string modelPortfolioId { get; set; }
        public int riskLevel { get; set; }
        public string status { get; set; }

        public AvailablePortfolio() { }

        public AvailablePortfolio(string _modelPortfolioId, int _riskLevel, string _status)
        {
            modelPortfolioId = _modelPortfolioId;
            riskLevel = _riskLevel;
            status = _status;
        }
    }

    public class Root
    {
        public List<AvailablePortfolio> availablePortfolios { get; set; }
    }


    public class InvalidParam
    {
        public string name { get; set; }
        public string reason { get; set; }
    }

    public class BadRequest
    {
        public List<InvalidParam> invalidParams { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class Unauthorized
    {  
        public string title { get; set; }
        public string type { get; set; }
    }
    
}
