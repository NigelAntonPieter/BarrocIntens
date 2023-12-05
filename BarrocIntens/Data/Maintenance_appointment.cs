using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class Maintenance_appointment
    {
        public int Id {  get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Column(TypeName = "longtext")]public string Remark { get; set; }
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }
        public DateOnly DateOfMaintenanceAppointment { get; set; }
        public bool IsFinished { get; set; }
        public ICollection<UserMaintenanceAppointment> UserMaintenanceAppointments { get; set; }
        public int Maintenance_ReceiptId { get; set; }
        public Maintenance_Receipt Maintenance_Receipt { get; set; }

    }
    public class UserMaintenanceAppointment
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MaintenanceAppointmentId { get; set; }
        public Maintenance_appointment MaintenanceAppointment { get; set; }
    }
}
