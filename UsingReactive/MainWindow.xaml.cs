using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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

namespace UsingReactive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Int32> lstIntNum = new List<int>() { 1, 2, 3, 4, 5 };

        Subject<Int32> subject;

        public MainWindow()
        {
            InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            //01
            //IObservable<Int32> input = Observable.Range(1, 15);
            //input.Where(i => i % 2 == 0).Subscribe(x => WriteLog(x.ToString()));

            //02
            //var observer = Observer.Create<int>(
            //                x => WriteLog(x.ToString()), // onNext参数（delegate）
            //                ex => { throw ex; }, // onError参数（delegate）
            //                () => { });  // onCompleted参数（delegate）

            //Observable.Range(1, 5).Subscribe(observer);

            IObservable<Int32> input = lstIntNum.ToObservable();
            subject = new Subject<int>();
            subject.Subscribe((temperature) =>
             WriteLog($"当前温度：{temperature}"));//订阅subject
            subject.Subscribe((temperature) =>
             WriteLog($"嘟嘟嘟，当前水温：{temperature}"));//订阅subject
            input.Subscribe(subject);
            input.Subscribe(i =>
                             WriteLog($"Hello：{i}"));
        }



        private void WriteLog(string msg)
        {
            this.tbx.Text += DateTime.Now.ToString() + ":" + msg + "\n";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lstIntNum.Add(1);
            subject.OnNext(1);
        }
    }
}
