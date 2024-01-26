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
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

namespace BarrocIntens
{
    public sealed partial class CreateClientNoteWindow : Window
    {
        private Note userNotes;
        private int userId;
        public DateTimeOffset AppointmentDate { get; set; }
        public List<Company> UserCompanies { get; set; } = new List<Company>();

        public CreateClientNoteWindow(int userId)
        {
            this.InitializeComponent();
            this.userId = userId;

            using (var db = new AppDbContext())
            {
                UserCompanies = db.Companies.Where(c => c.UserId == userId).ToList();

                CompaniesCB.ItemsSource = UserCompanies;
                CompaniesCB.DisplayMemberPath = "Name";
                CompaniesCB.SelectedValuePath = "Id";
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = (Company)CompaniesCB.SelectedItem;
            var companyId = selectedCompany.Id;
            using (var db = new AppDbContext())
            {
                if (userNotes != null)
                {
                    userNotes.Comments = commentsTB.Text;
                    userNotes.AppointmentTitle = appointmentTitleTB.Text;

                    var existingCompany = await db.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

                    if (existingCompany != null)
                    {
                        userNotes.CompanyId = companyId;
                    }
                    else
                    {
                        await wrongCompanyCD.ShowAsync();
                        return;
                    }

                    userNotes.AppointmentDate = DateCreatedPicker.Date.DateTime;

                    db.Notes.Update(userNotes);
                }
                else
                {
                    var existingCompany = db.Companies.FirstOrDefault(c => c.Id == companyId);

                    if (existingCompany != null)
                    {
                        userNotes = new Note
                        {
                            Comments = commentsTB.Text,
                            AppointmentTitle = appointmentTitleTB.Text,
                            UserId = userId,
                            CompanyId = companyId,
                            AppointmentDate = DateCreatedPicker.Date.DateTime
                        };

                        db.Notes.Add(userNotes);
                    }
                    else
                    {
                        await wrongCompanyCD.ShowAsync();
                        return;
                    }
                }

                await db.SaveChangesAsync();

                this.Close();
            }
        }
    }
}
