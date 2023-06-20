using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicClients.ViewMode.UserModel.ProfileViveModels
{
    public partial class ProfileViveModel 
    {
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(ProfileViveModel), new PropertyMetadata(""));

        public string UserEmail
        {
            get { return (string)GetValue(UserEmailProperty); }
            set { SetValue(UserEmailProperty, value); }
        }

        public static readonly DependencyProperty UserEmailProperty =
            DependencyProperty.Register("UserEmail", typeof(string), typeof(ProfileViveModel), new PropertyMetadata(""));

        public string UserPhone
        {
            get { return (string)GetValue(UserPhoneProperty); }
            set { SetValue(UserPhoneProperty, value); }
        }

        public static readonly DependencyProperty UserPhoneProperty =
            DependencyProperty.Register("UserPhone", typeof(string), typeof(ProfileViveModel), new PropertyMetadata(""));

        public string UserMobile
        {
            get { return (string)GetValue(UserMobileProperty); }
            set { SetValue(UserMobileProperty, value); }
        }

        public static readonly DependencyProperty UserMobileProperty =
            DependencyProperty.Register("UserMobile", typeof(string), typeof(ProfileViveModel), new PropertyMetadata(""));

        public string UserAddress
        {
            get { return (string)GetValue(UserAddressProperty); }
            set { SetValue(UserAddressProperty, value); }
        }

        public static readonly DependencyProperty UserAddressProperty =
            DependencyProperty.Register("UserAddress", typeof(string), typeof(ProfileViveModel), new PropertyMetadata(""));

    
    }
}
