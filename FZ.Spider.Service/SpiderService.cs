using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
namespace FZ.Spider.Spider.Service
{
	public class SpiderService : System.ServiceProcess.ServiceBase
	{
		private System.Timers.Timer timer1;
		/// <remarks> 
		/// Required designer variable.
		/// </remarks>
		private System.ComponentModel.Container components = null;


        public SpiderService()
		{
            InitializeComponent();
		}
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;


            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new SpiderService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = FZ.Spider.Configuration.Configs.RestartTime;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // AllAskService
            // 
            this.ServiceName = "SpiderService";
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();

		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{			
            
		}

		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
						
		}

		/*
		* Respond to the Elapsed event of the timer control
		*/
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
            this.timer1.Enabled = false;
            FZ.Spider.BS.BForService.ServiceStart();  
            
            this.timer1.Enabled = true;
		}
	}
}