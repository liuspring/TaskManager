using System;

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
            var task = new DemoTask();
            task.TestRun();
            Console.ReadLine();
        }
    }
}
