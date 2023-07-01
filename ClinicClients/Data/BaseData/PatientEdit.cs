using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicClients.Data.BaseData
{
    public class PatientEditName
    {

        public  string Имя { get; set; }
        public  string Фамилия { get; set; }
        public  DateTime ДатаРож { get; set; }
        public  string Пол { get; set; }
        public  string телефон { get; set; }
        public  string адрес { get; set; }

       

    }
    public class PatientDiagnos
    {

        public  string Диагноз;
        public  string Врач;
        public  string Отделение;
        public  string medication;

    }
}
