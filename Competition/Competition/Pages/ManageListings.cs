using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Competition.Global;
using static Competition.Global.GlobalDefinitions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AventStack.ExtentReports;

namespace Competition.Pages
{
    internal static class ManageListings
    {
        #region Manage listing's page objects
        //ShareSkill Button
        private static IWebElement btnShareSkill => driver.FindElement(By.LinkText("Share Skill"));

        //Manage Listings
        private static IWebElement manageListingsLink => driver.FindElement(By.XPath("//a[@href='/Home/ListingManagement']"));

        //Message warning no listing
        private static IWebElement warningMessage => driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));

        //Title
        private static IList<IWebElement> Titles => driver.FindElements(By.XPath("//div[@id='listing-management-section']//tbody/tr/td[3]"));

        //View button
        private static IWebElement view => driver.FindElement(By.XPath("(//i[@class='eye icon'])[1]"));

        //Edit button
        private static IWebElement edit => driver.FindElement(By.XPath("(//i[@class='outline write icon'])[1]"));

        //Yes/No button
        private static IList<IWebElement> clickActionsButton => driver.FindElements(By.XPath("//div[@class='actions']/button"));

        //Message
        //private static IWebElement message => driver.FindElement(By.XPath(e_message));
        //private static string e_message = "//div[@class='ns-box-inner']";
        //private static string messageContent = "";

        //Save button
        private static IWebElement btnSave => driver.FindElement(By.XPath("//input[@value='Save']"));
        #endregion

        //Add a skill
        internal static void AddListing(int rowNumber, string worksheet)
        {
            btnShareSkill.Click();
            wait(2);
            ShareSkill.EnterShareSkill(rowNumber, worksheet);
            wait(3);
        }

        //Edit listing
        internal static void EditListing(int rowNumber1, int rowNumber2, string worksheet)
        {  
            //Click on ManageListing
            GoToManageListings();
            wait(2);
            //Populate the Excel Sheet
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Read data
            string expectedTitle = ExcelLib.ReadData(rowNumber1, "Title");

            //Click on button Edit
            string e_Edit = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[2]";
            IWebElement btnEdit = driver.FindElement(By.XPath(e_Edit));
            btnEdit.Click();
            wait(2);

            ShareSkill.ClearData();
            ShareSkill.EnterShareSkill(rowNumber2, worksheet);
            wait(3);
        }

        //Verify add & edit
        internal static void ViewListing(int rowNumber, string worksheet)
        {

            //Click on ManageListing
            GoToManageListings();
            wait(2);

            //Read data
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);
            string expectedTitle = ExcelLib.ReadData(rowNumber, "Title");

            //Click on button View
            string e_View = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[1]";
            IWebElement btnView = driver.FindElement(By.XPath(e_View));
            btnView.Click();

            wait(2);
        }

        //Delete listing
        internal static void DeleteListing(int rowNumber, string worksheet)
        {
            //Click on Manage listing
            GoToManageListings();

            //Populate the Excel Sheet
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Read data
            string isDelete = ExcelLib.ReadData(rowNumber, "isDelete");
            string expectedTitle = ExcelLib.ReadData(rowNumber, "Title");

            //Click on button delete
            string strDelete = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[3]";
            IWebElement btnDelete = driver.FindElement(By.XPath(strDelete));
            btnDelete.Click();

            //Click Yes
            if (isDelete.Equals("Yes"))
            {
                clickActionsButton[1].Click();

                //Verify message
                //WaitForElement(driver, By.XPath(e_message), 3);
                //messageContent = message.Text;
            }
            else
            {
                //Click No
                clickActionsButton[0].Click();
            }
            Thread.Sleep(1000);
        }

        //Verify delete
        internal static string FindTitle(string title)
        {

            //Verify delete message
            //Assert.AreEqual(messageContent, title + " has been deleted");

            //Verify if there is no listing
            string actualTitle = "null";
            int titleCount = Titles.Count();
            if (titleCount.Equals(0))
            {
                return actualTitle;
            }
            else
            {
                //Verify if title is deleted
                for (int i = 0; i < titleCount; i++)
                {
                    actualTitle = Titles[i].Text;
                    if (title.Equals(actualTitle))
                        break;
                }
                return actualTitle;
            }
            wait(2);
        }

        internal static void CreateMultipleShareSkill(string worksheet)
        {
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);
            
            //Click on button Share Skill
            btnShareSkill.Click();
            wait(1);

            int rowNumber = 2;
            string title = ExcelLib.ReadData(rowNumber, "Title");

            //Check if title text is NOT "End of Dataset" (text)
            while (title != "End of Dataset") 
            {
                ShareSkill.EnterShareSkill(rowNumber, worksheet);
                
                rowNumber++;
                title = ExcelLib.ReadData(rowNumber, "Title");

                btnShareSkill.Click();
                wait(2);
            }
        }

        internal static void EnterShareSkill_Invalid(int testData, string worksheet)
        {
            //Populate excel file
            //ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Click on button ShareSkill
            btnShareSkill.Click();
            wait(1);

            //Enter invalid data
            ShareSkill.EnterShareSkill_InvalidData(testData, "NegativeTC");
            Thread.Sleep(2000);
        }

        //Functions to check title is existing and return title's position in manage listing
        internal static string GetTitleIndex(string expectedTitle)
        {
            //Check if there is no listing's title
            string recordIndex = "";
            int titleCount = Titles.Count();
            if (titleCount.Equals(0))
            {
                Assert.Ignore("There is no listing record.");
            }
            else
            {
                //Find title: Break loop when finding a title. Output: recordIndex
                for (int i = 0; i < titleCount; i++)
                {
                    string actualTitle = Titles[i].Text;
                    if (actualTitle.Equals(expectedTitle))
                    {
                        recordIndex = (i + 1).ToString();
                        break;
                    }
                }
                //If title-to-delete is not found
                if (recordIndex.Equals(""))
                {
                    Assert.Ignore("Listing '" + expectedTitle + "' is not found.");
                }
            }
            return recordIndex;
        }

        //Click on manage listing
        internal static void GoToManageListings()
        {
            try
            {
                //Click Manage Listing
                manageListingsLink.Click();
            }
            catch (Exception ex)
            {
                Assert.Fail("Manage Listing link is not found.", ex.Message);
            }
        }
    }

}
