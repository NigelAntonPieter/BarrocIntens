﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrocIntens.Date
{
    internal class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
    
        public ICollection<User> Users { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
