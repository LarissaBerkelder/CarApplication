using CarApplication.Utitlities.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarApplication.ViewModels
{
    public class ControlViewModel : INotifyPropertyChanged
    {
        // Up button 
        private bool upPressed;
        public bool UpPressed
        {
            get { return upPressed; }
            set
            {
                if(upPressed != value)
                {
                    upPressed = value;
                    OnPropertyChanged(nameof(UpPressed));
                    ButtonUpPressed();
                }
            }
        }
        // Down button
        private bool downPressed;
        public bool DownPressed
        {
            get { return downPressed; }
            set
            {
                if (downPressed != value)
                {
                    downPressed = value;
                    OnPropertyChanged(nameof(DownPressed));
                    ButtonDownPressed();
                }
            }
        }

        // Left button
        private bool leftPressed;
        public bool LeftPressed
        {
            get { return leftPressed; }
            set
            {
                if (leftPressed != value)
                {
                    leftPressed = value;
                    OnPropertyChanged(nameof(LeftPressed));
                    ButtonLeftPressed();
                }
            }
        }

        // Right button
        private bool rightPressed;
        public bool RightPressed
        {
            get { return rightPressed; }
            set
            {
                if (rightPressed != value)
                {
                    rightPressed = value;
                    OnPropertyChanged(nameof(RightPressed));
                    ButtonRightPressed();
                }
            }
        }


        private int intervalMilliseconds = 150;

        // Utilities
        SendControl sendControl = new SendControl();
        public ControlViewModel() 
        {
        }


        public void ButtonUpPressed()
        {
            if (UpPressed)
            {
                sendControl.Send("UP");
                Device.StartTimer(TimeSpan.FromMilliseconds(intervalMilliseconds), () =>
                {
                    return UpPressed;
                });
            }
            else sendControl.Send("STOP");
        }

        public void ButtonDownPressed()
        {
            if (DownPressed)
            {
                sendControl.Send("DOWN");
                Device.StartTimer(TimeSpan.FromMilliseconds(intervalMilliseconds), () =>
                {
                    return DownPressed;
                });
            }
            else sendControl.Send("STOP");
        }

        public void ButtonLeftPressed()
        {
            if (LeftPressed)
            {
                sendControl.Send("LEFT");
                Device.StartTimer(TimeSpan.FromMilliseconds(intervalMilliseconds), () =>
                {
                    return LeftPressed;
                });
            }
            else sendControl.Send("STOP");
        }

        public void ButtonRightPressed()
        {
            if (RightPressed)
            {
                sendControl.Send("RIGHT");
                Device.StartTimer(TimeSpan.FromMilliseconds(intervalMilliseconds), () =>
                {
                    return RightPressed;
                });
            }
            else sendControl.Send("STOP");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
