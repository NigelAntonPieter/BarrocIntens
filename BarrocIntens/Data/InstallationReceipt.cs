using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Data
{        public class InstallationReceipt
        {
            public int Id { get; set; }
            public string EmployeeName { get; set; }
            public string MachineModel { get; set; }
            public DateTime InstallationDate { get; set; }
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

        class Program
        {
            static void Main()
            {
                Console.WriteLine("Welcome to the Installation Receipt Generator!");

                // Get employee information
                Console.Write("Enter employee name: ");
                string employeeName = Console.ReadLine();

                // Get installation details
                Console.Write("Enter coffee machine model: ");
                string machineModel = Console.ReadLine();

                Console.Write("Enter installation date (YYYY-MM-DD): ");
                DateTime installationDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter connection costs: ");
                decimal connectionCosts = decimal.Parse(Console.ReadLine());

                // Save receipt to the database
                SaveReceiptToDatabase(employeeName, machineModel, installationDate, connectionCosts);

                Console.WriteLine("Installation receipt generated and saved to the database successfully!");
            }

            static void SaveReceiptToDatabase(string employeeName, string machineModel, DateTime installationDate, decimal connectionCosts)
            {
                using (var dbContext = new AppDbContext())
                {
                    // Create a new InstallationReceipt object
                    var receipt = new InstallationReceipt
                    {
                        EmployeeName = employeeName,
                        MachineModel = machineModel,
                        InstallationDate = installationDate,
                        ConnectionCosts = connectionCosts
                    };

                    // Add the receipt to the database
                    dbContext.InstallationReceipts.Add(receipt);
                    dbContext.SaveChanges();
                }
            }
        }
    }

