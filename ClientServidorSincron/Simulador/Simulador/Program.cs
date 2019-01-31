using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Simulador
{
    class Program
    {
        static void Main(string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Users\uesoporte20(PolPiedr\Downloads\ClientServidorSincron (1)\ClientServidorSincron\Client\Client\bin\Debug\Client.exe";
            process.StartInfo.Arguments = "Client";
            process.Start();
        }
    }
}
