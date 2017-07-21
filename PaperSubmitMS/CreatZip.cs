using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Ionic.Zip;  
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaperSubmitMS
{

    class CreatZip
    {
        
        /// <summary>  
        /// 创建一个 zip  
        /// </summary>  
        public string  CreateZip(DataSet ds,string path,int Number,int IDNumber)
        {
            string logFail = "";
            string logSuccess = "";
           // var baseDir = Path.GetFullPath(@"D:\此文件夹必须解压到D盘根目录下\加密后文件");
            string zipPath = null;


           // var imgDir = Path.GetFullPath(@"D:\此文件夹必须解压到D盘根目录下\原文件，文件名前11位必须为学号");
            var files = Directory.GetFiles(path);
            String[] filenames = System.IO.Directory.GetFiles(path);
            DataTable dt = ds.Tables["table1"];
            foreach (String filename in filenames)
            {
                String filenameWithoudPath = System.IO.Path.GetFileName(filename);
                using (ZipFile zip = new ZipFile())
                {

                    ZipEntry e = zip.AddFile(filename, "");
                    string zipName = filenameWithoudPath.Substring(0, 11);
                    string password = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string a = dr[0].ToString();
                        if (zipName.ToString() == dr[Number].ToString())
                        {
                            //使用身份证号后6位加密
                            password = dr[IDNumber].ToString().Substring(12, 6);
                            break;
                        }
                    }
                    if (password == null)
                    {
                        logFail += "学号为" + zipName + "的文件加密失败\n";
                      //  Console.WriteLine();
                        continue;
                    }
                    e.Password = password;
                    zipPath = Path.Combine(path, string.Format("{0}.zip", zipName));
                    zip.Save(zipPath);
                    logSuccess += "学号为" + zipName + "的文件加密成功\n";
                }
            }
            return logFail + logSuccess;
        }

        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" +
                             "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);

            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            DataTable dt = ds.Tables["table1"];
            conn.Close();
            return ds;
        }
    }
    
}
