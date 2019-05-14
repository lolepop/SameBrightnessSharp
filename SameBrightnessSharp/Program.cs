using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameBrightnessSharp
{
	class Program
	{
		static void Main(string[] args)
		{
			BrightnessMonitor monitor = new BrightnessMonitor();

			monitor.brightnessChanged += (newBrightness) => {
				Console.WriteLine("Brightness changed to " + newBrightness);
			};

			monitor.StartMonitor();


		}

	}
}
