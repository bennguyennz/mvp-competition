using Competition.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Competition.Global.GlobalDefinitions;

namespace Competition.Pages
{
    internal class ShareSkill
    {

        //Click on ShareSkill Button
        private IWebElement ShareSkillButton => driver.FindElement(By.LinkText("Share Skill"));

        //Enter the Title in textbox
        private IWebElement Title => driver.FindElement(By.Name("title"));

        //Enter the Description in textbox
        private IWebElement Description => driver.FindElement(By.Name("description"));

        //Click on Category Dropdown
        private IWebElement CategoryDropDown => driver.FindElement(By.Name("categoryId"));

        //Click on SubCategory Dropdown
        private IWebElement SubCategoryDropDown => driver.FindElement(By.Name("subcategoryId"));

        //Enter Tag names in textbox
        private IWebElement Tags => driver.FindElement(By.XPath("//div[@class='form-wrapper field  ']//input"));

        //Select the Service type
        private IWebElement ServiceTypeOption1 => driver.FindElement(By.Name("//input[@name='serviceType' and @value='0']"));
        private IWebElement ServiceTypeOption2 => driver.FindElement(By.Name("//input[@name='serviceType' and @value='1']"));



        //Select the Location Type
        private IWebElement LocationTypeOption1 => driver.FindElement(By.Name("//input[@name='locationType' and @value='0']"));
        private IWebElement LocationTypeOption2 => driver.FindElement(By.Name("//input[@name='locationType' and @value='1']"));

        //Click on Start Date dropdown
        private IWebElement StartDateDropDown => driver.FindElement(By.Name("startDate"));

        //Click on End Date dropdown
        private IWebElement EndDateDropDown => driver.FindElement(By.Name("endDate"));

        //Storing the table of available days
        private IWebElement Days => driver.FindElement(By.XPath("//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]"));

        //Storing the starttime
        private IWebElement StartTime => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //Click on StartTime dropdown
        private IWebElement StartTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[2]/input[1]"));

        //Click on EndTime dropdown
        private IWebElement EndTimeDropDown => driver.FindElement(By.XPath("//div[3]/div[3]/input[1]"));

        //Click on Skill Trade option
        private IWebElement SkillTradeOption => driver.FindElement(By.XPath("//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']"));

        //Enter Skill Exchange
        private IWebElement SkillExchange => driver.FindElement(By.XPath("//div[@class='form-wrapper']//input[@type='text']"));

        //Enter the amount for Credit
        private IWebElement CreditAmount => driver.FindElement(By.XPath("//input[@placeholder='Amount']"));

        //Click on Active/Hidden option
        private IWebElement ActiveOption => driver.FindElement(By.XPath("//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']"));

        //Click on Save button
        private IWebElement Save => driver.FindElement(By.XPath("//input[@value='Save']"));

        internal void EnterShareSkill(string title, string description, string category, string subCategory,
                string tags, string serviceType, string locationType, string availableDays, string skillTrade,
                string skillExchange, string active)
        {
            //Click on Share Skill button
            ShareSkillButton.Click();

            //Enter Title 
            Title.SendKeys("SpecFlow Feature Demo");

            //Enter Description
            Description.SendKeys("A session discussing about Automated testing using SpecFlow, BDD, POM.  Requirements: Visual studio Community version.");

            //Select category
            var selectCategory = new SelectElement(CategoryDropDown);
            selectCategory.SelectByText("Programming & Tech");

            //Select Subcategory
            var selectSubcategory = new SelectElement(SubCategoryDropDown);
            selectSubcategory.SelectByText("QA");

            //Enter tag
            Tags.Click();
            Tags.SendKeys("SpecFlow");
            Tags.SendKeys(Keys.Return);

            //Select Service type
            if (serviceType == "Hourly basis service")
                ServiceTypeOption1.Click();
            else ServiceTypeOption2.Click();

            //Select Location type
            if (locationType == "On-site")
                LocationTypeOption1.Click();
            else LocationTypeOption2.Click();

            //Enter Start date
            StartDateDropDown.Click();
            StartDateDropDown.SendKeys("start date");

            //Enter End date
            EndDateDropDown.Click();
            EndDateDropDown.SendKeys("End date");











        }

        internal void EditShareSkill()
        {

        }
    }
}
