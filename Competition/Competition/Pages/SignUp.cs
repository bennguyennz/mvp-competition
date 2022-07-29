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
    internal class SignUp
    {
        #region  Initialize Web Elements 
        //Finding the Join 
        private IWebElement Join => driver.FindElement(By.XPath("//*[@id='home']/div/div/div[1]/div/button"));

        //Identify FirstName Textbox
        private IWebElement FirstName => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[1]/input"));

        //Identify LastName Textbox
        private IWebElement LastName => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[2]/input"));

        //Identify Email Textbox
        private IWebElement Email => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[3]/input"));

        //Identify Password Textbox
        private IWebElement Password => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[4]/input"));

        //Identify Confirm Password Textbox
        private IWebElement ConfirmPassword => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[5]/input"));

        //Identify Term and Conditions Checkbox
        private IWebElement Checkbox => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/form/div[6]/div/div/input"));

        //Identify join button
        private IWebElement JoinBtn => driver.FindElement(By.XPath("//*[@id='submit-btn']"));
        #endregion

        internal void Register()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignUp");
            //Click on Join button
            Join.Click();

            //Enter FirstName
            FirstName.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "FirstName"));

            //Enter LastName
            LastName.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "LastName"));

            //Enter Email
            Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Email"));

            //Enter Password
            Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));

            //Enter Password again to confirm
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "ConfirmPswd"));

            //Click on Checkbox
            Checkbox.Click();

            //Click on join button to Sign Up
            JoinBtn.Click();


        }
    }
}
