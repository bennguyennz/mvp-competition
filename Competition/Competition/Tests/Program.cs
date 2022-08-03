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
            public void WhenIClickShareSkillAndEnterShareSkill()
            {
                shareSkillObj = new ShareSkill();
                shareSkillObj.EnterShareSkill();
            }

        }
    }
}