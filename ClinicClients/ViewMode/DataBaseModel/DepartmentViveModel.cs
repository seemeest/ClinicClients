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
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace ClinicClients.ViewMode
{
    //Department
    internal class DepartmentViveModel  : BaseViveModel<Department>
    {

        public DepartmentViveModel(DataGrid dataGrid, string Getaddres, string Setaddres) : base(dataGrid, Getaddres, Setaddres) { }

    }
}
