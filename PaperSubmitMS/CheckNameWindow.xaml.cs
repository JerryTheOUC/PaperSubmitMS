using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PaperSubmitMS
{
    /// <summary>
    /// CheckNameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckNameWindow : Window
    {
        public CheckNameWindow()
        {
            InitializeComponent();
        }

        private void CheckFilesNames(object sender, RoutedEventArgs e)
        {
            string fileType = selectFileType.SelectionBoxItem.ToString();
            string path = folderName.Text;
            PaperNameChecker paperNameChecker = new PaperNameChecker();
            paperNameChecker.excelData = FilesController.ExcelToDS(excelName.Text);
            ;
            result.Text = paperNameChecker.CheckFilesNames(fileType, path, 31);

            //result.Text += paperNameChecker.CheckFilesNames("自评表", path, 45);
            //  Console.ReadKey();
        }

        //20170722_Jerry_限制输入为数字，但是切换中文输入法后没用了，懒得研究了
        private void InputLimit(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void GetFolder(object sender, RoutedEventArgs e)
        {
            folderName.Text = FilesController.GetFolder();
        }

        private void GetExcel(object sender, RoutedEventArgs e)
        {
            excelName.Text = FilesController.GetExcelName();
        }

        private void selectFileType_SelectionChanged(object sender, EventArgs e)
        {
            if (selectFileType.SelectedIndex.ToString() == "0")
                fileNameLenth.Text  = "31";
            if(selectFileType.SelectedIndex.ToString() == "1")
                fileNameLenth.Text = "44";
        }
    }
}