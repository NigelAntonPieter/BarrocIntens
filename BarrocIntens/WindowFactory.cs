using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens
{
    internal class WindowFactory : IWindowFactory
    {
        public Window CreateWindow(string role)
        {
            switch (role)
            {
                case "Sales":
                    return new SalesWindow();
                case "Maintenance":
                    return new Maintenance();
                case "MaintenanceAdmin":
                    return new AdminMaintenanceWindow();
                case "Finance":
                    return new FinanceWindow();
                case "Purchase":
                    return new PurchaseWindow();
                default:
                    return new ClientWindow();
            }
        }
    }
}
