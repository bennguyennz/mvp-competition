using ExcelDataReader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.Global
{
    class GlobalDefinitions
    {
        //Initialise driver
        public static IWebDriver driver { get; set; }

        #region WaitforElement 

        public static void wait(int second)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(second);
        }
        public static IWebElement WaitForElement(IWebDriver driver, By ByAndlocator, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ByAndlocator)));
        }
        #endregion

        #region Excel 
        public class ExcelLib
        {
            static List<Datacollection> dataCol = new List<Datacollection>();

            public class Datacollection
            {
                public int rowNumber { get; set; }
                public string colName { get; set; }
                public string colValue { get; set; }
            }

 
            public static void ClearData()
            {
                dataCol.Clear();
            }


            public static DataTable ExcelToDataTable(string fileName, string SheetName)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                // Open file and return as Stream
                using (System.IO.FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
                    {
                        //excelReader.IsFirstRowAsColumnNames = true;

                        //Return as dataset
                        var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {

                                UseHeaderRow = true
                            }
                        });
                        
                        //Get all the tables
                        DataTableCollection table = result.Tables;

                        // store it in data table
                        DataTable resultTable = table[SheetName];

                        // return
                        return resultTable;
                    }
                }
            }

            public static string ReadData(int rowNumber, string columnName)
            {
                try
                {
                    //Retrieving Data using LINQ to reduce much of iterations

                    rowNumber = rowNumber - 1;
                    string data = (from colData in dataCol
                                   where colData.colName == columnName && colData.rowNumber == rowNumber
                                   select colData.colValue).SingleOrDefault();

                    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;


                    return data.ToString();
                }

                catch (Exception e)
                {
                    //Added by Kumar
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                    return null;
                }
            }

            public static void PopulateInCollection(string fileName, string SheetName)
            {
                ExcelLib.ClearData();
                DataTable table = ExcelToDataTable(fileName, SheetName);

                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = table.Columns[col].ColumnName,
                            colValue = table.Rows[row - 1][col].ToString()
                        };

                        //Add all the details for each row
                        dataCol.Add(dtTable);

                    }
                }

            }
        }

        #endregion

        #region screenshots
        public class Screenshot
        {
            public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName)
            {
                if (!System.IO.Directory.Exists(Base.ScreenshotPath))
                {
                    System.IO.Directory.CreateDirectory(Base.ScreenshotPath);
                }

                var screenShot = ((ITakesScreenshot)GlobalDefinitions.driver).GetScreenshot();
                var fileName = new StringBuilder(Base.ScreenshotPath + ScreenShotFileName + DateTime.Now.ToString("_dd-MM-yyyy_HHmm") + ".jpeg");
                screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Jpeg);
                return fileName.ToString();
            }

            public static string GetScreenshot()
            {
                return ((ITakesScreenshot)GlobalDefinitions.driver).GetScreenshot().AsBase64EncodedString;
            }

        }
        

        #endregion

       
    }
}

