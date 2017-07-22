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

    class ZipCreater
    {
        /// <summary>  
        /// 创建一个 zip  
        /// </summary>  
        public string  CreateZip(string path)
        {
            string logFail = "";
            string logSuccess = "";
            string zipPath = null;
     
            String[] filenames = System.IO.Directory.GetFiles(path);
            DataTable dt = excelData.Tables["table1"];
            foreach (String filename in filenames)
            {
                String filenameWithoudPath = System.IO.Path.GetFileName(filename);
                using (ZipFile zip = new ZipFile())
                {

                    ZipEntry e = zip.AddFile(filename, "");
                    string studentNumber = filenameWithoudPath.Substring(fileNameNumberPosition, fileNameNuberLenth);
                    string password = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string a = dr[0].ToString();
                        if (studentNumber.ToString() == dr[excelNumberPosition].ToString())
                        {
                            //使用身份证号后6位加密
                            password = dr[excelIdNumberPosition].ToString().Substring(12, 6);
                            break;
                        }
                    }
                    if (password == null)
                    {
                        logFail += "学号为" + studentNumber + "的文件加密失败\n";
                      //  Console.WriteLine();
                        continue;
                    }
                    e.Password = password;
                    zipPath = Path.Combine(path, string.Format("{0}.zip", studentNumber));
                    zip.Save(zipPath);
                    logSuccess += "学号为" + studentNumber + "的文件加密成功\n";
                }
            }
            return logFail + logSuccess;
        }
        /// <summary>
        /// 学号在excel中的列数
        /// </summary>
        public int excelNumberPosition { get; set; }
        /// <summary>
        /// 身份证号在excel中的列数
        /// </summary>
        public int excelIdNumberPosition { get; set; }
        /// <summary>
        /// 文件名中学号的位置
        /// </summary>
        public int fileNameNumberPosition { get; set; }
        /// <summary>
        /// 学号长度
        /// </summary>
        public int fileNameNuberLenth { get; set; }
        /// <summary>
        /// 读取的excel中的学号和身份证号
        /// </summary>
        public DataSet excelData { get; set; }
    }
    
}
