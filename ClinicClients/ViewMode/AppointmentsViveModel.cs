using ClinicClients.Data;
using ClinicClients.Model;
using ClinicClients.Vive;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
namespace ClinicClients.ViewMode
{
    public class AppointmentsViveModel : BaseViveModel<Appointment>
    {
        public AppointmentsViveModel(DataGrid dataGrid, string Getaddres, string Setaddres) : base(dataGrid, Getaddres, Setaddres)
        {
        }
    }

}


