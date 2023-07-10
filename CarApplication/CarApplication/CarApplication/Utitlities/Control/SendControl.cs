using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CarApplication.Utitlities.Control
{
    public class SendControl
    {
        public SendControl() { }

        public async void Send(string controlCommand)
        {
            Debug.WriteLine($"Control command: {controlCommand}");
            // Creating message  to send over bluetooth
            byte[] messageBytes = Encoding.ASCII.GetBytes(controlCommand);

            var succes = await ((App)Application.Current).Characteristic.WriteAsync(messageBytes);

            Debug.WriteLine($"Sending message: {messageBytes} succesfull: {succes}");

            // If not succesfull try sending the message again
            while (!succes)
            {
                Debug.WriteLine($"Sending message: {messageBytes} succesfull: {succes}");
                succes = await ((App)Application.Current).Characteristic.WriteAsync(messageBytes);

            }
        }
    }
}
