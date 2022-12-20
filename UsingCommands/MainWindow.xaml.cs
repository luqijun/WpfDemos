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

namespace UsingCommands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var commandBinding = new CommandBinding(ApplicationCommands.Close, OnCloseExecuted, OnCloseCanExecuted);
            this.CommandBindings.Add(commandBinding);


            //原理：在RoutedUICommand.ExecuteImpl方法会触发CommandManager.PreviewExecutedEvent事件   -> UIElement.OnPreviewExecutedThunk事件
            //CommandManager.RegisterClassCommandBinding(typeof(MainWindow), commandBinding);
        }

        private void OnCloseCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DemoWindow demoWindow = new DemoWindow();
            demoWindow.ShowDialog();
        }
    }
}
