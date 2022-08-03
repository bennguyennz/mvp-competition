using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition.Global;
using static Competition.Global.GlobalDefinitions;

namespace Competition.Pages
{
    internal class ManageListings
    {
        //Manage Listings link
        private IWebElement manageListingsLink => driver.FindElement(By.Name("Manage Listings"));
        
        //View button
        private IWebElement view => driver.FindElement(By.XPath("(//i[@class='eye icon'])[1]"));
        
        //Delete button
        private IWebElement delete => driver.FindElement(By.XPath("//table[1]/tbody[1]"));

        //Edit the listing
        private IWebElement edit => driver.FindElement(By.XPath("(//i[@class='outline write icon'])[1]"));

        //Click on Yes or No
        private IWebElement clickActionsButton => driver.FindElement(By.XPath("//div[@class='actions']"));

        internal void Listings()
        {
            //Populate the Excel Sheet
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ManageListings");

        }
    }

}
