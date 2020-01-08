using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isNumber = true;
        private ArrayList numbers = null;
        public MainWindow()
        {
            InitializeComponent();
            buttonStartWordPad.Click += buttonStartWordPad_Click;
            TextBoxForNumbers.KeyDown += TextBoxForNumbers_KeyDown;
            TextBoxForNumbers.PreviewKeyDown += TextBoxForNumbers_PreviewKeyDown;
            buttonConvert.Click += buttonConvert_Click;
        }

        private void buttonConvert_Click(object sender, RoutedEventArgs e)
        {
            if (numbers != null)
            {
                double element;

                if(double.TryParse(TextBoxForNumbers.Text, out element))
                    numbers.Add(element);
                
                using(StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "result.txt", false, Encoding.UTF8))
                {
                    foreach (var val in numbers)
                        streamWriter.WriteLine(val.ToString());
                }
                return;
            }

            MessageBox.Show("You didn't start wordpad or save text");

        }

        private void TextBoxForNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key < Key.D0 || e.Key > Key.D9)
            {
                if(e.Key != Key.Back && e.Key != Key.OemMinus && e.Key != Key.OemPeriod)
                {
                    isNumber = false;
                    return;
                }
            }

            isNumber = true;
        }

        private void TextBoxForNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isNumber)
                e.Handled = true;
        }

        private void buttonStartWordPad_Click(object sender, RoutedEventArgs e)
        {
            Process wordpadProcess = new Process();
            wordpadProcess.StartInfo.FileName = "wordpad.exe";
            wordpadProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;

            wordpadProcess.Start();

            wordpadProcess.WaitForExit();

            var arr = new List<string>();
            string line;

            using (StreamReader stream = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "ex.txt"))
            {
                while ((line = stream.ReadLine()) != null)
                    arr.Add(line);
            }

            string[] words = strRemLet(arr).Split(' ');
            numbers = new ArrayList();

            foreach(var val in words)
            {
                try
                {
                    numbers.Add(Convert.ToDouble(val));
                }
                catch(Exception E)
                {
                    //MessageBox.Show(E.Message);
                    continue;
                }
            }



        }

        private string strRemLet(List<string> lines)
        {
            StringBuilder sym = new StringBuilder();
            StringBuilder result = new StringBuilder();
            Regex regexAlphabet = new Regex("[a-zа-я]+");
            Regex regexSeparator = new Regex("[,]+");
            Regex regexMinus = new Regex("[-]+");
            MatchCollection matchCollection;

            for (int i = 0; i < lines.Count; i++)
            {
                matchCollection = regexAlphabet.Matches(lines[i]);

                foreach(var val in matchCollection)
                {
                    sym.Append(val.ToString());
                }

                lines[i] = regexSeparator.Replace(lines[i], ",");
                lines[i] = regexAlphabet.Replace(lines[i], " ");
                lines[i] = regexMinus.Replace(lines[i], "-");
            }

            using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "strings.txt", false, Encoding.UTF8/*Encoding.GetEncoding("windows-1251")*/))
            {
                streamWriter.Write(sym.ToString());
            }

            foreach (var val in lines)
                result.Append(val + " ");

            return result.ToString();

        }
    }
}
