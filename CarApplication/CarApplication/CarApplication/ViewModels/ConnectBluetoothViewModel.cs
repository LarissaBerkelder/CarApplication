using CarApplication.Models;
using CarApplication.Utitlities.Bluetooth;
using CarApplication.Views;
using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CarApplication.ViewModels
{
    public class ConnectBluetoothViewModel : INotifyPropertyChanged
    {
        // Acitivity indicator should be true when scanning for devices
        private bool activityIndicator;
        public bool ActivityIndicator
        {
            get { return activityIndicator; }
            set
            {
                activityIndicator = value;
                OnPropertyChanged(nameof(ActivityIndicator));
            }
        }

        // List with devices should be true when scanning for devices is completed 
        private bool listVisible;
        public bool ListVisible
        {
            get { return listVisible; }
            set
            {
                listVisible = value;
                OnPropertyChanged(nameof(ListVisible)); 
            }
        }

        // Information label 
        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        // List of bluetooth devices
        private ObservableCollection<DeviceModel> devices;
        public ObservableCollection<DeviceModel> Devices
        {
            get { return devices; }
            set
            {
                devices = value;
                OnPropertyChanged(nameof(Devices));
            }
        }

        // Utilities
        readonly ScanDevices scanDevices = new ScanDevices();
        readonly ConnectToDevice connect = new ConnectToDevice();

        // Bluetooth device
        public readonly string DeviceName = "BLE_Car";
        public IDevice BLEdevice { get; set; }

        public ConnectBluetoothViewModel() 
        {
            Devices = new ObservableCollection<DeviceModel>();
            ListVisible = false;
            ScanAndConnect();
        }

        public async void ScanAndConnect()
        {
            Label = "Scanning for devices";
            ActivityIndicator = true;
            var complete = await scanDevices.StartScanning();

            // Check if the scanning for devices is complete and if the ESP is present in the list 
            if (complete && scanDevices.BluetoothDevices.FirstOrDefault(d => d.Name == DeviceName) != null)
            {
                BLEdevice = scanDevices.BluetoothDevices.FirstOrDefault(d => d.Name == DeviceName);
                // Printing the information to the GUI
                string labelText = "Connecting to " + BLEdevice.Name;
                Label = labelText; 
                Debug.WriteLine($"Connecting to {BLEdevice.Name}");
            }
            else
            {
                // TODO: Add someting if the bluetooth device is not present in the list like restart scanning 
                return; 
            }

            // Connect to the bluetooth device
            Debug.WriteLine("Trying to connect to the device...");
            var connected = await connect.Connect(BLEdevice);
            if (connected)
            {
                Label = "Connected to " + BLEdevice.Name;
                Debug.WriteLine("Connected to the device");
                GetServiceAndCharacteristic get_s_c = new GetServiceAndCharacteristic(BLEdevice);
                await get_s_c.GetSC();
                if (get_s_c.Succes)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ControlPage());
                }
            }

        }
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
