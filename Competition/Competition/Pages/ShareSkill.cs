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
    public class ShareSkill
    {
        #region Page Objects for EnterShareSkill
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
        private IList<IWebElement> radioLocationType => driver.FindElements(By.Name("locationType"));

        //Start Date dropdown
        private IWebElement StartDateDropDown => driver.FindElement(By.Name("startDate"));

        //End Date dropdown
        private IWebElement EndDateDropDown => driver.FindElement(By.Name("endDate"));

        //Available days
        private IList<IWebElement> Days => driver.FindElements(By.XPath("//input[@name='Available']"));

        //Starttime
        private IList<IWebElement> StartTime => driver.FindElements(By.Name("StartTime"));

        //EndTime
        private IList<IWebElement> EndTime => driver.FindElements(By.Name("EndTime"));


        //StartTime dropdown
        private IWebElement StartTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //EndTime dropdown
        private IWebElement EndTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[3]/input[1]"));

        //Skill Trade option
        private IList<IWebElement> radioSkillTrade => driver.FindElements(By.Name("skillTrades"));

        //Skill Exchange
        private IWebElement SkillExchange => driver.FindElement(By.XPath("//div[@class='form-wrapper']//input[@type='text']"));
        private IList<IWebElement> skillExchangeTags => driver.FindElements(By.XPath("//form[@class='ui form']/div[8]/div[4]/div/div/div/div/span/a"));


        //Credit textbox
        private IWebElement CreditAmount => driver.FindElement(By.XPath("//input[@placeholder='Amount']"));

        //Work Samples button
        private IWebElement btnWorkSamples => driver.FindElement(By.XPath("//i[@class='huge plus circle icon padding-25']"));

        //Active option
        private IList<IWebElement> radioActive => driver.FindElements(By.XPath("//input[@name='isActive']"));

        //Save button
        private IWebElement Save => driver.FindElement(By.XPath("//input[@value='Save']"));
        #endregion

        #region Page Objects for VerifyShareSkill
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

        #region Page Objects for error Messages

        //Title message
        private IWebElement errorTitle => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[1]/div/div[2]/div/div[2]/div"));

        //Description message
        private IWebElement errorDescription => driver.FindElement(By.XPath("//div[@class='tooltip-target ui grid']//div/div[2]/div[2]/div"));

        //Category message
        private IWebElement errorCategory => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div[2]"));

        //Subcategory message
        private IWebElement errorSubcategory => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[2]/div[2]/div"));

        //Tags message
        private IWebElement errorTags => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[4]/div[2]/div[2]"));

        //StartDate message
        private IWebElement errorStartDate1 => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div[2]"));

        //StartDate mesage 2
        private IWebElement errorStartDate2 => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div[3]"));

        //Skill-Exchange tag
        private IWebElement errorSkillExchangeTags => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]/div[2]"));

        //Message
        private IWebElement message => driver.FindElement(By.XPath(e_message));
        private string e_message = "//div[@class='ns-box-inner']";

        #endregion

        //Filling Share-Skill details
        public void EnterShareSkill(int rowNumber, string worksheet)
        {
            //Initial a struct object and assign values
            Listing excelData = new Listing();
            GetExcel(rowNumber, worksheet, out excelData);

            //Enter Title 
            string title = excelData.title;
            Title.SendKeys(title);

            //Enter Description
            Description.SendKeys(excelData.description);

            //Select category
            var selectCategory = new SelectElement(CategoryDropDown);
            selectCategory.SelectByText(excelData.category);

            //Select Subcategory
            var selectSubcategory = new SelectElement(SubCategoryDropDown);
            selectSubcategory.SelectByText(excelData.subcategory);

            //Enter tag
            Tags.Click();
            Tags.SendKeys(excelData.tags);
            Tags.SendKeys(Keys.Return);

            //Select Service type
            SelectServiceType(excelData.serviceType);

            //Select Location type
            SelectLocationType(excelData.locationType);

            //Enter Start date
            StartDateDropDown.SendKeys(excelData.startDate);

            //Enter End date
            EndDateDropDown.SendKeys(excelData.endDate);

            //Enter Available days and hours
            EnterAvailableDaysAndHours((excelData.availableDays), (excelData.startTime), (excelData.endTime));

            //Select Skill Trade: "Credeit" or "Skill-exchange"
            SelectSkillTrade(excelData.skillTrade, excelData.skillExchange, excelData.credit);

            //Click button Upload Work Samples
            UploadWorkSamples();

            //Click Active or Hidden
            ClickActiveOption(excelData.ActiveOption);

            //Click on Save
            Save.Click();
            
        }

        #region Sub-methods for EnterShareSkill
        //Select Service type
        internal void SelectServiceType(string serviceTypeText)
        {
            string elementValue = "0";
            if (serviceTypeText.Equals("One-off service"))
                elementValue = "1";

            for (int i = 0; i < radioServiceType.Count(); i++)
            {
                string actualElementValue = radioServiceType[i].GetAttribute("Value");
                if (elementValue.Equals(actualElementValue))
                    radioServiceType[i].Click();
            }
        }

        //Select Location type
        internal void SelectLocationType(string locationTypeText)
        {
            //Select Location type
            string elementValue = "0";
            if (locationTypeText.Equals("Online"))
                elementValue = "1";

            for (int i = 0; i < radioLocationType.Count(); i++)
            {
                string actualElementValue = radioLocationType[i].GetAttribute("Value");
                if (elementValue.Equals(actualElementValue))
                    radioLocationType[i].Click();
            }
        }

        //Enter Available days and hours
        internal void EnterAvailableDaysAndHours(string availableDaysText, string startTimeText, string endTimeText)
        {
            //Enter available Days array = 
            string indexValue = "";

            switch (availableDaysText)
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

                    StartTime[i].SendKeys(startTimeText);
                    EndTime[i].SendKeys(endTimeText);
                }
            }
        }

        //Select Skill trade
        internal void SelectSkillTrade(string skillTradeText, string skillExchangeText, string creditText)
        {
            //Select "Skill Trade" options
            string elementValue = "true";

            if (skillTradeText.Equals("Credit"))
                elementValue = "false";

            for (int i = 0; i < radioSkillTrade.Count(); i++)
            {
                string actualElementValue = radioSkillTrade[i].GetAttribute("value");
                if (elementValue.Equals(actualElementValue))
                {
                    //Select "Skill exchange" or "Credit"
                    radioSkillTrade[i].Click();
                    wait(1);

                    if (skillTradeText.Equals("Skill-exchange"))
                    {
                        //Enter tags for Skill-exchange
                        SkillExchange.Click();
                        SkillExchange.SendKeys(skillExchangeText);
                        SkillExchange.SendKeys(Keys.Return);
                    }
                    else
                    {
                        //Enter Credit amount
                        CreditAmount.SendKeys(creditText);
                    }
                }
            }
        }

        //Upload Work samples
        internal void UploadWorkSamples()
        {
            btnWorkSamples.Click();
            wait(3);

            //Run AutoIT-script to execute file uploading
            using (Process exeProcess = Process.Start(Base.AutoScriptPath))
            {
                exeProcess.WaitForExit();
            }
        }

        //Click Active or Hidden
        internal void ClickActiveOption(string ActiveOptionText)
        {
            string elementValue = "true";
            if (ActiveOptionText.Equals("Hidden"))
                elementValue = "false";

            for (int i = 0; i < radioActive.Count(); i++)
            {
                string actualElementValue = radioActive[i].GetAttribute("Value");
                if (elementValue.Equals(actualElementValue))
                    radioActive[i].Click();
            }
        }
        #endregion

        //sub-method for Edit
        internal void ClearData()
        {
            //Clear title
            Title.Click();
            Title.SendKeys(Keys.Control + "A");
            Title.SendKeys(Keys.Delete);

            //Clear description
            Description.Click();
            Description.SendKeys(Keys.Control + "A");
            Description.SendKeys(Keys.Delete);

            //Clear tags
            int countTags = displayedTags.Count();
            for (int i = 0; i < countTags; i++)
            {
                if (countTags > 0)
                    displayedTags[i].Click();
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

        //Negative test
        internal void EnterShareSkill_InvalidData(int testData, string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
            Listing test = new Listing();
            shareSkillObj.GetExcel(testData, worksheet, out test);

            //Assert no data
            if (test.isClickSaveFirst == "Yes")
            {
                Save.Click();
            }
            //Assert invalid data
            else if (test.isClickSaveFirst == "No")
            {
                //Enter invalid data, depending on excel
                EnterDataOnConditions(test.title, test.description, test.tags, test.startDate, test.endDate,
                    test.skillTrade, test.skillExchange, test.credit, test.category, test.subcategory);

                //Click Save button
                Save.Click();
            }
        }

        #region Sub-methods for EnterShareSkill_InvalidData
        internal void EnterDataOnConditions(string titleText, string descriptionText, string tagsText,
            string startDateText, string endDateText, string skillTradeText, string skillExchangeText,
            string creditAmountText, string categoryText, string subCategoryText)
        {
            //Enter title
            if (titleText != "Ignore")
            {
                Title.SendKeys(titleText);
            }

            //Enter Description
            if (descriptionText != "Ignore")
            {
                Description.SendKeys(descriptionText);
            }

            //Select category 
            var selectCategory = new SelectElement(CategoryDropDown);
            if (categoryText != "Ignore")
            {
                selectCategory.SelectByText(categoryText);
            }

            if (subCategoryText == "Ignore")
            {
                //Select Subcategory
                var selectSubcategory = new SelectElement(SubCategoryDropDown);
                selectSubcategory.SelectByText(subCategoryText);
            }

            //Enter tags
            if (tagsText != "Ignore")
            {
                Tags.Click();
                Tags.SendKeys(tagsText);
                Tags.SendKeys(Keys.Return);
            }

            //Enter Start date
            if (startDateText != "Ignore")
            {
                StartDateDropDown.SendKeys(startDateText);
            }

            //Enter End date
            if (endDateText != "Ignore")
            {
                EndDateDropDown.SendKeys(endDateText);
            }

            //Select "Skill Trade" options
            if (skillTradeText != "Ignore")
            {
                SelectSkillTrade(skillTradeText, skillExchangeText, creditAmountText);
            }
        }
        #endregion

        #region struct and sub-methods for assertions
        internal struct Listing
        {
            public string title;
            public string description;
            public string category;
            public string subcategory;
            public string startDate;
            public string endDate;
            public string serviceType;
            public string locationType;
            public string skillTrade;
            public string skillExchange;
            public string tags;
            public string availableDays;
            public string startTime;
            public string endTime;
            public string credit;
            public string ActiveOption;
            public string isClickSaveFirst;
        }
        internal void GetExcel(int rowNumber, string worksheet, out Listing excelData)
        {
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            excelData.title = ExcelLib.ReadData(rowNumber, "Title");
            excelData.description = ExcelLib.ReadData(rowNumber, "Description");
            excelData.category = ExcelLib.ReadData(rowNumber, "Category");
            excelData.subcategory = ExcelLib.ReadData(rowNumber, "Subcategory");
            excelData.startDate = ExcelLib.ReadData(rowNumber, "StartDate");
            excelData.endDate = ExcelLib.ReadData(rowNumber, "EndDate");
            excelData.serviceType = ExcelLib.ReadData(rowNumber, "ServiceType");
            excelData.locationType = ExcelLib.ReadData(rowNumber, "LocationType");
            excelData.skillTrade = ExcelLib.ReadData(rowNumber, "SkillTradeOption");
            excelData.skillExchange = ExcelLib.ReadData(rowNumber, "SkillExchange");
            excelData.tags = ExcelLib.ReadData(rowNumber, "Tags");
            excelData.availableDays = ExcelLib.ReadData(rowNumber, "Days");
            excelData.startTime = ExcelLib.ReadData(rowNumber, "StartTime");
            excelData.endTime = ExcelLib.ReadData(rowNumber, "EndTime");
            excelData.credit = ExcelLib.ReadData(rowNumber, "CreditAmount");
            excelData.ActiveOption = ExcelLib.ReadData(rowNumber, "ActiveOption");
            excelData.isClickSaveFirst = ExcelLib.ReadData(rowNumber, "isClickSaveFirst");

        }
        internal void GetWeb(out Listing webData)
        {
            webData.title = actualTitle.Text;
            webData.description = actualDescription.Text;
            webData.category = actualCategory.Text;
            webData.subcategory = actualSubcategory.Text;
            webData.startDate = actualStartDate.Text;
            webData.endDate = actualEndDate.Text;
            webData.serviceType = actualServiceType.Text;
            webData.locationType = actualLocationType.Text;

            webData.skillTrade = "dummy";
            webData.skillExchange = "dummy";
            webData.tags = "dummy";
            webData.availableDays = "dummy";
            webData.startTime = "dummy";
            webData.endTime = "dummy";
            webData.credit = "dummy";
            webData.ActiveOption = "dummy";
            webData.isClickSaveFirst = "dummy";
        }
        internal void GetPortalMessage(out Listing portal)
        {
            portal.title = errorTitle.Text;
            portal.description = errorDescription.Text;
            portal.tags = errorTags.Text;

            portal.category = "dummy";
            portal.subcategory = "dummy";
            portal.startDate = "dummy";
            portal.endDate = "dummy";
            portal.serviceType = "dummy";
            portal.locationType = "dummy";
            portal.availableDays = "dummy";
            portal.startTime = "dummy";
            portal.endTime = "dummy";
            portal.skillTrade = "dummy";
            portal.skillExchange = "dummy";
            portal.credit = "dummy";
            portal.ActiveOption = "dummy";
            portal.isClickSaveFirst = "dummy";
        }


        internal string GetSkillTrade(string skillTradeOption)
        {
            if (skillTradeOption == "Credit")
                return actualSkillsTrade.Text;
            else
                return actualSkillExchange.Text;
        }
        internal string GetMessage()
        {
            //Check confirmation message
            WaitForElement(driver, By.XPath(e_message), 5);
            return message.Text;
        }
        internal string GetDateErrorMessage1()
        {
            return errorStartDate2.Text;
        }
        internal string GetDateErrorMessage2()
        {
            return errorStartDate1.Text;
        }
        internal string GetCategoryError()
        {
            return errorCategory.Text;
        }
        internal string GetSubcategoryError()
        {
            return errorSubcategory.Text;
        }
        internal string GetSkillExchangeError()
        {
            return errorSkillExchangeTags.Text;
        }
        internal string GetCredit()
        {
            return CreditAmount.Text;
        }
        #endregion
    }
}