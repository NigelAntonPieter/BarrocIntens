using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BarrocIntensTestlLibrary.LoginWindow
{
    public class UserSettingsManager
    {
        public const string LastUsernameKey = "LastUsername";

        public static string LoadLastUsername()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(LastUsernameKey, out object value) && value is string lastUsername)
            {
                return lastUsername;
            }

            return string.Empty;
        }

        public static void SaveLastUsername(string username)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[LastUsernameKey] = username;
        }
        public interface ILocalSettings
        {
            Dictionary<string, object> Values { get; }
        }
    }

}
