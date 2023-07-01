using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicClients.Data.UserData
{
    public class DoctorsSchedule
    {
        public int apportimentNumber;
        public string PatientName { get; set; }
        public string DeportamentName { get; set; }
        public DateTime appointment_date { get; set; }

    }
}
