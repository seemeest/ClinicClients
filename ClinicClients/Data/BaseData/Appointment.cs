using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicClients.Data
{
    public class Appointment
    {
        [JsonProperty("AppointmentId")]
        public int AppointmentId { get; set; }

        [JsonProperty("PatientId")]
        public int PatientId { get; set; }

        [JsonProperty("DoctorId")]
        public int DoctorId { get; set; }

        [JsonProperty("DepartmentId")]
        public int DepartmentId { get; set; }

        [JsonProperty("AppointmentDate")]
        public DateTime AppointmentDate { get; set; }

    }
}
