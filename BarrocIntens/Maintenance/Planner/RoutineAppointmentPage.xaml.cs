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
using System.Data.Entity;
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
    public sealed partial class RoutineAppointmentPage : Page
    {
        
        public RoutineAppointmentPage()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();
            var maintenanceUsers = db.Users.Where(u => u.Role == "Maintenance").ToList();
            var Company = db.Companies.Include(c => c.Name);
            UserComboBox.ItemsSource = maintenanceUsers;
            CompanyComboBox.ItemsSource = Company;

            CompanyComboBox.SelectionChanged += CompanyComboBox_SelectionChanged;
        }

        private void CompanyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Wanneer de geselecteerde maintenance appointment verandert, vul automatisch de locatie in
            if (CompanyComboBox.SelectedItem is Company selectedcompany)
            {
                using var db = new AppDbContext();

                // Zoek de bijbehorende Company op basis van de CompanyId
                var selectedCompany = db.Companies.FirstOrDefault(c => c.Id == selectedcompany.Id);

                if (selectedCompany != null)
                {
                    LocationEl.Text = selectedCompany.Street;
                }
                else
                {
                    // Handle het geval waarin de bijbehorende Company niet kan worden gevonden
                    LocationEl.Text = "Locatie niet gevonden";
                }
            }
        }

        public async void SaveAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if ( UserComboBox.SelectedItem != null && CompanyComboBox.SelectedItem != null && RoutineDate.SelectedDate.HasValue && LocationEl.Text != null)
            {
                using var db = new AppDbContext();

                var selectedUser = (User)UserComboBox.SelectedItem;
                
                var selectedCompany = (Company)CompanyComboBox.SelectedItem;
                db.Attach(selectedCompany);

                var selectedDate = DateOnly.Parse(RoutineDate.SelectedDate.ToString().Split(" ")[0]);
                var numberOfMonths = 12; // Aantal maanden vooruit

                for (int i = 0; i < numberOfMonths; i++)
                {
                    var existingRoutine = db.Routines
                        .FirstOrDefault(r =>
                            r.Company.Id == selectedCompany.Id &&
                            r.DateOfRoutineAppointment.Month == selectedDate.AddMonths(i).Month &&
                            r.DateOfRoutineAppointment.Year == selectedDate.AddMonths(i).Year);

                    if (existingRoutine == null)
                    {
                        var newRoutine = new Routine
                        {
                            Company = selectedCompany,
                            Location = LocationEl.Text,
                            DateOfRoutineAppointment = selectedDate.AddMonths(i),
                            IsFinished = false,
                            DateAdded = DateTime.Now,
                            UserRoutineAppointments = new List<UserRoutineAppointment>(),
                        };

                        newRoutine.UserRoutineAppointments.Add(new UserRoutineAppointment
                        {
                            UserId = selectedUser.Id,
                        });

                        db.Routines.Add(newRoutine);
                    }
                }

                db.SaveChanges();
                this.Frame.Navigate(typeof(PlanningInPage));
            }
            else
            {
                await RoutineDialog.ShowAsync();
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
