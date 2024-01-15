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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens.Maintenance.Planner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppointMaintenance : Page
    {
        private Maintenance_appointment selectedMaintenance;

        public AppointMaintenance()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();

            var maintenanceUsers = db.Users.Where(u => u.Role == "Maintenance").ToList();
            UserComboBox.ItemsSource = maintenanceUsers;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is Maintenance_appointment maintenance)
            {
                // Gebruik de ontvangen gegevens, bijvoorbeeld:
                selectedMaintenance = maintenance;
                // Werk de UI bij op basis van de ontvangen gegevens
                // ...
            }
        }

        public async void SaveAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMaintenance != null && UserComboBox.SelectedItem != null && AppointmentDate.SelectedDate.HasValue)
            {
                using var db = new AppDbContext();

                var selectedUser = (User)UserComboBox.SelectedItem;

                // Maak een nieuwe UserMaintenanceAppointment aan en koppel de geselecteerde gebruiker en de afspraak
                var userMaintenanceAppointment = new UserMaintenanceAppointment
                {
                    UserId = selectedUser.Id,
                    MaintenanceAppointmentId = selectedMaintenance.Id
                };

                // Voeg het nieuwe UserMaintenanceAppointment toe aan de database
                db.UserMaintenanceAppointments.Add(userMaintenanceAppointment);
                selectedMaintenance.DateOfMaintenanceAppointment = DateOnly.Parse(AppointmentDate.SelectedDate.ToString().Split(" ")[0]);

                db.MaintenanceAppointments.Update(selectedMaintenance);
                db.SaveChanges();



                // Navigeer terug naar PlanningInPage
                this.Frame.Navigate(typeof(PlanningInPage));
            }
            else
            {
                await maintenanceDialog.ShowAsync();
            }

        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
