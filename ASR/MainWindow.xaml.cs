using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlayerTrainer;
using PlayerTester;
using SpeechRecognition;
using System.Text.RegularExpressions;
using System.IO;

namespace ASR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path = @"C:\Users\kirangda\Documents\Visual Studio 2010\Projects\ASR\ASR\ASR\bin\Debug\Data.txt";
        //consists of all words in database
        public List<string> words = new List<string>();
        //word recognised if a optimal match is found
        string matchedWord = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            TrainerForm trainForm = new TrainerForm();
            trainForm.ShowDialog();
            foreach (string word in words)
                if (word == TrainerForm.word)
                {
                    System.Windows.MessageBox.Show("Word already exists.");
                    return;
                }
            words.Add(TrainerForm.word);
            StoreData();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete(TrainerForm.FileName);
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            //FormMain 
        }

        private void Hyperlink_Click_2(object sender, RoutedEventArgs e)
        {
            string line;
            int i, index = 0, pos = 0;
            double min = 99;
            double match;
            TesterForm testForm = new TesterForm();
            testForm.ShowDialog();
            matchedWord = null;
            if (testForm.flag == true)
            {
                //obj here represents the trained words data
                Feature obj = new Feature();
                //newobj here represents test word data
                Feature newobj = new Feature();
                newobj.getData(TesterForm.FileName);
                newobj.preemphasis();
                newobj.detectVoice();
                newobj.feature_computation();
                StreamReader file = new StreamReader(path);
                line = file.ReadLine();
                while (line != null)
                {
                    i = 0;
                    line = file.ReadLine();
                    obj.realData = new double[int.Parse(line)];
                    line = file.ReadLine();
                    while (line != null && !(Regex.IsMatch(line, @"^[a-z A-Z]+$")))
                    {
                        obj.realData[i++] = double.Parse(line);
                        line = file.ReadLine();
                    }
                    obj.feature_computation();
                    match = obj.compare(newobj);
                    if (match < min)
                    {
                        min = match;
                        pos = index;
                    }
                    index++;
                }
                if (min < 6)
                    matchedWord = words[pos];
                if (matchedWord == null)
                    System.Windows.MessageBox.Show("Word not recognized.");
                else
                    System.Windows.MessageBox.Show("Matched word: " + matchedWord);
                file.Close();
            }
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete(TesterForm.FileName);
        }

        private void StoreData()
        {
            Feature obj = new Feature();
            obj.word = TrainerForm.word;
            obj.getData(TrainerForm.FileName);
            obj.preemphasis();
            obj.detectVoice();
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(obj.word);
            sw.WriteLine(obj.realData.Length);
            foreach (double d in obj.realData)
                sw.WriteLine(d);
            sw.Close();
        }

        [STAThread]
        static void Main(string[] args)
        {
            string line;
            MainWindow window = new MainWindow();
            StreamReader file = new StreamReader(window.path);
            while ((line = file.ReadLine()) != null)
                if (Regex.IsMatch(line, @"^[a-zA-Z]+$"))
                    window.words.Add(line);
            file.Close();
            window.ShowDialog();
        }
    }
}
