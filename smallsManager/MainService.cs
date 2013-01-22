using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NLog;

namespace smallsManager
{
    public partial class MainService : ServiceBase
    {
        System.Timers.Timer mainTimer;
        Logger logger = LogManager.GetLogger("MainService");
        WasapiOut wasapiOutDevice;
        WaveStream mainOutputStream;
        WaveChannel32 volumeStream;

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

        private void InitializeMainTimer()
        {
            if (mainTimer == null)
            {
                mainTimer = new System.Timers.Timer(30000);
                mainTimer.Elapsed += new System.Timers.ElapsedEventHandler(ForcedTimer_Elapsed);
                mainTimer.Start();                
            }
        }

        private void ForcedTimer_Elapsed(object state, System.Timers.ElapsedEventArgs e)
        {
            logger.Info("Playing sound");
            try
            {
                mainOutputStream = CreateInputStream("C:\\Program Files (x86)\\James\\My Product Name\\Sound\\chime-mid.mp3");
                wasapiOutDevice = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 100);                
                wasapiOutDevice.Init(mainOutputStream);
                wasapiOutDevice.Play();
            }
            catch (Exception ex)
            {
                logger.Error("Failed to play sound: " + ex.Message);
            }
            //CloseWaveOut();
                       
        }

        private WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".mp3"))
            {
                WaveStream mp3Reader = new Mp3FileReader(fileName);         
                inputStream = new  WaveChannel32(mp3Reader);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }
            
            volumeStream = inputStream;
            return volumeStream;
        }

        private void CloseWaveOut()
        {
            if (wasapiOutDevice != null)
            {
                wasapiOutDevice.Stop();
            }
            if (mainOutputStream != null)
            {
                // this one really closes the file and ACM conversion
                volumeStream.Close();
                volumeStream = null;
                // this one does the metering stream
                mainOutputStream.Close();
                mainOutputStream = null;
            }
            if (wasapiOutDevice != null)
            {
                wasapiOutDevice.Dispose();
                wasapiOutDevice = null;
            }
        }

    }
}
