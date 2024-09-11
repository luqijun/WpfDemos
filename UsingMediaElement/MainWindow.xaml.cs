using System;
using System.Collections.Generic;
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
using Path = System.IO.Path;

namespace UsingMediaElement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.mediaElement.LoadedBehavior = MediaState.Manual;
            this.mediaElement.Source = new Uri("Resources/test.wmv", UriKind.Relative);
            this.mediaElement.Play();
        }

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {
           
        }

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {

        }

        private void Element_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // 必须确保 Windows Media Player Version 10 以上 
        }
    }
}
