using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcgSnippet.Models
{
    public class PatientEcg
    {
        public string EcgImage { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Lead { get; set; }
    }
}