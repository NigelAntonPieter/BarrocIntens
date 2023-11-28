using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace BarrocIntens.Data
{
    internal class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string EmailAddress { get; set; }

        public string Street { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Note> Notes { get; set; }
    }

}

