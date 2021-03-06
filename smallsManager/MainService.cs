﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace smallsManager
{
    public partial class MainService : ServiceBase
    {
        #region Properties

        System.Timers.Timer mainTimer;
        Logger logger = LogManager.GetLogger("MainService");
        ThresholdWatcher twatcher;

        #endregion Properties

        #region Service Control

        public MainService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            OnStart(null);
            
        }

        protected override void OnStart(string[] args)
        {
            InitializeMainTimer();
        }

        protected override void OnStop()
        {
        }

        #endregion Service Control

        #region Methods

        private void InitializeMainTimer()
        {
            if (mainTimer == null)
            {
                mainTimer = new System.Timers.Timer(30000);
                mainTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
                mainTimer.Start();                
            }
        }

        private void Timer_Elapsed(object state, System.Timers.ElapsedEventArgs e)
        {
            logger.Debug("Running tasks");
            try
            {
                //check if system is on power

                var isOnBattery = WmiHelper.IsPowerOnline();
                if (!isOnBattery)
                    logger.Error("I need power!");

                //cpu threshold alert

                if (twatcher == null)
                {
                    twatcher = new ThresholdWatcher();
                }
                if (twatcher.AddSample(PerformanceHelper.GetTotalCpuUsage()) == true)
                {
                    logger.Error("CPU usage has exceeded the 90% threshold for the past five minutes");
                    twatcher.Reset();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Failed to run task: " + ex.Message);
            }
        }

        #endregion Methods


    }
}
