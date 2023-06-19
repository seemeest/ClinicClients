using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicClients.Data
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }


    }
}
