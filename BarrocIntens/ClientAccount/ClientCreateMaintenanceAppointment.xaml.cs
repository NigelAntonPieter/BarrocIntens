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

namespace BarrocIntens.ClientAccount
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClientCreateMaintenanceAppointment : Page
    {
        public ClientCreateMaintenanceAppointment()
        {
            this.InitializeComponent();
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
                        newMaintenanceAppointment.CompanyId = (int)Data.User.LoggedInUser.CompanyId;

                        context.MaintenanceAppointments.Add(newMaintenanceAppointment);
                        await context.SaveChangesAsync();
                        this.Frame.GoBack();
                    }
                }
                else
                {
                    await locationDialog.ShowAsync();
                }
            }
            else
            {
                await addApointmentDialog.ShowAsync();
            }
        }

        private bool AreAllFieldsFilled()
        {
            return !string.IsNullOrWhiteSpace(RemarkTextBox.Text)
                && !string.IsNullOrWhiteSpace(LocationTextBox.Text);
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
