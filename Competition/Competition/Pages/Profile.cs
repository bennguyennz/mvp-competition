using Competition.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Competition.Global.GlobalDefinitions;

namespace Competition.Pages
{
    internal class Profile
    {
        #region  Initialize Web Elements 
        //Click on Edit button
        private IWebElement AvailabilityTimeEdit => driver.FindElement(By.XPath("//span[contains(text(),'Part Time'));//i[@class='right floated outline small write icon']"));

        //Click on Availability Time dropdown
        private IWebElement AvailabilityTime => driver.FindElement(By.Name("availabiltyType"));

        //Click on Availability Time option
        private IWebElement AvailabilityTimeOpt => driver.FindElement(By.XPath("//option[@value='0']"));

        //Click on Availability Hour dropdown
        private IWebElement AvailabilityHours => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[1]/div/div[3]/div"));

        //Click on salary
        private IWebElement Salary => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[1]/div/div[4]/div"));

        //Click on Location
        private IWebElement Location => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[2]/div/div[2]/div"));

        //Choose Location
        private IWebElement LocationOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[2]/div/div[2]/div/div[2]"));

        //Click on City
        private IWebElement City => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[2]/div/div[3]/div"));

        //Choose City
        private IWebElement CityOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[2]/div/div[3]/div/div[2]"));

        //Click on Add new to add new Language
        private IWebElement AddNewLangBtn => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div"));

        //Enter the Language on text box
        private IWebElement AddLangText => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[3]/div/div[2]/div/div/div[1]/input"));

        //Enter the Language on text box
        private IWebElement ChooseLang => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[3]/div/div[2]/div/div/div[2]/select"));

        //Enter the Language on text box
        private IWebElement ChooseLangOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[3]/div/div[2]/div/div/div[2]/select/option[3]"));

        //Add Language
        private IWebElement AddLang => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[3]/div/div[2]/div/div/div[3]/input[1]"));

        //Click on Add new to add new skill
        private IWebElement AddNewSkillBtn => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[4]/div/div[2]/div/table/thead/tr/th[3]/div"));

        //Enter the Skill on text box
        private IWebElement AddSkillText => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[4]/div/div[2]/div/div/div[1]/input"));

        //Click on skill level dropdown
        private IWebElement ChooseSkill => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[4]/div/div[2]/div/div/div[2]/select"));

        //Choose the skill level option
        private IWebElement ChooseSkilllevel => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[4]/div/div[2]/div/div/div[2]/select/option[3]"));

        //Add Skill
        private IWebElement AddSkill => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[4]/div/div[2]/div/div/span/input[1]"));

        //Click on Add new to Educaiton
        private IWebElement AddNewEducation => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/table/thead/tr/th[6]/div"));


        //Enter university in the text box
        private IWebElement EnterUniversity => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[1]/div[1]/input"));


        //Choose the country
        private IWebElement ChooseCountry => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[1]/div[2]/select"));

        //Choose the skill level option
        private IWebElement ChooseCountryOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[1]/div[2]/select/option[6]"));

        //Click on Title dropdown
        private IWebElement ChooseTitle => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[2]/div[1]/select"));

        //Choose title
        private IWebElement ChooseTitleOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[2]/div[1]/select/option[5]"));

        //Degree
        private IWebElement Degree => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[2]/div[2]/input"));

        //Year of graduation
        private IWebElement DegreeYear => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[2]/div[3]/select"));

        //Choose Year
        private IWebElement DegreeYearOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[2]/div[3]/select/option[4]"));

        //Click on Add
        private IWebElement AddEdu => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[5]/div/div[2]/div/div/div[3]/div/input[1]"));

        //Add new Certificate
        private IWebElement AddNewCerti => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/table/thead/tr/th[4]/div"));

        //Enter Certification Name
        private IWebElement EnterCerti => driver.FindElement(By.Name("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/div/div[1]/div/input"));

        //Certified from
        private IWebElement CertiFrom => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/div/div[2]/div[1]/input"));

        //Year
        private IWebElement CertiYear => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/div/div[2]/div[2]/select"));

        //Choose Opt from Year
        private IWebElement CertiYearOpt => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/div/div[2]/div[2]/select/option[5]"));

        //Add Ceritification
        private IWebElement AddCerti => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[6]/div/div[2]/div/div/div[3]/input[1]"));

        //Add Desctiption
        private IWebElement Description => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[8]/div/div[2]/div[1]/textarea"));

        //Click on Save Button
        private IWebElement Save => driver.FindElement(By.XPath("//*[@id='account-profileEdit-section']/div/section[2]/div/div/div/form/div[8]/div/div[4]/span/button[1]"));

        #endregion

        internal void EditProfile()
        {

        }
    }
}
