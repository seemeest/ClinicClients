using ClinicClients.Data;
using ClinicClients.ViewMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClinicClients.Vive
{
    public class MainWindowViewModel : DependencyObject
    {
        public Page FramePage
        {
            get { return (Page)GetValue(FramePageProperty); }
            set { SetValue(FramePageProperty, value); }
        }

        public static readonly DependencyProperty FramePageProperty =
             DependencyProperty.Register("FramePage", typeof(Page), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public MainWindowViewModel()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FramePage = new LogIn();
            });

            ThisPage.PageChanged += OnPageChanged;
        }

        private void OnPageChanged(Page thisPage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FramePage = thisPage;
            });
        }
    }

}
