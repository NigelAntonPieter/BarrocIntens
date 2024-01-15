using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BarrocIntens
{
    public sealed partial class LeaseContractEditWindow : Window
    {
        private LeaseContract _selectedLeaseContract;

        public LeaseContractEditWindow(LeaseContract selectedLeaseContract)
        {
            this.InitializeComponent();
            _selectedLeaseContract = selectedLeaseContract;

            IsPaidCheckBox.IsChecked = _selectedLeaseContract.IsPaid;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            _selectedLeaseContract.IsPaid = IsPaidCheckBox.IsChecked ?? false;

            using (var dbContext = new AppDbContext())
            {
                dbContext.Update(_selectedLeaseContract);
                dbContext.SaveChanges();
            }

            this.Close();
        }
    }
}
