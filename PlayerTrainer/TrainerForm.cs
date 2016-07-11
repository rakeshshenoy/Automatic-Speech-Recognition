using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PlayerTrainer
{
    public partial class TrainerForm : Form
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public TrainerForm()
        {
            InitializeComponent();
            Init();
            this.label2.Text = "Word";
            button1.Text = "Stop";
            button1.Enabled = false;
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
        public static string FileName = @"C:\Users\kirangda\Documents\Visual Studio 2010\Projects\ASR\ASR\ASR\bin\Debug\Train.wav";
        public static string word = "";
        int SamplesPerSecond = 8000;
        int BitsPerSample = 16;
        int Channels = 1;

        /// <summary>
        /// Init
        /// </summary>
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

        /// <summary>
        /// StartRecord
        /// </summary>
        private void StartRecord()
        {
            try
            {
                if (recorderOne.Started == false)
                {
                    recorderOne.Start(WinSound.WinSound.GetRecordingNames()[1], SamplesPerSecond, BitsPerSample, Channels, 8, 1024);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("FormMain.cs | StartRecord() | {0}", ex.Message));
            }
        }

        /// <summary>
        /// StopRecord
        /// </summary>
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
        /// <summary>
        /// On_RecordingStopped
        /// </summary>
        private void OnRecordingStopped()
        {
            try
            {
                //Bei gültigem Dateiname
                if (FileName.Length > 0)
                {
                    //Aufgenommene Daten speichern
                    if (RecordedBytes.Count > 0)
                    {
                        WinSound.WaveFile.Create(FileName, (uint)SamplesPerSecond, (short)BitsPerSample, (short)Channels, RecordedBytes.ToArray());
                        //Daten für nächste Aufnahme leeren
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
        /// <summary>
        /// ShowPlaying
        /// </summary>
        private void ShowPlaying()
        {
            ButtonRecord.Enabled = false;
            Application.DoEvents();
        }

        /// <summary>
        /// FormMain_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //recorderOne.DataRecorded -= new WinSound.Recorder.DelegateDataRecorded(OnDataRecorded);
                //recorderOne.RecordingStopped -= new WinSound.Recorder.DelegateStopped(OnRecordingStopped);
                bool hrPlayer = playerOne.Close();
                bool hrRecorder = recorderOne.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// ButtonRecord_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            StartRecord();
            ButtonRecord.Enabled = false;
            button1.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            word = textBox4.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopRecord();
            Close();
        }
    }
}
