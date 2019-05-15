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
		Thread procThread;

		protected override void OnStart(string[] args)
		{
			procThread = new Thread(() => {
				WriteLog("Service started");

				monitor.BrightnessChanged += (newBrightness) => {
					WriteLog("Brightness changed to " + newBrightness.ToString());
				};

				monitor.StartMonitor();

				Thread.Sleep(Timeout.Infinite);

			});

			procThread.Start();
		}

		[Conditional("DEBUG")]
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
