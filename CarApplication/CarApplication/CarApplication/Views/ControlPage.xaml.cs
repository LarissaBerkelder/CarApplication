using CarApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarApplication.Views
{
	public partial class ControlPage : ContentPage
	{
		public ControlPage ()
		{
			InitializeComponent ();
			BindingContext = new ControlViewModel();
		}
	}
}