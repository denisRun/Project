using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NixProject
{
    class Guest
    {
        public string FullName;
        public DateTime BirthDate;
        public string Residence;

        public Guest(string fullName, DateTime birthDate, string residence)
        {
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.Residence = residence;
        }
    }
}
