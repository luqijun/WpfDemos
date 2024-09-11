using Emgu.CV;
using System;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReadVideos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoCapture _capture;
        private DispatcherTimer _timer;
        private Mat _frame;
        private BitmapSource _bitmapSource;


        public MainWindow()
        {
            InitializeComponent();

            // 初始化视频捕获
            _capture = new VideoCapture("test.mp4");
            _frame = new Mat();

            // 设置定时器来更新视频帧
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1.0 / 30); // 30 FPS
            _timer.Tick += ProcessFrame;
            _timer.Start();
        }

        private void ProcessFrame(object? sender, EventArgs e)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                _capture.Read(_frame); // 读取下一帧
                if (!_frame.IsEmpty)
                {
                    // 调试时读取视频会卡
                    imageControl.Source = _frame.ToBitmapSource();

                    // 将Mat转换为Bitmap
                    //using (Bitmap bitmap = _frame.ToBitmap())
                    //{
                    //    // 将Bitmap转换为BitmapSource
                    //    _bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    //        bitmap.GetHbitmap(),
                    //        IntPtr.Zero,
                    //        Int32Rect.Empty,
                    //        BitmapSizeOptions.FromEmptyOptions());

                    //    // 显示在Image控件上
                    //    imageControl.Source = _bitmapSource;
                    //}
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // 释放资源
            if (_capture != null)
                _capture.Dispose();

            if (_frame != null)
                _frame.Dispose();

            if (_timer != null)
                _timer.Stop();
        }
    }
}