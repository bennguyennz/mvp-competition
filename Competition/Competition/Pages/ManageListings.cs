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
    internal class ManageListings
    {
        #region Manage listing's page objects
        //ShareSkill Button
        private IWebElement btnShareSkill => driver.FindElement(By.LinkText("Share Skill"));

        //Manage Listings
        private IWebElement manageListingsLink => driver.FindElement(By.XPath("//a[@href='/Home/ListingManagement']"));

        //Message warning no listing
        private IWebElement warningMessage => driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));

        //Title
        private IList<IWebElement> Titles => driver.FindElements(By.XPath("//div[@id='listing-management-section']//tbody/tr/td[3]"));

        //View button
        private IWebElement view => driver.FindElement(By.XPath("(//i[@class='eye icon'])[1]"));

        //Edit button
        private IWebElement edit => driver.FindElement(By.XPath("(//i[@class='outline write icon'])[1]"));

        //Yes/No button
        private IList <IWebElement> clickActionsButton => driver.FindElements(By.XPath("//div[@class='actions']/button"));
        
        //Message
        private IWebElement message => driver.FindElement(By.XPath(e_message));
        private string e_message = "//div[@class='ns-box-inner']";
        #endregion

        //Add a skill
        public void AddListing(int rowNumber, string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
            btnShareSkill.Click();
            wait(1);
            shareSkillObj.EnterShareSkill(rowNumber,worksheet);
            wait(2);
        }

        public void AddInvalidListing(int rowNumber, string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
            btnShareSkill.Click();
            wait(2);
        }

        //Edit listing
        public void EditListing(int rowNumber1, int rowNumber2, string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
           
            //Click on ManageListing
            GoToManageListings();

            //Populate the Excel Sheet
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Read data
            string expectedTitle = ExcelLib.ReadData(rowNumber1, "Title");

            //Click on button Edit
            string e_Edit = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[2]";
            IWebElement btnEdit = driver.FindElement(By.XPath(e_Edit));
            btnEdit.Click();
            wait(1);

            shareSkillObj.ClearData();
            shareSkillObj.EnterShareSkill(rowNumber2,worksheet);
            wait(2);
        }

        //Verify add & edit
        public void VerifyListing(int rowNumber, string worksheet)
        {
            ShareSkill ShareSkillObj = new ShareSkill();

            //Click on ManageListing
            GoToManageListings();

            //Populate the Excel Sheet
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Read data
            string expectedTitle = ExcelLib.ReadData(rowNumber, "Title");

            //Click on button Edit
            string e_View = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[1]";
            IWebElement btnView = driver.FindElement(By.XPath(e_View));
            btnView.Click();

            //Call verify ShareSkill
            ShareSkillObj.VefiryEnterShareSkill(rowNumber, worksheet);
            wait(2);
        }

        //Delete listing
        public void DeleteListing(int rowNumber, string worksheet)
        {
            //Click on Manage listing
            GoToManageListings();

            //Populate the Excel Sheet
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            //Read data
            string expectedTitle = ExcelLib.ReadData(rowNumber, "Title");
            string isDelete = ExcelLib.ReadData(rowNumber, "isDelete");

            //Click on button delete
            string strDelete = "//div[@id='listing-management-section']//tbody/tr[" + GetTitleIndex(expectedTitle) + "]/td[8]/div/button[3]";
            IWebElement btnDelete = driver.FindElement(By.XPath(strDelete));
            btnDelete.Click();

            //Click Yes
            if (isDelete.Equals("Yes"))
            {
                clickActionsButton[1].Click();

                //Verify message
                WaitForElement(driver, By.XPath(e_message), 3);
                Assert.AreEqual(message.Text, expectedTitle + " has been deleted");
            }
            else
            {
                //Click No
                clickActionsButton[0].Click();
            }
            wait(2);
        }

        //Verify delete
        public void VerifyDelete(int row, string worksheet)
        {
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);
            string title = ExcelLib.ReadData(row, "Title");
            wait(1);

            //Verify if there is no listing
            int titleCount = Titles.Count();
            if (titleCount.Equals(0))
            {
                Assert.Pass("No listing found.");
            }
            else
            {   //Verify if title is deleted
                for (int i = 0; i < titleCount; i++)
                {
                    Assert.That(Titles[i].Text != title, "Delete Failed. Listing has not been deleted");
                }
            }
            wait(2);
        }

        public void CreateMultipleShareSkill(string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);
            
            btnShareSkill.Click();
            wait(1);

            int rowNumber = 2;
            string title = ExcelLib.ReadData(rowNumber, "Title");

            //Check if title text is NOT "null" (text)
            while (title != "Dataset end") 
            {
                shareSkillObj.EnterShareSkill(rowNumber, worksheet);
                rowNumber++;
                title = ExcelLib.ReadData(rowNumber, "Title");

                btnShareSkill.Click();
                wait(2);
            }

        }

        public void AddListing_Invalid(int rowNumber, string worksheet)
        {
            ShareSkill shareSkillObj = new ShareSkill();
            //Populate excel file
            ExcelLib.PopulateInCollection(Base.ExcelPath, worksheet);

            btnShareSkill.Click();
            wait(1);

            shareSkillObj.CreateListing_InvalidData()

        }

        //Functions to check title is existing and return title's position in manage listing
        public string GetTitleIndex(string expectedTitle)
        {
            //Check if there is no listing's title
            string recordIndex = null;
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
                    string title = Titles[i].Text;
                    if (title.Equals(expectedTitle))
                    {
                        recordIndex = (i + 1).ToString();
                        break;
                    }
                }
                //If title-to-delete is not found
                if (recordIndex.Equals(null))
                {
                    Assert.Ignore("Listing '" + expectedTitle + "' is not found.");
                }
            }
            return recordIndex;
        }

        //Click on manage listing
        internal void GoToManageListings()
        {
            try
            {
                //Click Manage Listing
                manageListingsLink.Click();
                wait(1);
            }
            catch (Exception ex)
            {
                Assert.Fail("Manage Listing link is not found.", ex.Message);
            }
        }
    }

}
