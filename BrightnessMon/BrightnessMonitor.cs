using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Management;

namespace SameBrightnessSharp
{
	class BrightnessMonitor
	{
		private readonly string currentInstance;
		private int prevBrightness, prevState;

		public delegate void BrightnessChangedCallback(int newBrightness);
		public event BrightnessChangedCallback BrightnessChanged;

		public BrightnessMonitor()
		{
			currentInstance = WMITool.GetDisplayInstance();

			prevBrightness = WMITool.GetBrightness();
			prevState = WMITool.GetBatteryState();

		}

		public void BrightnessTick(int currBrightness)
		{
			int currState = WMITool.GetBatteryState();

			if (currState != prevState)
			{
				WMITool.SetBrightness(currentInstance, prevBrightness);
				BrightnessChanged?.Invoke(prevBrightness);
			}

			prevState = currState;
			prevBrightness = currBrightness;
		}

		public void StartMonitor()
		{
			WMITool.BrightnessChanged((object sender, EventArrivedEventArgs e) => BrightnessTick(int.Parse(e.NewEvent.Properties["Brightness"].Value.ToString())));
		}
	}
}
