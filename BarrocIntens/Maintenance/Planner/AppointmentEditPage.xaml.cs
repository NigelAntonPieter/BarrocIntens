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
using System.Collections.ObjectModel;
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
    public sealed partial class AppointmentEditPage : Page
    {
        private Maintenance_appointment _selectedMaintenanceAppointment;
        public AppointmentEditPage()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            var maintenanceUsers = db.Users.Where(u => u.Role == "Maintenance").ToList();
            UserComboBox.ItemsSource = maintenanceUsers;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Maintenance_appointment clickedAppointment)
            {
                _selectedMaintenanceAppointment = clickedAppointment;
            }
        }


        private void uitLogEL_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private async void editAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMaintenanceAppointment != null && AppointmentDate.SelectedDate.HasValue)
            {
                var selectedUser = (User)UserComboBox.SelectedItem;

                using (var dbContext = new AppDbContext())
                {
                    // Haal de geselecteerde afspraak op met behulp van de Id
                    var maintenanceAppointment = dbContext.MaintenanceAppointments
                        .Include(m => m.UserMaintenanceAppointments)
                        .SingleOrDefault(m => m.Id == _selectedMaintenanceAppointment.Id);

                    if (maintenanceAppointment != null)
                    {
                        // Wijzig de medewerker en de datum van de onderhoudsafspraak
                        maintenanceAppointment.DateOfMaintenanceAppointment = DateOnly.Parse(AppointmentDate.SelectedDate.ToString().Split(" ")[0]);

                        // Pas de geselecteerde medewerker aan
                        var userMaintenanceAppointment = new UserMaintenanceAppointment
                        {
                            UserId = selectedUser.Id,
                            MaintenanceAppointmentId = maintenanceAppointment.Id
                        };

                        // Voeg de nieuwe medewerker toe of vervang de bestaande
                        maintenanceAppointment.UserMaintenanceAppointments.Clear();
                        maintenanceAppointment.UserMaintenanceAppointments.Add(userMaintenanceAppointment);

                        // Opslaan van wijzigingen in de database
                        dbContext.SaveChanges();
                    }
                }

                // Navigeer terug naar de vorige pagina of een andere gewenste pagina
                this.Frame.GoBack();
            }
            else
            {
                await editAppointmentDialog.ShowAsync();
            }
        }
    }
}
