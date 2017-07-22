using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using MessageBox = System.Windows.MessageBox;

namespace PaperSubmitMS
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

        private void EncryptPaper(object sender, RoutedEventArgs e)
        {
            CreateZipWindow createZipWindow = new CreateZipWindow();
            createZipWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckNameWindow checkNameWindow = new CheckNameWindow();
            checkNameWindow.ShowDialog();

        }

    }
}
