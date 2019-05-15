using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SameBrightnessSharp;
using System.IO;
using System.Globalization;

namespace SameBrightnessService
{
	public partial class BrightnessSvc : ServiceBase
	{
		public BrightnessSvc()
		{
			InitializeComponent();
		}

		BrightnessMonitor monitor = new BrightnessMonitor();

		protected override void OnStart(string[] args)
		{
			new Thread(() => {
				WriteLog("Service started");

				monitor.BrightnessChanged += (newBrightness) => {
					WriteLog("Brightness changed to " + newBrightness.ToString());
				};

				monitor.StartSvcTicker();

			}).Start();

		}

		protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
		{
			WriteLog(powerStatus.ToString("g"));
			
			monitor.Tick(powerStatus == PowerBroadcastStatus.PowerStatusChange);

			return base.OnPowerEvent(powerStatus);
		}

		public void WriteLog(string msg)
		{
			string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
			Directory.CreateDirectory(dir);

			string file = Path.Combine(dir, "svcLog " + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt");

			using (StreamWriter sw = File.AppendText(file))
			{
				sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " + msg);
			}
			
		}

		protected override void OnStop()
		{
		}
	}
}
