using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarApplication.Views;
using Plugin.BLE.Abstractions.Contracts;

namespace CarApplication
{
    public partial class App : Application
    {
        // Globally accessable Characteristic for bluetooth communication
        public ICharacteristic Characteristic { get; set; }

        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new ConnectBluetoothPage());
            MainPage = navigationPage;
            //var navigationPage = new NavigationPage(new ControlPage());
            //MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
