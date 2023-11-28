using BarrocIntensTestlLibrary.LoginWindow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using static BarrocIntensTestlLibrary.LoginWindow.UserSettingsManager;
namespace BarrocUnitTests.LoginWindowTests
{



    [TestClass]
    public class UserSettingsManagerTests
    {
        [TestMethod]
        public void LoadLastUsername_WhenKeyExists_ReturnsLastUsername()
        {

            var originalValue = "TestUsername";
            ApplicationData.Current.LocalSettings.Values[UserSettingsManager.LastUsernameKey] = originalValue;

            var result = UserSettingsManager.LoadLastUsername();

            Assert.AreEqual(originalValue, result);
        }

        [TestMethod]
        public void LoadLastUsername_WhenKeyDoesNotExist_ReturnsEmptyString()
        {

            ApplicationData.Current.LocalSettings.Values.Clear();


            var result = UserSettingsManager.LoadLastUsername();


            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void SaveLastUsername_SetsValueInLocalSettings()
        { 
            var newValue = "NewTestUsername";

            UserSettingsManager.SaveLastUsername(newValue);

            Assert.IsTrue(ApplicationData.Current.LocalSettings.Values.ContainsKey(UserSettingsManager.LastUsernameKey));
            Assert.AreEqual(newValue, ApplicationData.Current.LocalSettings.Values[UserSettingsManager.LastUsernameKey]);
        }
    }



}
