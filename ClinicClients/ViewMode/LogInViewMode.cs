using ClinicClients.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicClients.ViewMode
{
    public class LogInViewMode : DependencyObject
    {
        public string ErorText
        {
            get { return (string)GetValue(ErorTextProperty); }
            set { SetValue(ErorTextProperty, value); }
        }

        public static readonly DependencyProperty ErorTextProperty =
            DependencyProperty.Register("ErorText", typeof(string), typeof(LogInViewMode), new PropertyMetadata(""));

        public LogInViewMode()
        {
            Error.AuthErrorChanged += OnAuthErrorChanged;
        }

        private void OnAuthErrorChanged(string authError)
        {
            ErorText = authError;
        }
    }

}
