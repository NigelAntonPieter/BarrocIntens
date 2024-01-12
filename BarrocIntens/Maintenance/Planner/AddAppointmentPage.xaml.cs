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
    public sealed partial class AddAppointmentPage : Page
    {
        public AddAppointmentPage()
        {
            this.InitializeComponent();

            using (var context = new AppDbContext())
            {
                var companies = context.Companies.ToList(); // Veronderst
                CompanyComboBox.ItemsSource = companies.Select(c => new { c.Id, c.Name }).ToList();
                CompanyComboBox.DisplayMemberPath = "Name"; // Toon de bedrijfsnamen
                CompanyComboBox.SelectedValuePath = "Id"; // Gebruik de Id als geselecteerde waarde

            }
           
        }

        private void logoutClick_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void FillCompanyComboBox()
        {
            
        }

        private async void SaveMaintenanceAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (AreAllFieldsFilled())
            {
                Maintenance_appointment newMaintenanceAppointment = CreateMaintenanceAppointment();

                if (newMaintenanceAppointment != null)
                {
                    using (var context = new AppDbContext())
                    {
                        int selectedCompanyId = (int)CompanyComboBox.SelectedValue;
                        newMaintenanceAppointment.CompanyId = selectedCompanyId;

                        context.MaintenanceAppointments.Add(newMaintenanceAppointment);
                        await context.SaveChangesAsync();
                        this.Frame.GoBack();
                    }

                    // Geef een succesmelding of voer andere acties uit
                    // MessageBox.Show("Maintenance Appointment opgeslagen");
                }
                else
                {
                    // Melding weergeven dat er ongeldige gegevens zijn ingevoerd
                    // MessageBox.Show("Ongeldige gegevens ingevoerd");
                }
            }
            else
            {
                // Melding weergeven dat niet alle velden zijn ingevuld
                // MessageBox.Show("Vul alle verplichte velden in");
            }
        }

        private bool AreAllFieldsFilled()
        {
            return !string.IsNullOrWhiteSpace(RemarkTextBox.Text)
                && !string.IsNullOrWhiteSpace(LocationTextBox.Text);
            // Voeg hier andere controles toe indien nodig
        }

        private Maintenance_appointment CreateMaintenanceAppointment()
        {
            if (AreAllFieldsFilled())
            {
                Maintenance_appointment newMaintenanceAppointment = new Maintenance_appointment
                {
                    Remark = RemarkTextBox.Text,
                    Location = LocationTextBox.Text,
                    DateAdded = DateTime.Now, // Geef de huidige datum en tijd
                    IsFinished = false, // Nieuwe afspraak is niet voltooid
                    DateOfMaintenanceAppointment = null, // Laat DateOfMaintenanceAppointment leeg (null)
                                                         // Vul andere velden van Maintenance_appointment in zoals nodig
                                                         // Voorbeeld:
                    Maintenance_ReceiptId = 1, // Of wat de standaardwaarde voor Maintenance_ReceiptId is
                    Maintenance_Receipt = null, // Of de Maintenance_Receipt-relatie, afhankelijk van je logica
                    UserMaintenanceAppointments = new List<UserMaintenanceAppointment>(), // Lege lijst voor koppeling met gebruikers
                                                                                          // En andere velden die nodig zijn voor de afspraak
                };

                return newMaintenanceAppointment;
            }
            else
            {
                return null; // Retourneer null als niet alle verplichte velden zijn ingevuld
            }
        }
    }
}
