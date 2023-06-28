using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicClients.Data
{
    public static class Error
    {
        public static event Action<string> AuthErrorChanged;

        private static string _authError;
        public static string AuthError
        {
            get { return _authError; }
            set
            {
                if (_authError != value)
                {
                    _authError = value;
                    AuthErrorChanged?.Invoke(value);
                }
            }
        }
    }

}
