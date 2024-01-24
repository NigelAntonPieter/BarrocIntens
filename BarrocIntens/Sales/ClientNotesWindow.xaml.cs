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
    public sealed partial class ClientNotesWindow : Window
    {
        private int userId;
        private Note userNotes;
        public DateTimeOffset AppointmentDate { get; set; }

        public ClientNotesWindow(int userId, int noteId)
        {
            this.userId = userId;
            this.InitializeComponent();

            using (var db = new AppDbContext())
            {
                companiesCB.ItemsSource = db.Companies.ToList();
                companiesCB.DisplayMemberPath = "Name";
                companiesCB.SelectedValuePath = "Id";
            }
            LoadUserNotes(noteId);
        }

        private void LoadUserNotes(int noteId)
        {
            using (var db = new AppDbContext())
            {
                userNotes = db.Notes.FirstOrDefault(n => n.Id == noteId);

                if (userNotes != null)
                {
                    commentsTB.Text = userNotes.Comments;
                    appointmentTitleTB.Text = userNotes.AppointmentTitle;
                    //companiesCB.Text = userNotes.Company.Name;
                    companiesCB.SelectedValue = userNotes.CompanyId;
                    appointmentDateTB.Text = userNotes.AppointmentDate.ToString();
                    appointmentDatePicker.Date = userNotes.AppointmentDate;
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = (Company)companiesCB.SelectedItem;
            var companyId = selectedCompany.Id;
            using (var db = new AppDbContext())
            {
                if (userNotes != null)
                {
                    userNotes.Comments = commentsTB.Text;
                    userNotes.AppointmentTitle = appointmentTitleTB.Text;


                    var existingCompany = await db.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

                    userNotes.CompanyId = companyId;

                    userNotes.AppointmentDate = AppointmentDate;

                    db.Notes.Update(userNotes);
                }
                else
                {
                        var existingCompany = await db.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
                            userNotes = new Note
                            {
                                Comments = commentsTB.Text,
                                AppointmentTitle = appointmentTitleTB.Text,
                                UserId = userId,
                                CompanyId = companyId,
                                AppointmentDate = AppointmentDate
                            };

                            db.Notes.Add(userNotes);
                }

                await db.SaveChangesAsync();

                this.Close();
            }
        }
    }
}