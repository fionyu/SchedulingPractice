#define USE_SPINWAIT
using Microsoft.Extensions.Hosting;
using SchedulingPractice.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubWorker.FionDemo
{
    public class GetJobsService : BackgroundService
    {
        private CancellationToken _stop;
        private List<JobInfo> readyJobs = new List<JobInfo>();

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._stop = stoppingToken;

            TimeSpan duration = JobSettings.MinPrepareTime + JobSettings.MaxDelayTime;

            using (JobsRepo repo = new JobsRepo())
            {
                while(true)
                {
                    readyJobs = repo.GetReadyJobs(duration).Where(x=>x.LockAt == null).ToList();
                    Program.JobList.AddRange(readyJobs);
                    try
                    {
                        Console.WriteLine("---------- GetJobsList ----------");
                        await Task.Delay(duration, stoppingToken);
                    }
                    catch (TaskCanceledException) { break; }
                }

            }
        }
    }
}
