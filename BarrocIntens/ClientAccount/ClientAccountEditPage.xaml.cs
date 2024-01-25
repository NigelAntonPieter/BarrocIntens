using BarrocIntens.Data;
using BarrocIntens.Utility;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.ClientAccount
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientAccountEditPage : Page
    {
        private User currentUser;
        private bool isEditing = false;
        public ClientAccountEditPage()
        {
            this.InitializeComponent();
            currentUser = Data.User.LoggedInUser;
            UserNameTextBlock.Text = currentUser.UserName;
        }
        private async void EditUserNameEl_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                if (isEditing)
                {
                    currentUser.UserName = UserNameTextBox.Text;
                    db.Users.Update(currentUser);
                    db.SaveChanges();

                    currentUser = db.Users.FirstOrDefault(u => u.Id == currentUser.Id);
                    UserNameTextBlock.Text = currentUser.UserName;
                    UserNameTextBlock.Visibility = Visibility.Visible;
                    UserNameTextBox.Visibility = Visibility.Collapsed;

                    isEditing = false;

                    await ShowSuccesDialog("username has changed");
                    return;
                }
                else
                {
                    UserNameTextBlock.Visibility = Visibility.Collapsed;
                    UserNameTextBox.Visibility = Visibility.Visible;
                    UserNameTextBox.Text = currentUser.UserName;

                    isEditing = true;
                }
            }
        }

        private async Task ShowSuccesDialog(string succesMessage)
        {
            SuccesMessageText.Text = succesMessage;
            await succesDialog.ShowAsync();
        }
        private async Task ShowErrorDialog(string errorMessage)
        {
            ErrorMessageText.Text = errorMessage;
            await errorDialog.ShowAsync();
        }

        private async Task ShowPasswordDialogAsync(string reason)
        {
            ContentDialogResult result = await changePasswordDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var authManager = new BarrocIntensTestlLibrary.LoginWindow.AuthenticationManager(new AppAuthenticationService());
                if (authManager.Authenticate(Data.User.LoggedInUser.UserName, currentPasswordBox.Password))
                {
                    if (reason == "ChangePassword")
                    {
                        await ShowSetNewPasswordDialog("");
                    }
                    else if (reason == "RequestDeleteAccount")
                    {
                        await SendEmailToClientAdminAsync();
                    }
                }
                else
                {
                    await ShowErrorDialog("incorrect password");
                }
            }
            else if (result == ContentDialogResult.Secondary)
            {
                changePasswordDialog.Hide();
            }
        }
        private async Task ShowSetNewPasswordDialog(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ErrorMessagePasswordText.Text = error;
            }
            ContentDialogResult result = await SetNewPasswordDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (NewpasswordBox1.Password == NewpasswordBox2.Password)
                {
                    using (var dbContext = new AppDbContext())
                    {
                        var user = dbContext.Users.FirstOrDefault(u => u.Id == Data.User.LoggedInUser.Id);

                        if (user != null)
                        {
                            user.Password = SecureHasher.Hash(NewpasswordBox2.Password);

                            await dbContext.SaveChangesAsync();
                            await ShowSuccesDialog("password has changed succesfully");
                        }
                    }
                }
                else
                {
                    await ShowSetNewPasswordDialog("Passwords do not match");
                }
            }
            else if (result == ContentDialogResult.Secondary)
            {
                changePasswordDialog.Hide();
            }
        }
        private async void EditPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            await ShowPasswordDialogAsync("ChangePassword");
        }

        private async void RequestDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            await ShowPasswordDialogAsync("RequestDeleteAccount");
        }
        async Task SendEmailToClientAdminAsync()
        {
            var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 587,
                Credentials = new NetworkCredential("fbeed68a24ce3c", "e3aa23f72b0da1"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                //dit moet eigenlijk email worden van de clientadmin maar users heeft nog geen email dus is nog een hardcoded email
                From = new MailAddress("brent.albers1999@gmail.com"),
                Subject = $"Delete account: {Data.User.LoggedInUser.Name}",
                Body = $"New Delete request:\n\nGebruiker Id: {Data.User.LoggedInUser.Id}",
            };

            mailMessage.To.Add("brent.albers1999@gmail.com");

            smtpClient.Send(mailMessage);
            await ShowSuccesDialog("Request to delete account has been sent");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
