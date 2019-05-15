using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace SameBrightnessSharp
{
	class WMITool
	{
		public static void BrightnessChanged(EventArrivedEventHandler eventArrivedEventHandler)
		{
			try
			{
				WqlEventQuery query = new WqlEventQuery(
					"SELECT * FROM WmiMonitorBrightnessEvent");

				ManagementEventWatcher watcher = new ManagementEventWatcher(new ManagementScope("root\\WMI"), query);

				watcher.EventArrived += eventArrivedEventHandler;

				watcher.Start();
				return;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public static int GetBrightness() // current brightness should range from 0 to 100
		{
			try
			{
				ManagementObjectSearcher s = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightness");

				foreach (ManagementObject mo in s.Get())
					return int.Parse(mo["CurrentBrightness"].ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return -1;
		}

		public static string GetDisplayInstance() // used in WmiSetBrightness
		{
			try
			{
				ManagementObjectSearcher s = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM WmiMonitorBrightnessMethods");

				foreach (ManagementObject mo in s.Get())
					return mo["InstanceName"].ToString();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return null;
		}

		public static void SetBrightness(string instance, int brightness)
		{
			try
			{
				ManagementObject mo = new ManagementObject("root\\WMI", string.Format("WmiMonitorBrightnessMethods.InstanceName='{0}'", instance), null);

				ManagementBaseObject p = mo.GetMethodParameters("WmiSetBrightness");

				p["Timeout"] = 0;
				p["Brightness"] = brightness;

				mo.InvokeMethod("WmiSetBrightness", p, null);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public static int GetBatteryState() // uint16 BatteryStatus
		{
			try
			{
				ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");

				foreach (ManagementObject mo in objOSDetails.Get())
					return int.Parse(mo["BatteryStatus"].ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return -1;
		}

	}
}
