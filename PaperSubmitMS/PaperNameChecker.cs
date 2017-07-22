using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSubmitMS
{
    class PaperNameChecker
    {
        public string CheckFilesNames( string fileName, string path, int fileNameLen)
        {
            if (fileName == string.Empty)
                return "请选择文件类型";
            string failLog = string.Empty;
            string successLog = string.Empty;
         
            DirectoryInfo dir = new DirectoryInfo(path);
       
            FileInfo[] filenames = dir.GetFiles();
            DataTable dt = excelData.Tables["table1"];

            foreach (DataRow dr in dt.Rows)
            {
               
                string number = dr[1].ToString();

                FileInfo file = filenames.SingleOrDefault(f => f.Name.ToString().Substring(13, 11) == number);

                if (file == null)
                {
                    failLog += "学号为" + number + dr[2] + "的" + fileName + "未找到\n";
                    continue;
                }
                if (file.Name.Length != fileNameLen)
                {
                    failLog += "学号为" + number + dr[2] + "的" + fileName + "文件名有问题\n";
                    continue;
                }
                string schoolNumber = file.Name.Substring(0, 5);
                string subjectNumber = file.Name.Substring(6, 6);

                if (schoolNumber != "10423")
                    failLog += "学号为" + number + dr[2].ToString() + "的" + fileName + "学校代码有问题\n";
                if (subjectNumber != dr[6].ToString())
                   failLog += "学号为" + number + dr[2].ToString() + "的" + fileName + "二级学科代码有问题\n";

                 if (schoolNumber == "10423" && subjectNumber == dr[6].ToString())
                     successLog += "学号为" + number + dr[2].ToString() + "的" + fileName + "检查完毕\n";
                // Console.WriteLine(number);
            }
            return failLog + successLog;
        }

        /// <summary>
        /// 读取的excel中的数据
        /// </summary>
        public DataSet  excelData { get; set; }

        /// <summary>
        /// 文件中学号的位置
        /// </summary>
        public int fileNumberPosition { get; set; }

        /// <summary>
        /// 学号长度
        /// </summary>
        public int fileNumberLenth { get; set; }

        /// <summary>
        /// 文件名中二级学科位置
        /// </summary>
        public int fileSubjectNumberPosition { get; set; }

        /// <summary>
        /// 二级学科代码长度
        /// </summary>
        public int subjectNumberLenth { get; set; }

        /// <summary>
        /// 学校代码，海大是10423
        /// </summary>
        public string schoolNumber { get; set; }

        /// <summary>
        /// excel中学号的列号
        /// </summary>
        public int excelNumberPosition { get; set; }

        /// <summary>
        /// excel中学生姓名列号
        /// </summary>
        public int excelStudentNamePosition { get; set; }

        /// <summary>
        /// excel中二级学科列号
        /// </summary>
        public int excelSubjectNumberPosition { get; set; }
    }
}
