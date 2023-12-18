using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SalesWindow : Window
    {
        public SalesWindow(User user)
        {
            this.InitializeComponent();

            var accountType = "client";

            using (var db = new AppDbContext())
            {
                var users = db.Users
                    .Where(u => u.Role == accountType);

                clientsListView.ItemsSource = users;
            }
        }

        private void MakeClientAccount_Click(object sender, RoutedEventArgs e)
        {
            var window = new ClientRegisterWindow();
            window.Activate();
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            var accountType = "client";

            using (var db = new AppDbContext())
            {
                var users = db.Users
                    .Where(u => u.Role == accountType);

                clientsListView.ItemsSource = users;
            }
        }

        private void clientsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (User)e.ClickedItem;

            var window = new ClientNotesListWindow(clickedItem.Id);
            window.Activate();
        }
    }
}
