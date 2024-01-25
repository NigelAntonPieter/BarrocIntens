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
using Windows.System;
using System.Data.Entity;
using Microsoft.SqlServer.Server;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.ClientAccount
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientAccountPage : Page
    {

        public ObservableCollection<LeaseContract> LeaseContracts { get; set; }
        public ObservableCollection<Maintenance_appointment> MaintenanceAppointments { get; set; }

        public ClientAccountPage()
        {
            this.InitializeComponent();
            LeaseContracts = new ObservableCollection<LeaseContract>();
            MaintenanceAppointments = new ObservableCollection<Maintenance_appointment>();

            using (var db = new AppDbContext())
            {
                var userCompany = db.Users
                    .Where(u => u.Id == Data.User.LoggedInUser.Id)
                    .Select(u => u.Company)
                    .FirstOrDefault();

                if (userCompany != null)
                {
                    LeaseContracts = new ObservableCollection<LeaseContract>(db.LeaseContracts.Where(lc => lc.CompanyId == userCompany.Id).ToList());
                    MaintenanceAppointments = new ObservableCollection<Maintenance_appointment>(
                        db.MaintenanceAppointments.Where(ma => ma.CompanyId == userCompany.Id).ToList());
                    LeaseContractListView.ItemsSource = LeaseContracts;
                    MaintenanceListView.ItemsSource = MaintenanceAppointments;
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClientAccountEditPage));
        }

        private void ClientCreateAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ClientCreateMaintenanceAppointment));
        }
    }
}
