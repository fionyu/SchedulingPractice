using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SchedulingPractice.Core;

namespace SubWorker.FionDemo
{
    class Program
    {
        public static List<JobInfo> JobList = new List<JobInfo>();

        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<ExecureService>();
                    services.AddHostedService<GetJobsService>();
                })
                .Build();
            using (host)
            {
                host.Start();
                host.WaitForShutdown();
            }
        }
    }


}
