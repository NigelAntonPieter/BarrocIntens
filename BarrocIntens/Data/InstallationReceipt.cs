using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{
    public class InstallationReceipt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string MachineModel { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [Required]
        public decimal ConnectionCosts { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<InstallationReceipt> InstallationReceipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;port=3306;user=root;password=KutLuna;database=BarrocIntens");
        }
    }
}
