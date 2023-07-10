using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarApplication.ViewModels;

namespace CarApplication.Views
{
    public partial class ConnectBluetoothPage : ContentPage
    {
        public ConnectBluetoothPage()
        {
            InitializeComponent();
            BindingContext = new ConnectBluetoothViewModel();
        }

    }
}