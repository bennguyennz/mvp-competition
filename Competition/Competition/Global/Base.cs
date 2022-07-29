using AventStack.ExtentReports;
using Competition.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Competition.Global.GlobalDefinitions;

namespace Competition.Global
{
    class Base
    {
        #region To access Path from resource file
        public static int Browser = 2;
        public static string excelPath = @"D:\workspace\mvpstudio-competition\Competition\Competition\TestLibrary\TestData.xlsx";
        public static string ScreenshotPath = "";
        public static string ReportPath = "";
        public static string IsLogin = "true";
        #endregion

        #region reports
        public static ExtentTest test;
        public static ExtentReports extent;

        public static string ExcelPath { get => excelPath; set => excelPath = value; }
        #endregion


        #region setup and tear down
        [SetUp]
        public void Inititalize()
        {

            switch (Browser)
            {

                case 1:
                    GlobalDefinitions.driver = new FirefoxDriver();
                    break;
                case 2:
                    GlobalDefinitions.driver = new ChromeDriver();
                    GlobalDefinitions.driver.Manage().Window.Maximize();
                    break;
            }

            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignIn");
            GlobalDefinitions.driver.Navigate().GoToUrl(ExcelLib.ReadData(2, "Url"));

            #region Initialise Reports
            // start reporters
            //var htmlReporter = new ExtentHtmlReporter(MarsResource.ReportPath + "ExtentReport.html");
            ////htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //var extent = new ExtentReports();
            //extent.AttachReporter(htmlReporter);


            ////htmlReporter.LoadConfig(MarsResource.ReportXMLPath+"//extent-config.xml");

            #endregion

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
            //    //var test = extent.CreateTest("MyFirstTest", "Sample description");
            //    // Screenshot
            //    String img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report");
            //    //AddScreenCapture(@"E:\Dropbox\VisualStudio\Projects\Beehive\TestReports\ScreenShots\");
            //    test.Log(Status.Info, "Image example: " + img);

            //    // log with snapshot
            //    test.Fail("details", MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());

            //    // test with snapshot
            //    test.AddScreenCaptureFromPath("screenshot.png");

            //    // end test. (Reports)
            //    //extent.endTest(test);
            //    // calling Flush writes everything to the log file (Reports)
            //    extent.Flush();
            // Close the driver :)            
            GlobalDefinitions.driver.Close();
            GlobalDefinitions.driver.Quit();
        }
        #endregion
    }

}
