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

        public ClientNotesWindow(int userId)
        {
            this.userId = userId;
            this.InitializeComponent();
            LoadUserNotes();
        }

        private void LoadUserNotes()
        {
            using (var db = new AppDbContext())
            {
                userNotes = db.Notes.FirstOrDefault(n => n.UserId == userId);

                if (userNotes != null)
                {
                    commentsTB.Text = userNotes.Comments;
                    appointmentTitleTB.Text = userNotes.AppointmentTitle;
                    companiesTB.Text = userNotes.CompanyId.ToString();
                    appointmentDateTB.Text = userNotes.AppointmentDate.ToString();
                    appointmentDatePicker.Date = userNotes.AppointmentDate;
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                if (userNotes != null)
                {
                    userNotes.Comments = commentsTB.Text;
                    userNotes.AppointmentTitle = appointmentTitleTB.Text;

                    if (int.TryParse(companiesTB.Text, out int companyId))
                    {
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
                    }
                    else
                    {
                        await wrongInputCD.ShowAsync();
                        return;
                    }

                    userNotes.AppointmentDate = AppointmentDate;

                    db.Notes.Update(userNotes);
                }
                else
                {
                    if (int.TryParse(companiesTB.Text, out int companyId))
                    {
                        var existingCompany = await db.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

                        if (existingCompany != null)
                        {
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
                        else
                        {
                            await wrongCompanyCD.ShowAsync();
                            return;
                        }
                    }
                    else
                    {
                        await wrongInputCD.ShowAsync();
                        return;
                    }
                }

                await db.SaveChangesAsync();

                this.Close();
            }
        }
    }
}