#define USE_SPINWAIT
using Microsoft.Extensions.Hosting;
using SchedulingPractice.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubWorker.FionDemo
{
    public class ExecureService : BackgroundService
    {
        private CancellationToken _stop;
        private List<JobInfo> readyJobs = new List<JobInfo>();

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1);
            this._stop = stoppingToken;

            TimeSpan duration = JobSettings.ExecuteDuration;

            using (JobsRepo repo = new JobsRepo())
            {
                while(true)
                {
                    readyJobs = Program.JobList;
                    while (readyJobs.Count > 0)
                    {
                        var jobs = readyJobs.Where(x => x.RunAt <= DateTime.Now).ToList();
                        foreach (var job in jobs)
                        {
                            if (job.State == 0 && repo.AcquireJobLock(job.Id))
                            {
                                if (repo.ProcessLockedJob(job.Id))
                                {
                                    Console.WriteLine($"[job ID: {job.Id}] update state....");
                                    readyJobs.Remove(job);
                                }
                            }
                        }
                        Program.JobList.RemoveAll(x => jobs.Select(y => y.Id).Contains(x.Id));

                    }

                    //try
                    //{
                    //    Console.WriteLine("---------- Delay ----------");
                    //    await Task.Delay(duration, stoppingToken);
                    //}
                    //catch (TaskCanceledException) { break; }
                }

            }

        }

    }
}
