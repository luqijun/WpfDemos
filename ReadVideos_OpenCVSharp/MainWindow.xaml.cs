using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReadVideos_OpenCVSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private VideoCapture capture;
        private System.Windows.Threading.DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化VideoCapture  
            capture = new VideoCapture("test.mp4");

            // 设置定时器以捕获视频帧  
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30); // 每30毫秒捕获一帧  
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Mat frame = new Mat();
            if (capture.Read(frame))
            {
                // 调试时读取视频会卡
                videoImage.Source = frame.ToBitmapSource();
            }
            else
            {
                // 视频结束  
                timer.Stop();
                capture.Release();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            capture?.Release();
            capture?.Dispose();

            base.OnClosed(e);
        }
    }
}