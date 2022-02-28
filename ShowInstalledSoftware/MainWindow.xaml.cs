using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace ShowInstalledSoftware
{
   

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("msi.dll", SetLastError = true)]
        static extern int MsiEnumProducts(int iProductIndex, StringBuilder lpProductBuf);
        [DllImport("msi.dll", SetLastError = true)]
        static extern int MsiGetProductInfo(string szProduct, string szProperty, StringBuilder lpValueBuf, ref int pcchValueBuf);

        public MainWindow()
        {
            InitializeComponent();

            ShowInstalledSoftware();
        }

        public void ShowInstalledSoftware()
        {
            StringBuilder result = new StringBuilder();
            for (int index = 0; ; index++)
            {
                StringBuilder productCode = new StringBuilder(39);
                if (MsiEnumProducts(index, productCode) != 0)
                {
                    break;
                }
                foreach (string property in new string[] { "ProductName", "Publisher", "VersionString", })
                {
                    int charCount = 512;
                    StringBuilder value = new StringBuilder(charCount);
                    if (MsiGetProductInfo(productCode.ToString(), property, value, ref charCount) == 0)
                    {
                        value.Length = charCount;
                        result.AppendLine(value.ToString());
                    }
                }
                result.AppendLine();
            }

            this.tbx.Text= result.ToString();
        }
    }
}
