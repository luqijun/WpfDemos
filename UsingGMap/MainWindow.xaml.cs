using GMap.NET;
using GMap.NET.MapProviders;
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

namespace UsingGMap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("cn.bing.com");
                this.mapControl.Manager.Mode = GMap.NET.AccessMode.ServerAndCache;
            }
            catch
            {
                this.mapControl.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            GMapProvider.Language = LanguageType.ChineseSimplified;

            this.mapControl.CacheLocation = Environment.CurrentDirectory + "\\GMapCache\\"; //缓存位置
            this.mapControl.MinZoom = 2;  //最小缩放
            this.mapControl.MaxZoom = 17; //最大缩放
            this.mapControl.Zoom = 5;     //当前缩放
            this.mapControl.DragButton = MouseButton.Left; //左键拖拽地图

            GMapProviders.BingMap.RefererUrl = "https://cn.bing.com/maps";
            this.mapControl.MapProvider = GMapProviders.BingMap;


            this.mapControl.Position = new GMap.NET.PointLatLng(31.235881, 121.480518);





        }
    }
}
