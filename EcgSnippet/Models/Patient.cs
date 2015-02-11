using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcgSnippet.Models
{
    public class Patient
    {
        public virtual PatientDetails PatientDetails { get; set; }
        public virtual List<PatientEcg> PatientEcg { get; set; }
    }
}