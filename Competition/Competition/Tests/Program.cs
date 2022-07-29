using Competition.Pages;
using NUnit.Framework;

namespace Competition
{
    internal class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class User : Global.Base
        {
            ShareSkill shareSkillObj;
            [Test]
            public void Test1()
            {

            }

            //public void WhenIClickShareSkillThenEnterShareSkill(string title, string description, string category, string subCategory, 
            //    string tags, string serviceType, string locationType, string availableDays, string skillTrade, 
            //    string skillExchange, string active)
            //{
            //    shareSkillObj.EnterShareSkill(title, description, category, subCategory, tags, serviceType,
            //        locationType, availableDays, skillTrade, skillExchange, active);
            //}

        }
    }
}