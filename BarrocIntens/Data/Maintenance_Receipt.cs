using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Maintenance_Receipt
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } 
        public DateOnly VisitDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } 
        public string CustomerLocation { get; set; } 
        public string ServiceType { get; set; } 
        public string WorkDescription { get; set; } 
        public string MaterialsUsed { get; set; } 
        public decimal LaborHours { get; set; } 
        public decimal MaterialCost { get; set; } 
        public int Maintenance_appointmentId { get; set; }
        public Maintenance_appointment Maintenance_appointment { get; set; }

    }
}
