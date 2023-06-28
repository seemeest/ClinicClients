using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClinicClients.Data
{
    public static class ThisPage


    {
        public static event Action<Page> PageChanged;

        private static Page _thisPage;
        public static Page thisPage
        {
            get { return _thisPage; }
            set
            {
                if (_thisPage != value)
                {
                    _thisPage = value;
                    PageChanged?.Invoke(value);
                }
            }
        }
    }
}
