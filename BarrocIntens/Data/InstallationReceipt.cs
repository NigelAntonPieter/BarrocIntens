using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ProductId { get; set; }

        [Required]

        public DateTime InstallationDate { get; set; }

        [Required]
        public decimal ConnectionCosts { get; set; }
    }
}

