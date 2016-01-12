﻿using System;
using System.Net;
using System.Threading;
using ServerMonitoring.WindowsAgent.Application;
using ServerMonitoring.WindowsAgent.Pusher;

namespace ServerMonitoring.WindowsAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            var application = new MonitoringApp();

            application.Start();

            while (true)
            {
                var pushData = application.GetDataToPush();

                try
                {
                    HttpPusher.Push(pushData);
                    Console.WriteLine("ok");
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("error");
                }

                Thread.Sleep(1000);
            }
        }
    }
}