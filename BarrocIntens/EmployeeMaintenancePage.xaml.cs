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
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BarrocIntens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeeMaintenancePage : Page
    {
        public Data.User _currentUser;
        public EmployeeMaintenancePage()
        {
            this.InitializeComponent();

            this.InitializeComponent();

            LoadMaintenanceAppointments();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _currentUser = (Data.User)e.Parameter;


        }
        private void LoadMaintenanceAppointments()
        {
            using (var dbContext = new AppDbContext())
            {

                var maintenanceAppointments = dbContext.UserMaintenanceAppointments
                    .Where(uma => uma.UserId == _currentUser.Id)
                    .Join(
                        dbContext.MaintenanceAppointments,
                        uma => uma.MaintenanceAppointmentId,
                        ma => ma.Id,
                        (uma, ma) => ma
                    )
                    .ToList();


                MaintenanceListView.ItemsSource = maintenanceAppointments;
            }
        }
        private void MaintenanceListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedMaintenance = (Maintenance_appointment)e.ClickedItem;

            this.Frame.Navigate(typeof(MaintenanceDetailPage), selectedMaintenance);
        }
    }

}
