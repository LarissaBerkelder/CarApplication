using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApplication.Utitlities.Bluetooth
{
    public class GetServiceAndCharacteristic
    {
        private IDevice Device { get; set; }
        private IService Service { get; set; }

        readonly private Guid SERVICE_UUID = new Guid("a71ec97a-1f14-11ee-be56-0242ac120002");
        readonly private Guid CHARACTERISTIC_UUID = new Guid("a71ecbfa-1f14-11ee-be56-0242ac120002");
        public bool Succes { get; set; }
        public GetServiceAndCharacteristic(IDevice device)
        {
            Device = device;
            Succes = false;
        }

        public async Task GetSC()
        {
            var services = await Device.GetServicesAsync();
            if (services != null)
            {
                Service = services.FirstOrDefault(d => d.Id == SERVICE_UUID);
            }

            var characteristics = await Service.GetCharacteristicsAsync();
            if (characteristics != null)
            {
                ((App)Application.Current).Characteristic = characteristics.FirstOrDefault(d => d.Id == CHARACTERISTIC_UUID);
                Succes = true;
            }
        }
    }
}
