using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PaperSubmitMS
{
    /// <summary>
    /// CreateZip.xaml 的交互逻辑
    /// </summary>
    public partial class CreateZipWindow : Window
    {
        public CreateZipWindow()
        {
            InitializeComponent();
        }


        private void CreatZip(object sender, RoutedEventArgs e)
        {
            try
            {
                ZipCreater zipCreater = new ZipCreater();

                zipCreater.excelNumberPosition = Int32.Parse(excelNumberPosition.Text) - 1;
                zipCreater.excelIdNumberPosition = Int32.Parse(excelIdNumberPosition.Text) - 1;
                zipCreater.fileNameNumberPosition = Int32.Parse(fileNumberPosition.Text) - 1;
                zipCreater.fileNameNuberLenth = Int32.Parse(fileNumberLength.Text);
                zipCreater.excelData = FilesController.ExcelToDS(excelName.Text);
                result.Text = zipCreater.CreateZip(folderName.Text);
               
            }
            catch (Exception exc)
            {
                result.Text = exc.Message;
            }


        }

        private void GetFolder(object sender, RoutedEventArgs e)
        {
            folderName.Text = FilesController.GetFolder();
        }

        private void GetExcel(object sender, RoutedEventArgs e)
        {
            excelName.Text = FilesController.GetExcelName();
        }

        //20170722_Jerry_限制输入为数字，但是切换中文输入法后没用了，懒得研究了
        private void InputLimit(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void ShowInstructions(object sender, RoutedEventArgs e)
        {
           string Instructions =  "1、点击“选择文件位置”，选择需要加密的论文所在的文件夹。其中论文前12位必须为学号；\n";
           Instructions += "2、点击“选择Excel文件”，默认第一列为学号，第二列为身份证号，可以修改；\n";
           Instructions += "3、点击创建Zip，将在原文件夹下生成加密过的文件，为什么这么做呢？因为开发者懒；\n";
           Instructions += "4、加密失败可能是没有转换为文本，excel单元格转文本方法  http://jingyan.baidu.com/article/d2b1d102965bb05c7e37d404.html\n";
           Instructions += "5、加密后的文件名现实可（ken）能（ding）有问题，不要在意这些细节，解压后可以正常阅读";

           result.Text = Instructions;

        }

    }
}
