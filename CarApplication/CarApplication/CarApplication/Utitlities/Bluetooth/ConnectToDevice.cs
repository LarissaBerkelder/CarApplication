using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApplication.Utitlities.Bluetooth
{
    public class ConnectToDevice
    {
        private readonly IAdapter _adapter;

        public ConnectToDevice()
        {
            _adapter = CrossBluetoothLE.Current.Adapter;
        }

        public async Task<bool> Connect(IDevice Device)
        {
            var connectParameters = new ConnectParameters(false, true);
            await _adapter.ConnectToDeviceAsync(Device, connectParameters);
            if(Device.State == DeviceState.Connected)
            {
                return true;
            }
            // TODO: add someting if connecting does not work 
            return false;
        }
    }
}
