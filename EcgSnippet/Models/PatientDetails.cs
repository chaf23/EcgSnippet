using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcgSnippet.Models
{
    public class PatientDetails
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Mrn { get; set; }
    }
}