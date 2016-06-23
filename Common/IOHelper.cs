using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Common
{
    public class IoHelper
    {
        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            var info = new DirectoryInfo(srcDir);
            var info2 = new DirectoryInfo(tgtDir);
            if (info2.FullName.StartsWith(info.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }
            if (info.Exists)
            {
                if (!info2.Exists)
                {
                    info2.Create();
                }
                FileInfo[] files = info.GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    File.Copy(files[i].FullName, info2.FullName + @"\" + files[i].Name, true);
                }
                DirectoryInfo[] directories = info.GetDirectories();
                for (int j = 0; j < directories.Length; j++)
                {
                    CopyDirectory(directories[j].FullName, info2.FullName + @"\" + directories[j].Name);
                }
            }
        }

        public static void CreateDirectory(string filepath)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(filepath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("路径" + filepath, exception);
            }
        }

        //public static bool DataTableToCSVFile(DataTable tb, string filefullname)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < tb.Columns.Count; i++)
        //    {
        //        builder.Append(ToCSVSafeString(tb.Columns[i].ColumnName) + ",");
        //    }
        //    builder.Remove(builder.Length - 1, 1);
        //    builder.Append("\r\n");
        //    for (int j = 0; j < tb.Rows.Count; j++)
        //    {
        //        for (int k = 0; k < tb.Columns.Count; k++)
        //        {
        //            builder.Append(ToCSVSafeString((tb.Rows[j][k] == null) ? "" : tb.Rows[j][k].ToString()) + ",");
        //        }
        //        builder.Remove(builder.Length - 1, 1);
        //        builder.Append("\r\n");
        //    }
        //    builder.Remove(builder.Length - 2, 2);
        //    string fileName = filefullname;
        //    FileInfo info = new FileInfo(fileName);
        //    if (!info.Directory.Exists)
        //    {
        //        info.Directory.Create();
        //    }
        //    if (File.Exists(fileName))
        //    {
        //        File.Delete(fileName);
        //    }
        //    File.AppendAllText(fileName, builder.ToString(), Encoding.GetEncoding("GBK"));
        //    return true;
        //}

        //public static bool DataTableToExcelFile(DataTable tb, string filefullname)
        //{
        //    try
        //    {
        //        string fileName = filefullname;
        //        FileInfo info = new FileInfo(fileName);
        //        if (!info.Directory.Exists)
        //        {
        //            info.Directory.Create();
        //        }
        //        if (File.Exists(fileName))
        //        {
        //            File.Delete(fileName);
        //        }
        //        Application application = (Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
        //        application.Visible = false;
        //        application.UserControl = true;
        //        object template = Missing.Value;
        //        _Workbook workbook = application.Workbooks.Add(template);
        //        _Worksheet worksheet = workbook.Worksheets.Add(template, template, 1, template) as _Worksheet;
        //        worksheet.Name = "数据导出";
        //        for (int i = 0; i < tb.Columns.Count; i++)
        //        {
        //            DataColumn column = tb.Columns[i];
        //            worksheet.Cells[1, i + 1] = column.ColumnName;
        //            ((Range)worksheet.Columns[i + 1, Type.Missing]).NumberFormatLocal = "@";
        //        }
        //        for (int j = 0; j < tb.Rows.Count; j++)
        //        {
        //            for (int k = 0; k < tb.Columns.Count; k++)
        //            {
        //                string str2 = "";
        //                if (tb.Rows[j][k] != null)
        //                {
        //                    str2 = tb.Rows[j][k].ToString().Trim();
        //                }
        //                worksheet.Cells[j + 2, k + 1] = str2;
        //            }
        //        }
        //        workbook.SaveAs(fileName, template, template, template, template, template, XlSaveAsAccessMode.xlShared, template, template, template, template, template);
        //        application.Quit();
        //        Process[] processesByName = Process.GetProcessesByName("EXCEL");
        //        foreach (Process process in processesByName)
        //        {
        //            if ((process.ProcessName == "EXCEL") && (process.MainWindowTitle == ""))
        //            {
        //                process.Kill();
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception exception)
        //    {
        //        File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "excel.log", exception.Message + "\r\n\r\n");
        //        return false;
        //    }
        //}

        //public static byte[] DateTableToExcelFileBytes(DataTable dt, string fileFullname)
        //{
        //    DataGrid grid = null;
        //    StringWriter writer = null;
        //    HtmlTextWriter writer2 = null;
        //    if (dt != null)
        //    {
        //        writer = new StringWriter();
        //        writer2 = new HtmlTextWriter(writer);
        //        grid = new DataGrid
        //        {
        //            DataSource = dt.DefaultView,
        //            AllowPaging = false
        //        };
        //        grid.DataBind();
        //        grid.RenderControl(writer2);
        //        string s = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=gb2312\"/>" + writer.ToString();
        //        return Encoding.GetEncoding("gb2312").GetBytes(s);
        //    }
        //    return null;
        //}

        public static string ReadTextFile(string path)
        {
            return File.ReadAllText(path);
        }

        private static string ToCSVSafeString(string s)
        {
            s = s ?? "";
            string str = s;
            bool flag = s.Contains(",");
            bool flag2 = s.Contains("\"");
            if (flag2)
            {
                s = s.Replace("\"", "\"\"");
            }
            if (flag || flag2)
            {
                s = "\"" + s + "\"";
            }
            return s;
        }

        public static void WriteTextFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
