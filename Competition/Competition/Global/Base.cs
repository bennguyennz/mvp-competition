using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Competition.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Competition.Global.GlobalDefinitions;
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace Competition.Global
{
    class Base
    {
        #region Constant configuration
        public static int Browser = 2;
        public static string IsLogin = "true";

        public static string excelPath = @"D:\workspace\mvpstudio-competition\Competition\Competition\TestLibrary\TestData\TestData.xlsx";
        public static string AutoScriptPath = @"D:\workspace\mvpstudio-competition\Competition\Competition\TestLibrary\TestData\UploadScript.exe";
        public static string ScreenshotPath = @"D:\workspace\mvpstudio-competition\Competition\Competition\TestLibrary\Screenshots\";
        public static string ReportPath = @"D:\workspace\mvpstudio-competition\Competition\Competition\TestLibrary\Reports\";
        #endregion

        public static string ExcelPath { get => excelPath; set => excelPath = value; }

        #region reports
        public static AventStack.ExtentReports.ExtentReports extent;
        public static AventStack.ExtentReports.ExtentTest test;

        #endregion

        #region setup and tear down
        [OneTimeSetUp]
        protected void ExtentStart()
        {   
            //Initialize report
            string reportName = System.IO.Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "TestLibrary/TestReports"
            + Path.DirectorySeparatorChar + "Report_" + DateTime.Now.ToString("_dd-MM-yyyy_HHmm") + Path.DirectorySeparatorChar;

            //start reporters
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportName);
            htmlReporter.Config.DocumentTitle = "Automation Report";//Title of the report
            htmlReporter.Config.ReportName = "Functional Report"; //Name of the report.
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new AventStack.ExtentReports.ExtentReports();

            extent.AttachReporter(htmlReporter);
        }

        [SetUp]
        public void Inititalize()
        {

            switch (Browser)
            {

                case 1:
                    driver = new FirefoxDriver();
                    break;
                case 2:
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    break;
            }

            //Load excel into collection
            ExcelLib.PopulateInCollection(Base.excelPath, "SignIn");

            //Open URL
            driver.Navigate().GoToUrl(ExcelLib.ReadData(2, "Url"));

            if (IsLogin == "true")
            {
                SignIn obj = new SignIn();
                obj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.Register();
            }

        }

        [TearDown]
        public void TearDown()
        {
            // Screenshot
            String img = Screenshot.SaveScreenshot(GlobalDefinitions.driver, "Screenshot");

            // log with snapshot
            var exec_status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            
            string TC_Name = TestContext.CurrentContext.Test.Name;
            string base64 = Screenshot.GetScreenshot();

            Status logStatus = Status.Pass;
            switch (exec_status)
            {
                case TestStatus.Failed:

                    logStatus = Status.Fail;
                    test.Log(Status.Fail, exec_status + errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64).Build());
                    break;

                case TestStatus.Skipped:

                    logStatus = Status.Skip;
                    test.Log(Status.Skip, errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64).Build());
                    break;

                case TestStatus.Inconclusive:
                
                    logStatus = Status.Warning;
                    test.Log(Status.Warning, "Test ");
                    break;

                case TestStatus.Passed:

                    logStatus = Status.Pass;
                    test.Log(Status.Pass, "Test Passed");
                    break;

                default:    
                    break;
            }
           
            // Close the driver            
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        protected void ExtentClose()
        {
            // calling Flush writes everything to the log file (Reports)
            extent.Flush();
        }
        #endregion
    }
}
