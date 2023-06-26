using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
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

namespace UsingWebsocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ISocketClient _client;

        public MainWindow()
        {
            InitializeComponent();

            this.tbxUrl.Text = "http://172.30.83.53:5000/tsm"; // "http://172.30.83.53:5055/dcenter";
            LogHelper.SetLogHandler(this.Log);
        }

        private void Log(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.tbxOutput.Text += message + "\n";
            });
        }


        private void btnTestConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client = new SocketIOClient(this.tbxUrl.Text.Trim()); //new WebSocketClient(this.tbxUrl.Text.Trim());
                _client.Start();

            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.Message);
            }

        }



        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            _client?.SendMessage(this.tbxInput.Text);
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                this.tbxInputFile.Text = openFileDialog.FileName;
            }
        }

        private async void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbxInputFile.Text))
                return;

            string filename = new System.IO.FileInfo(this.tbxInputFile.Text).Name;

            // Create a new HttpClient object.
            HttpClient client = new HttpClient();

            // Create a new multipart form data request.
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StreamContent(new MemoryStream(File.ReadAllBytes(this.tbxInputFile.Text))), "file", filename);

            // Call the PostAsync() method on the HttpClient object and pass the FormUrlEncodedContent object as a parameter.
            HttpResponseMessage response = await client.PostAsync("http://172.30.83.53:5000/artwork/upload", content);

            // Get the response from the PostAsync() method and check the status code.
            int statusCode = (int)response.StatusCode;
            if (statusCode == 200)
            {
                LogHelper.Info("Image uploaded successfully.");
            }
            else
            {
                LogHelper.Info("Error uploading image.");
            }
        }
    }
}
