using Competition.Pages;
using NUnit.Framework;


namespace Competition
{
    [TestFixture]
    [Parallelizable]
    //[Category("Sprint1")]
    internal class Execution : Global.Base
    {
        ShareSkill shareSkillObj;
        ManageListings manageListingsObj;

        [Test, Order(1)]
        public void TC1_WhenIClickShareSkillAndEnterShareSkill()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            manageListingsObj = new ManageListings();

            manageListingsObj.AddListing(2, "ShareSkill"); 
            manageListingsObj.VerifyListing(2, "ShareSkill");
        }

        [Test, Order(2)]
        public void TC2_WhenIEditAListing()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            manageListingsObj = new ManageListings();
            manageListingsObj.AddListing(5, "ManageListings");
            manageListingsObj.AddListing(6, "ManageListings");
            manageListingsObj.EditListing(5, 7, "ManageListings");
            manageListingsObj.VerifyListing(7, "ManageListings");
        }

        [Test, Order(3)]
        public void TC3_WhenIDeleteAListing()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            manageListingsObj = new ManageListings();
            manageListingsObj.DeleteListing(7, "ManageListings");
            manageListingsObj.VerifyDelete(7, "ManageListings");
        }
        [Test, Order(5)]
        public void TC5_WhenIAddInvalidListing()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            manageListingsObj = new ManageListings();
            manageListingsObj.EnterShareSkill_Invalid(2, 3, 4, "NegativeTC"); //Set (2,3,4) or (6,7,8)
        }

        [Test, Order(4)]
        public void TC4_CreateMultipleListing()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            manageListingsObj = new ManageListings();
            manageListingsObj.CreateMultipleShareSkill("ShareSkill");
        }
    }
}