using Competition.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Competition.Global.GlobalDefinitions;
using static Competition.Global.Base;

namespace Competition.Pages
{
    internal class ShareSkill
    {
        #region Page Objects for EnterShareSkill
        //ShareSkill Button
        private IWebElement btnShareSkill => driver.FindElement(By.LinkText("Share Skill"));

        //Title textbox
        private IWebElement Title => driver.FindElement(By.Name("title"));

        //Description textbox
        private IWebElement Description => driver.FindElement(By.Name("description"));

        //Category Dropdown
        private IWebElement CategoryDropDown => driver.FindElement(By.Name("categoryId"));

        //SubCategory Dropdown
        private IWebElement SubCategoryDropDown => driver.FindElement(By.Name("subcategoryId"));

        //Tag names textbox
        private IWebElement Tags => driver.FindElement(By.XPath("//form[@class='ui form']/div[4]/div[2]/div/div/div/div/input"));

        //Entered displayed Tags
        private IList<IWebElement> displayedTags => driver.FindElements(By.XPath("//form[@class='ui form']/div[4]/div[2]/div/div/div/span/a"));
        //form[@class='ui form']/div[4]/div[2]/div/div/div/span/a

        //Service type radio button
        private IList<IWebElement> radioServiceType => driver.FindElements(By.Name("serviceType"));

        //Location Type radio button
        private IList <IWebElement> radioLocationType => driver.FindElements(By.Name("locationType"));

        //Start Date dropdown
        private IWebElement StartDateDropDown => driver.FindElement(By.Name("startDate"));

        //End Date dropdown
        private IWebElement EndDateDropDown => driver.FindElement(By.Name("endDate"));

        //Available days
        private IList <IWebElement> Days => driver.FindElements(By.XPath("//input[@name='Available']"));

        //Starttime
        private IList <IWebElement> StartTime => driver.FindElements(By.Name("StartTime"));

        //EndTime
        private IList<IWebElement> EndTime => driver.FindElements(By.Name("EndTime"));
        

        //StartTime dropdown
        private IWebElement StartTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //EndTime dropdown
        private IWebElement EndTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[3]/input[1]"));

        //Skill Trade option
        private IList <IWebElement> radioSkillTrade => driver.FindElements(By.Name("skillTrades"));

        //Skill Exchange
        private IWebElement SkillExchange => driver.FindElement(By.XPath("//div[@class='form-wrapper']//input[@type='text']"));
        private IList<IWebElement> skillExchangeTags => driver.FindElements(By.XPath("//form[@class='ui form']/div[8]/div[4]/div/div/div/div/span/a"));
        

        //Credit textbox
        private IWebElement CreditAmount => driver.FindElement(By.XPath("//input[@placeholder='Amount']"));

        //Work Samples button
        private IWebElement btnWorkSamples => driver.FindElement(By.XPath("//i[@class='huge plus circle icon padding-25']"));

        //Active option
        private IList <IWebElement> radioActive => driver.FindElements(By.XPath("//input[@name='isActive']"));
       
        //Save button
        private IWebElement Save => driver.FindElement(By.XPath("//input[@value='Save']"));
        #endregion

        #region Page Objects for ValidateShareSkill
         //Title
        private IWebElement actualTitle => driver.FindElement(By.XPath("//span[@class='skill-title']"));

        //Description
        private IWebElement actualDescription => driver.FindElement(By.XPath("//div[text()='Description']//following-sibling::div"));

        //Category
        private IWebElement actualCategory => driver.FindElement(By.XPath("//div[text()='Category']//following-sibling::div"));

        //Subcategory
        private IWebElement actualSubcategory => driver.FindElement(By.XPath("//div[text()='Subcategory']//following-sibling::div"));

        //Service Type
        private IWebElement actualServiceType => driver.FindElement(By.XPath("//div[text()='Service Type']//following-sibling::div"));

        //Start Date
        private IWebElement actualStartDate => driver.FindElement(By.XPath("//div[text()='Start Date']//following-sibling::div"));

        //End Date
        private IWebElement actualEndDate => driver.FindElement(By.XPath("//div[text()='End Date']//following-sibling::div"));

        //Location Type
        private IWebElement actualLocationType => driver.FindElement(By.XPath("//div[text()='Location Type']//following-sibling::div"));

        //Skill Trade
        private IWebElement actualSkillsTrade => driver.FindElement(By.XPath("//div[text()='Skills Trade']//following-sibling::div"));

        //Skill Exchange
        private IWebElement actualSkillExchange => driver.FindElement(By.XPath("//div[text()='Skills Trade']//following-sibling::div/span"));
        #endregion


        //Filling Share-Skill details
        public void EnterShareSkill(int rowNumber, string worksheet)
        {
            //var node = test.CreateNode("Step 1. Create listing");
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Enter Title 
            Title.SendKeys(ExcelLib.ReadData(rowNumber, "Title"));

            //Enter Description
            Description.SendKeys(ExcelLib.ReadData(rowNumber, "Description"));

            //Select category
            var selectCategory = new SelectElement(CategoryDropDown);
            selectCategory.SelectByText(ExcelLib.ReadData(rowNumber, "Category"));

            //Select Subcategory
            var selectSubcategory = new SelectElement(SubCategoryDropDown);
            selectSubcategory.SelectByText(ExcelLib.ReadData(rowNumber, "Subcategory"));

            //Enter tag
            Tags.Click();
            Tags.SendKeys(ExcelLib.ReadData(rowNumber, "Tags"));
            Tags.SendKeys(Keys.Return);

            //Select Service type
            string expectedServiceType = ExcelLib.ReadData(rowNumber, "ServiceType");
            string expectedServiceValue = "0";
            if (expectedServiceType.Equals("One-off service"))
                expectedServiceValue = "1";

            for (int i = 0; i < radioServiceType.Count(); i++)
            {
                string actualServiceValue = radioServiceType[i].GetAttribute("Value");
                if (expectedServiceValue.Equals(actualServiceValue))
                {
                    radioServiceType[i].Click();
                }
            }

            //Select Location type
            string expectedLocationType = ExcelLib.ReadData(rowNumber, "LocationType");
            
            string expectedLocationValue = "0";
            if (expectedLocationType.Equals("Online"))
                expectedLocationValue = "1";
                
            for (int i=0; i < radioLocationType.Count(); i++)
            {
                string actualLocationValue = radioLocationType[i].GetAttribute("Value");
                if (expectedLocationValue.Equals(actualLocationValue))
                {
                    radioLocationType[i].Click();
                }
            }

            //Enter Start date
            StartDateDropDown.SendKeys(ExcelLib.ReadData(rowNumber, "StartDate"));

            //Enter End date
            EndDateDropDown.SendKeys(ExcelLib.ReadData(rowNumber, "EndDate"));
            EndDateDropDown.SendKeys(Keys.Enter);

            //Enter available Days
            string expectedDays = ExcelLib.ReadData(rowNumber, "Days");
            string indexValue = "";

            switch (expectedDays)
            {
                case "Sun": 
                    indexValue = "0";
                    break;
                case "Mon":
                    indexValue = "1";
                    break;
                case "Tue":
                    indexValue = "2";
                    break;
                case "Wed":
                    indexValue = "3";
                    break;
                case "Thu":
                    indexValue = "4";
                    break;
                case "Fri":
                    indexValue = "5";
                    break;
                case "Sat":
                    indexValue = "6";
                    break;
                default:
                    Assert.Ignore("Day is invalid.");
                    break;
            }

            for (int i = 0; i < Days.Count; i++)
            {
                if (indexValue.Equals(Days[i].GetAttribute("index")))
                { 
                    Days[i].Click();
                    string startTime = ExcelLib.ReadData(rowNumber, "StartTime");
                    string endTime = ExcelLib.ReadData(rowNumber, "EndTime");
                    StartTime[i].SendKeys(startTime);
                    EndTime[i].SendKeys(endTime);
                }
            }
            wait(1);

            //Select "Skill Trade" options
            string expectedSkillTrade = ExcelLib.ReadData(rowNumber, "SkillTradeOption");
            string expectedSkillValue = "true";

            if(expectedSkillTrade.Equals("Credit"))
                expectedSkillValue = "false";

            for (int i = 0; i < radioSkillTrade.Count(); i++)
            {
                string actualSkillTradeValue=radioSkillTrade[i].GetAttribute("value");
                if (expectedSkillValue.Equals(actualSkillTradeValue))
                {
                    //Select "Skill exchange" or "Credit"
                    radioSkillTrade[i].Click();
                    wait(1);

                    if (expectedSkillTrade.Equals("Skill-exchange"))
                    //Enter tags for Skill-exchange
                    {
                        SkillExchange.Click();
                        SkillExchange.SendKeys(ExcelLib.ReadData(rowNumber, "SkillExchange"));
                        SkillExchange.SendKeys(Keys.Return);
                    }
                    else
                    {
                        //Enter Credit amount
                        CreditAmount.SendKeys(ExcelLib.ReadData(rowNumber, "CreditAmount"));
                    }
                }               
            }

            //Click button Upload Work Samples
            btnWorkSamples.Click();
            wait(3);

            //Run AutoIT-script to execute file uploading
            using (Process exeProcess = Process.Start(Base.AutoScriptPath))
            {
                exeProcess.WaitForExit();
            }

            wait(3);

            //Select ActiveOption
            string expectedActiveOption = ExcelLib.ReadData(rowNumber, "ActiveOption");

            string expectedActiveValue = "true";
            if (expectedActiveOption.Equals("Hidden"))
                expectedActiveValue = "false";

            for (int i=0; i< radioActive.Count(); i++)
            {
                string actualActiveValue = radioActive[i].GetAttribute("Value");
                if (expectedActiveValue.Equals(actualActiveValue))
                    radioActive[i].Click();
            }

            //Click on Save
            Save.Click();
            wait(3);
            //node.Pass("Step 1 is Passed");
        }
        public void CreateMultipleShareSkill(string worksheet)
        {
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            int rowNumber = 2;
            string title = ExcelLib.ReadData(rowNumber, "Title");
            while (title != "null")
            {
                EnterShareSkill(rowNumber, worksheet);
                rowNumber++;
                title = ExcelLib.ReadData(rowNumber, "Title");                
            }
        }

        public void CreateListing_InvalidData(int rowNumber, string worksheet)
        { 
            
        
        }

        //Assertions on ShareSkill
        public void VefiryEnterShareSkill(int rowNumber, string worksheet)
        {
            //var node = test.CreateNode("Step 2. Verify listing");
            
            //Populate excel data
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Verify expected Title vs actual Title
            Assert.AreEqual(ExcelLib.ReadData(rowNumber, "Title"), actualTitle.Text);

            //Verify expected Description vs actual Description
            Assert.AreEqual(ExcelLib.ReadData(rowNumber, "Description"), actualDescription.Text);

            //Verify expected Category vs actual Category
            Assert.AreEqual(ExcelLib.ReadData(rowNumber, "Category"), actualCategory.Text);

            //Verify expected Subcategory vs actual Subcategory
            Assert.AreEqual(ExcelLib.ReadData(rowNumber, "Subcategory"), actualSubcategory.Text);

            //Verify expected ServiceType vs actual ServiceType
            string expectedServiceType = ExcelLib.ReadData(rowNumber, "ServiceType");
            if (expectedServiceType == "One-off service")
            {
                expectedServiceType = "One-off";
            }
            else expectedServiceType = "Hourly";

            Assert.AreEqual(expectedServiceType, actualServiceType.Text);

            //Verify expected StartDate vs actual StartDate
            string expectedStartDate = DateTime.Parse(ExcelLib.ReadData(rowNumber, "StartDate"))
                .ToString("yyyy-MM-dd"); //Read data, parse as date, and format reverse as string
            Assert.AreEqual(expectedStartDate, actualStartDate.Text);

            //Verify expected EndDate vs actual EndDate
            string expectedEndDate = DateTime.Parse(ExcelLib.ReadData(rowNumber, "EndDate")).ToString("yyyy-MM-dd");
            Assert.AreEqual(expectedEndDate, actualEndDate.Text);

            //Verify expected LocationType vs actual LocationType
            string expectedLoationType = ExcelLib.ReadData(rowNumber, "LocationType");
            if (expectedLoationType.Equals("On-site"))
                expectedLoationType = "On-Site";

            Assert.AreEqual(expectedLoationType, actualLocationType.Text);

            //Verify Skills Trade
            if (ExcelLib.ReadData(rowNumber, "SkillTradeOption") == "Credit")
                Assert.AreEqual("None Specified", actualSkillsTrade.Text);
            else
                Assert.AreEqual(ExcelLib.ReadData(rowNumber, "SkillExchange"), actualSkillExchange.Text);

            //node.Pass("Step 2 is passed");
        }
        public void ClearData()
        {
            //Clear title
            Title.Click();
            Title.SendKeys(Keys.Control+"A");
            Title.SendKeys(Keys.Delete);

            //Clear description
            Description.Click();
            Description.SendKeys(Keys.Control+"A");
            Description.SendKeys(Keys.Delete);

            //Clear tags
            int countTags = displayedTags.Count();
            for (int i = 0; i < countTags; i++)
            {
                if (countTags > 0)
                {
                    displayedTags[i].Click();
                }
            }

            //Clear days
            for (int i = 0; i < Days.Count; i++)
            {
                bool dayState = Days[i].Selected;
                if (dayState.Equals(true))
                {
                    //Unselected day
                    Days[i].Click();

                    //Clear StartTime
                    StartTime[i].SendKeys(Keys.Delete);
                    StartTime[i].SendKeys(Keys.Tab);
                    StartTime[i].SendKeys(Keys.Delete);
                    StartTime[i].SendKeys(Keys.Tab);
                    StartTime[i].SendKeys(Keys.Delete);

                    //Clear Entime
                    EndTime[i].SendKeys(Keys.Delete);
                    EndTime[i].SendKeys(Keys.Tab);
                    EndTime[i].SendKeys(Keys.Delete);
                    EndTime[i].SendKeys(Keys.Tab);
                    EndTime[i].SendKeys(Keys.Delete);
                }

            }
            wait(1);

            //Clear skill trade
            for (int i = 0; i < radioSkillTrade.Count; i++)
            {
                string value = radioActive[i].GetAttribute("Value");
                bool state = radioSkillTrade[i].Selected;

                //Clear skill trade tags
                if (value.Equals("true") & state.Equals(true))
                {
                    for (int j = 0; j < skillExchangeTags.Count; j++)
                    {
                        skillExchangeTags[j].Click();
                    }
                }
                //Clear credit amount
                else if (value.Equals("false") & state.Equals(true))
                {
                    CreditAmount.Click();
                    CreditAmount.Clear();
                }
            }

        }
    }
}
