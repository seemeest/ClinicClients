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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace ClinicClients.ViewMode
{
    internal class DoctorViveModel : BaseViveModel<Doctor>
    {
        public DoctorViveModel(DataGrid dataGrid, string Getaddres, string Setaddres) : base(dataGrid, Getaddres, Setaddres)
        {
        }
    }
}
