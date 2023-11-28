using BarrocIntens.Data;
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
using BarrocIntensTestlLibrary;
using BarrocIntensTestlLibrary.LoginWindow;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Maintenance : Window
    {
        private readonly User _currentUser;

        public Maintenance(User user)
        {
            _currentUser = user;
            this.InitializeComponent();

            LoadMaintenanceAppointments();
        }

        private void LoadMaintenanceAppointments()
        {
            using (var dbContext = new AppDbContext())
            {
  
                var maintenanceAppointments = dbContext.UserMaintenanceAppointments
                    .Where(uma => uma.UserId == _currentUser.Id)
                    .Select(uma => uma.MaintenanceAppointment)
                    .ToList();

                MaintenanceListView.ItemsSource = maintenanceAppointments;
            }
        }
    }
}
