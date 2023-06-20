using ClinicClients.Data;
using ClinicClients.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicClients.ViewMode
{
    public class PatientViveModel : BaseViveModel<Patient>
    {
        public PatientViveModel(DataGrid dataGrid, string Getaddres, string Setaddres) : base(dataGrid, Getaddres, Setaddres)
        {
        }
    }

}
    

