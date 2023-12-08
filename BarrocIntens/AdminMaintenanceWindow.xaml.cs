using BarrocIntens.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
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

namespace BarrocIntens
{
    public sealed partial class AdminMaintenanceWindow : Window
    {
        public AdminMaintenanceWindow(User user)
        {
            this.InitializeComponent();

            using (var dbContext = new AppDbContext())
            {
                var maintenanceAppointments = dbContext.MaintenanceAppointments.Include(ma =>ma.UserMaintenanceAppointments).ToList();
                var Users = dbContext.Users.Include(u => u.UserMaintenanceAppointments).ToList();
                MaintenanceListView.ItemsSource = maintenanceAppointments;
            }
        }
    }
}
