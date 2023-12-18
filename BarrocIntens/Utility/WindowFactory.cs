using BarrocIntens.Data;
using BarrocIntens.Maintenance;
using BarrocIntens.Maintenance.Planner;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Utility
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
                    return new MaintenanceWindow(user);
                case "MaintenanceAdmin":
                    return new MaintenanceWindow(user);
                case "Finance":
                    return new FinanceWindow(user);
                case "Purchase":
                    return new PurchaseWindow(user);
                case "Planner":
                    return new PlannerWindow(user);
                default:
                    return new ClientWindow(user);
            }
        }
    }
}
