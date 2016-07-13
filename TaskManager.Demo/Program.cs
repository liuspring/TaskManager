using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Demo
{
    /// <summary>
    /// 仅用于任务本地调试使用
    /// 需要将项目配置为->控制台应用程序
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //var fs = new FileStream("E:\\ak.txt", FileMode.Append);
            ////获得字节数组
            //byte[] data = System.Text.Encoding.Default.GetBytes("Hello World3!");
            ////开始写入
            //fs.Write(data, 0, data.Length);
            ////清空缓冲区、关闭流
            //fs.Flush();
            //fs.Close();
            var task = new DemoTask();
            task.TestRun();
            Console.ReadLine();
        }
    }
}
