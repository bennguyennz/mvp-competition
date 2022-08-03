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
    internal class SignIn
    {
        #region
        //  Initialize Web Elements 
        //Finding the Sign Link
        private IWebElement SignIntab => driver.FindElement(By.XPath("//a[contains(text(),'Sign')]"));

        // Finding the Email Field
        private IWebElement Email => driver.FindElement(By.Name("email"));

        //Finding the Password Field
        private IWebElement Password => driver.FindElement(By.Name("password"));

        //Finding the Login Button
        private IWebElement LoginBtn => driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
        #endregion

        public void LoginSteps()
        {
            //Populate excel data
            ExcelLib.PopulateInCollection(Base.ExcelPath, "SignIn");

            //Click on Signin button
            SignIntab.Click();

            //Enter email
            Email.SendKeys(ExcelLib.ReadData(2, "Username"));
            

            //Enter password
            //GlobalDefinitions.wait(5);
            Password.SendKeys(ExcelLib.ReadData(2, "Password"));
            
            //Click Login button
            LoginBtn.Click();

            //GlobalDefinitions.wait(5);
            Thread.Sleep(3000);

        }
    }
}
