using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Threading;

namespace Enterprise.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process[] p = Process.GetProcessesByName("devenv");//获取指定进程信息
            //// Process[] p = Process.GetProcesses();//获取所有进程信息
            //string cpu = string.Empty;
            //string info = string.Empty;

            //PerformanceCounter pp = new PerformanceCounter();//性能计数器
            //pp.CategoryName = "Process";//指定获取计算机进程信息  如果传Processor参数代表查询计算机CPU 
            //pp.CounterName = "% Processor Time";//占有率
            ////如果pp.CategoryName="Processor",那么你这里赋值这个参数 pp.InstanceName = "_Total"代表查询本计算机的总CPU。
            //pp.InstanceName = "devenv";//指定进程 
            //pp.MachineName = ".";
            //if (p.Length > 0)
            //{
            //    foreach (Process pr in p)
            //    {
            //        while (true)//1秒钟读取一次CPU占有率。
            //        {
            //            info = pr.ProcessName + "内存：" +
            //                                    (Convert.ToInt64(pr.WorkingSet64.ToString()) / 1024).ToString();//得到进程内存
            //            Console.WriteLine(info + "    CPU使用情况：" + Math.Round(pp.NextValue(), 2).ToString() + "%");
            //            Thread.Sleep(1000);
            //        }
            //    }
            //}
            GetCpuAndMemory();
        }

        static void GetCpuAndMemory()
        {
            PerformanceCounter cpuCounter;
            PerformanceCounter ramCounter;

            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            while (true)
            {
                Console.WriteLine("CPU Usage: " + Math.Round(cpuCounter.NextValue()) + "%");
                Console.WriteLine("Available Memory: " + ramCounter.NextValue() + "MB");
                Thread.Sleep(1000);
            }
        }
    }
}
