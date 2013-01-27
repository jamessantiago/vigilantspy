using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smallsManager
{
    public class ThresholdWatcher
    {
        private int _sampleInterval { get; set; } 
        private int _thresholdInterval { get; set; } 
        private int _thresholdMaxSample { get; set; }
        private float _threshold { get; set; }
        private string _name {get; set;}
        private bool _thresholdReached {get; set;}
        private Stack<float> _sampleStack { get; set; }


        public int SampleInterval {get { return _sampleInterval;} set {_sampleInterval = value;}}
        public int ThresholdInterval { get { return _thresholdInterval; } set { _thresholdInterval = value; } }
        public int ThresholdMaxSample { get { return _thresholdMaxSample; } set { _thresholdMaxSample= value; } }
        public float Threshold { get { return _threshold; } set { _threshold = value; } }
        public string Name {get { return _name;} set {_name = value;}}
        public bool ThresholdReached {get { return _thresholdReached;} private set {_thresholdReached = value;}}
        public Stack<float> SampleStack { get { return _sampleStack; } private set { _sampleStack = value; } }


        public ThresholdWatcher()
        {
            _sampleStack = new Stack<float>();
            _sampleInterval = 30000;
            _thresholdInterval = 360000;
            _thresholdMaxSample = 20;
            _threshold = 90F;
            _name = "CPU Watcher";
        }

        public ThresholdWatcher(int SampleInterval, int ThresholdInterval, int ThresholdMaxSample, string Name)
        {
            _sampleStack = new Stack<float>();
            _sampleInterval = SampleInterval;
            _thresholdInterval = ThresholdInterval;
            _thresholdMaxSample = ThresholdMaxSample;
            _name = Name;
        }

        public bool AddSample(float Sample)
        {
            _sampleStack.Push(Sample);
            if (_sampleStack.Count > _thresholdMaxSample)
                _sampleStack.Pop();

            return GetThresholdReached();
        }

        private bool GetThresholdReached()
        {
            int samplesToCheck = 1;
            if (_thresholdInterval > _sampleInterval && _sampleInterval > 0)
                samplesToCheck = _thresholdInterval / _sampleInterval;

            if (SampleStack.Count > samplesToCheck)
            {
                float average = SampleStack.Skip(Math.Max(0, SampleStack.Count - samplesToCheck)).Take(samplesToCheck).Average();
                if (average > _threshold)
                    _thresholdReached = true;
                else
                    _thresholdReached = false;
            }
            else
                _thresholdReached = false;
            return _thresholdReached;
        }

        public void Reset()
        {
            _sampleStack.Clear();
        }
    }
}
