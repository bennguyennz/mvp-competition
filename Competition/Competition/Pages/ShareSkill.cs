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

namespace Competition.Pages
{
    internal class ShareSkill
    {
        #region Page Objects for ShareSkill
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
        private IWebElement Tags => driver.FindElement(By.XPath("//div[@class='form-wrapper field  ']//input"));

        //Service type radio button
        private IList<IWebElement> radioServiceType => driver.FindElements(By.Name("serviceType"));

        //Location Type radio button
        private IList <IWebElement> radioLocationType => driver.FindElements(By.Name("locationType"));

        //Start Date dropdown
        private IWebElement StartDateDropDown => driver.FindElement(By.Name("startDate"));

        //End Date dropdown
        private IWebElement EndDateDropDown => driver.FindElement(By.Name("endDate"));

        //Savailable days
        private IWebElement Days => driver.FindElement(By.XPath("//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]"));

        //Starttime
        private IWebElement StartTime => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //StartTime dropdown
        private IWebElement StartTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //EndTime dropdown
        private IWebElement EndTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[3]/input[1]"));

        //Skill Trade option
        private IList <IWebElement> radioSkillTrade => driver.FindElements(By.Name("skillTrades"));

        //Skill Exchange
        private IWebElement SkillExchange => driver.FindElement(By.XPath("//div[@class='form-wrapper']//input[@type='text']"));

        //Credit textbox
        private IWebElement CreditAmount => driver.FindElement(By.XPath("//input[@placeholder='Amount']"));

        //Work Samples button
        private IWebElement btnWorkSamples => driver.FindElement(By.XPath("//i[@class='huge plus circle icon padding-25']"));

        //Active option
        private IList <IWebElement> radioActive => driver.FindElements(By.XPath("//input[@name='isActive']"));
       
        //Save button
        private IWebElement Save => driver.FindElement(By.XPath("//input[@value='Save']"));
        #endregion

        
        //Filling Share-Skill details
        internal void EnterShareSkill()
        {
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");

            //Click on Share Skill button
            btnShareSkill.Click();
            wait(1);

            //Enter Title 
            Title.SendKeys(ExcelLib.ReadData(2, "Title"));

            //Enter Description
            Description.SendKeys(ExcelLib.ReadData(2, "Description"));

            //Select category
            var selectCategory = new SelectElement(CategoryDropDown);
            selectCategory.SelectByText(ExcelLib.ReadData(2, "Category"));

            //Select Subcategory
            var selectSubcategory = new SelectElement(SubCategoryDropDown);
            selectSubcategory.SelectByText(ExcelLib.ReadData(2, "Subcategory"));

            //Enter tag
            Tags.Click();
            Tags.SendKeys(ExcelLib.ReadData(2, "Tags"));
            Tags.SendKeys(Keys.Return);

            ////Select Service type
            string expectedServiceType = ExcelLib.ReadData(2, "ServiceType");
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
            Thread.Sleep(1000);

            //Select Location type
            string expectedLocationType = ExcelLib.ReadData(2, "LocationType");
            
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
            Thread.Sleep(1000);

            //Enter Start date
            StartDateDropDown.SendKeys(ExcelLib.ReadData(2, "StartDate"));

            //Enter End date
            EndDateDropDown.SendKeys(ExcelLib.ReadData(2, "EndDate"));
            EndDateDropDown.SendKeys(Keys.Enter);

            //Enter Days



            wait(1);

            //Select "Skill Trade" options
            string expectedSkillTrade = ExcelLib.ReadData(2, "SkillTradeOption");

            string expectedSkillValue = "true";
            if(expectedSkillTrade.Equals("Credit"))
                expectedSkillValue = "false";

            for (int i = 0; i < radioSkillTrade.Count(); i++)
            {
                string actualSkillTradeValue=radioSkillTrade[i].GetAttribute("Value");
                if (expectedSkillValue.Equals(actualSkillTradeValue))
                {
                    //Select "Skill exchange" or "Credit"
                    radioSkillTrade[i].Click();
                    wait(1);

                    if (expectedSkillTrade.Equals("Skill-exchange"))
                    //Enter tags for Skill-exchange
                    {
                        SkillExchange.Click();
                        SkillExchange.SendKeys(ExcelLib.ReadData(2, "SkillExchange"));
                        SkillExchange.SendKeys(Keys.Return);
                    }
                    else
                    {
                        //Enter Credit amount
                        CreditAmount.SendKeys(ExcelLib.ReadData(2, "CreditAmount"));
                    }
                }               
            }
            Thread.Sleep(1000);

            //Click button Upload Work Samples
            btnWorkSamples.Click();
            wait(3);

            //Run AutoIT-script to execute file uploading
            using (Process exeProcess = Process.Start(Base.AutoScriptPath))
            {
                exeProcess.WaitForExit();
            }
            Thread.Sleep(1000);

            //Select ActiveOption
            string expectedActiveOption = ExcelLib.ReadData(2, "ActiveOption");

            string expectedActiveValue = "true";
            if (expectedActiveOption.Equals("Hidden"))
                expectedActiveValue = "false";

            for (int i=0; i< radioActive.Count(); i++)
            {
                string actualActiveValue = radioActive[i].GetAttribute("Value");
                if (expectedActiveValue.Equals(actualActiveValue))
                    radioActive[i].Click();
            }
            Thread.Sleep(1000);

            //Click on Save
            Save.Click();
            Thread.Sleep(3000);
            wait(1);

            //Assertions
            ThenShareSkillIsCreated();
        }

        #region POM pattern for assertions
        //button manage listing
        private IWebElement manageListing => driver.FindElement(By.XPath("//a[@href='/Home/ListingManagement']"));
        
        //button view listing
        private IWebElement buttonView => driver.FindElement(By.XPath("//tbody/tr[1]//i[@class='eye icon']"));
        
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

        
        //Assertions on ShareSkill
        internal void ThenShareSkillIsCreated()
        {
            //Click manage listing
            manageListing.Click();
            wait(1);

            //Click on button View
            buttonView.Click();
            wait(1);
            
            //Populate excel data
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");

            //Validate expected Title vs actual Title
            Assert.AreEqual(ExcelLib.ReadData(2, "Title"), actualTitle.Text);

            //Validate expected Description vs actual Description
            Assert.AreEqual(ExcelLib.ReadData(2, "Description"), actualDescription.Text);

            //Validate expected Category vs actual Category
            Assert.AreEqual(ExcelLib.ReadData(2, "Category"), actualCategory.Text);

            //Validate expected Subcategory vs actual Subcategory
            Assert.AreEqual(ExcelLib.ReadData(2,"Subcategory"), actualSubcategory.Text);

            //Validate expected ServiceType vs actual ServiceType
            Assert.AreEqual(ExcelLib.ReadData(2,"ServiceType"), actualServiceType.Text+" service");

            //Validate expected StartDate vs actual StartDate
            string expectedStartDate = DateTime.Parse(ExcelLib.ReadData(2, "StartDate"))
                .ToString("yyyy-MM-dd"); //Read data, parse as date, and format reverse as string
            Assert.AreEqual(expectedStartDate, actualStartDate.Text);

            //Validate expected EndDate vs actual EndDate
            string expectedEndDate = DateTime.Parse(ExcelLib.ReadData(2, "EndDate")).ToString("yyyy-MM-dd");
            Assert.AreEqual(expectedEndDate, actualEndDate.Text);

            //Validate expected LocationType vs actual LocationType
            Assert.AreEqual(ExcelLib.ReadData(2, "LocationType"), actualLocationType.Text);

            //Validate Skills Trade
            if (ExcelLib.ReadData(2, "SkillTradeOption") == "Credit")
                Assert.AreEqual("None Specified", actualSkillsTrade.Text);
            else
                Assert.AreEqual(ExcelLib.ReadData(2, "SkillExchange"), actualSkillExchange.Text);
        }

        internal void EditShareSkill()
        {

        }
    }
}
