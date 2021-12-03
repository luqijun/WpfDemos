using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace VisualEditor.Python
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            proCmd.StandardInput.WriteLine(input.Text);
            tb.Text +="发送："+ input.Text + "\r\n";
            tb.ScrollToEnd();
        }

        private void UpdateCmd(object sender, DataReceivedEventArgs e)
        {
            tb.Dispatcher.Invoke(new Action(()=> 
            {
                tb.Text += e.Data + "\r\n";
                tb.ScrollToEnd();
                Console.WriteLine(e.Data);
            }));
        }
        Process proCmd = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            proCmd = new Process();
            //proCmd.StartInfo.FileName = @"cmd.exe";
            proCmd.StartInfo.FileName = @"D:\Software4Develop\Anaconda3\envs\Python27\python.exe";
            proCmd.StartInfo.Arguments = @"-i -m pdb C:\Users\11209\Desktop\pythonTest\pTest.py";
            proCmd.StartInfo.UseShellExecute = false;                
            proCmd.StartInfo.CreateNoWindow = true;   
            proCmd.StartInfo.RedirectStandardInput = true;     
            proCmd.StartInfo.RedirectStandardOutput = true;    
            proCmd.StartInfo.RedirectStandardError = true;     
            //proCmd.StandardInput.AutoFlush = true;             //每次调用 Write()之后，将其缓冲区刷新到基础流
            proCmd.OutputDataReceived += UpdateCmd;
            proCmd.ErrorDataReceived += UpdateCmd;

            proCmd.Start();//执行  
            proCmd.BeginOutputReadLine();
            proCmd.BeginErrorReadLine();
        }
    }
}
