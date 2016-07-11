using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PlayerTester
{
    public partial class TesterForm : Form
    {
        
        //constructor
        public TesterForm()
        {
            InitializeComponent();
            Init();
            ButtonStop.Text = "Stop";
            ButtonStop.Enabled = false;
            textBox1.Text = BitsPerSample.ToString();
            textBox2.Text = SamplesPerSecond.ToString();
            textBox3.Text = Channels.ToString();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }

        //Attribute
        WinSound.Player playerOne = new WinSound.Player();
        WinSound.Recorder recorderOne = new WinSound.Recorder();
        List<Byte> RecordedBytes = new List<Byte>();
        public static string FileName = @"C:\Users\kirangda\Documents\Visual Studio 2010\Projects\ASR\ASR\ASR\bin\Debug\Test.wav";
        int SamplesPerSecond = 8000;
        int BitsPerSample = 16;
        int Channels = 1;
        public bool flag = false;

        
        /// Init
        
        private void Init()
        {
            try
            {
                //Events
                recorderOne.DataRecorded += new WinSound.Recorder.DelegateDataRecorded(OnDataRecorded);
                recorderOne.RecordingStopped += new WinSound.Recorder.DelegateStopped(OnRecordingStopped);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// StartRecord
        private void StartRecord()
        {
            try
            {
                if (recorderOne.Started == false)
                {
                    recorderOne.Start(WinSound.WinSound.GetRecordingNames()[1], SamplesPerSecond, BitsPerSample, Channels, 8, 1024);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | StartRecord() | {0}", ex.Message));
            }
        }

       
        /// StopRecord
       
        private void StopRecord()
        {
            try
            {
                if (recorderOne.Started)
                {
                    recorderOne.Stop();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | StopRecord() | {0}", ex.Message));
            }
        }

        
        /// ShowPlaying
        private void ShowPlaying()
        {
            ButtonRecord.Enabled = false;
            Application.DoEvents();
        }

       
        /// FormMain_FormClosing
    
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                recorderOne.DataRecorded -= new WinSound.Recorder.DelegateDataRecorded(OnDataRecorded);
                recorderOne.RecordingStopped -= new WinSound.Recorder.DelegateStopped(OnRecordingStopped);
                bool hrPlayer = playerOne.Close();
                bool hrRecorder = recorderOne.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
      
        /// ButtonRecord_Click
    
        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            StartRecord();
            ButtonRecord.Enabled = false;
            ButtonStop.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopRecord();
            Close();
        }

        private void OnDataRecorded(Byte[] data)
        {
            try
            {
                RecordedBytes.AddRange(data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | On_DataRecorded() | {0}", ex.Message));
            }
        }
    
        /// On_RecordingStopped
        private void OnRecordingStopped()
        {
            try
            {
                // If a valid filename
                if (FileName.Length > 0)
                {
                    // Save Recorded Data
                    if (RecordedBytes.Count > 0)
                    {
                        WinSound.WaveFile.Create(FileName, (uint)SamplesPerSecond, (short)BitsPerSample, (short)Channels, RecordedBytes.ToArray());
                        //Empty data for next shot
                        RecordedBytes.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | On_RecordingStopped() | {0}", ex.Message));
                RecordedBytes.Clear();
            }
        }
    }
}
