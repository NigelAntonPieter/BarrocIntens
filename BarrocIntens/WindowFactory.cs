using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens
{
    public interface IWindowFactory
    {
        Window CreateWindow(User user);
    }
    public class WindowFactory : IWindowFactory
    {
        public Window CreateWindow(User user)
        {
            switch (user.Role)
            {
                case "Sales":
                    return new SalesWindow(user);
                case "Maintenance":
                    return new Maintenance(user);
                case "MaintenanceAdmin":
                    return new AdminMaintenanceWindow(user);
                case "Finance":
                    return new FinanceWindow(user);
                case "Purchase":
                    return new PurchaseWindow(user);
                default:
                    return new ClientWindow(user);
            }
        }
    }
}
