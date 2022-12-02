#region ENBREA Progress - Copyright (C) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA Progress
 *    
 *    Copyright (C) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Threading;

namespace Enbrea.Progress.Demo
{
    class Program
    {
		static void DoSomething(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		static void Main()
		{
			try
			{
				Console.WriteLine("PROGRESS DEMO");
				Console.WriteLine();

				// Count Demo
				var report = new ProgressReport(ProgressUnit.Count)
				{
                    Theme = new ProgressReportTheme()
                    {
                        ProgressTextFormat = "> {0}"
                    },
                    MaxProgressValue = 100
				};

                report.Caption("Callouts");

                report.Information("This is an information.");
                report.Success("This is ok.");
                report.Warning("This is a warning!");
                report.Error("This is an error!!!");
                report.NewLine();

                report.Caption("Count Demo");

				report.Start("Open File");
                DoSomething(5);
                report.Finish();

				report.Start("Load Data (and some dummy text which is very, very, very, very long and tries to cover the complete text line)");
				for (var j = 1; j < 100; ++j)
				{
                    DoSomething(10);
                    report.Continue(j);
				}
				report.Finish(100);

				report.Start("Close File");
                DoSomething(20);
                report.Finish();

				report.Message("100 steps counted");
				report.NewLine();

				// Percent Demo
				report = new ProgressReport(ProgressUnit.Percent)
                {
                    Theme = new ProgressReportTheme()
                    {
                        ProgressTextFormat = "> {0}"
                    }
                };

                report.Caption("Percent Demo");

				report.Start("Open File");
                DoSomething(20);
                report.Finish();

				report.Start("Load Data A");
				for (var i = 1; i <= 100; ++i)
				{
                    DoSomething(10);
                    report.Continue(i);
				}
				report.Finish(100);

				report.Start("Load Data B");
				for (var i = 1; i <= 100; ++i)
				{
                    DoSomething(20);
                    report.Continue(i);
				}
				report.Finish(100);

				report.Message("Data A and B loaded");
				report.NewLine();

                // Custom Value Demo
                report = new ProgressReport()
                {
                    Theme = new ProgressReportTheme()
                    {
                        ProgressTextFormat = "> {0}"
                    }
                };

                report.Caption("Custom Value Demo");

                report.Start("Open File");
                DoSomething(20);
                report.Finish();

                report.Start("Load Data A");
                for (var i = 1; i <= 100; ++i)
                {
                    DoSomething(10);
                    report.Continue("{0}/{1}", i, i*10);
                }
                report.Finish();

                report.Start("Load Data B");
                for (var i = 1; i <= 100; ++i)
                {
                    DoSomething(20);
                    report.Continue("{0}/{1}", i, i * 2);
                }
                report.Finish("{0}/{1}", 100, 200);

                report.Message("Data A and B loaded");
                report.NewLine();

				// Failed Demo
                report = new ProgressReport(ProgressUnit.Percent)
				{
                    Theme = new ProgressReportTheme()
                    {
                        ProgressTextFormat = "> {0}"
                    }
                };

				report.Caption("Failed Demo");

				report.Start("Open File");
                DoSomething(20);
                report.Finish();

				var Failed = false;

				report.Start("Load Data");
				try
				{
					for (var i = 1; i <= 100; ++i)
					{
                        DoSomething(10);
                        report.Continue(i);
						if (i == 50) throw new Exception("Error!");
					}
					report.Finish();
				}
				catch
				{
					report.Cancel();
					report.NewLine();
					Failed = true;
				}

				if (!Failed)
				{
					report.Start("Close File");
                    DoSomething(5);
                    report.Finish();

					report.Message("100 steps counted");
					report.NewLine();
				}

				// File Donwload Demo
				report = new ProgressReport(ProgressUnit.FileSize)
                {
                    Theme = new ProgressReportTheme()
					{
                        CaptionFormat = "** {0} **",
                        CaptionTextColor = ConsoleColor.Blue,
						MessageTextColor = ConsoleColor.Green,
						ProgressTextFormat = "> {0}"
					},
					MaxProgressValue = 2048
				};

                report.Caption("File Download Demo");

				report.Start("Connect");
                DoSomething(5);
                report.Finish();

				report.Start("Download File");
				for (var j = 1; j <= 2048; ++j)
				{
                    DoSomething(5);
                    report.Continue(j);
				}
				report.Finish();

				report.Start("Disconnect");
                DoSomething(5);
                report.Finish();

				report.Success(">", "2048 Bytes downloaded");
				report.NewLine();

			}
			catch (Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine(ex.ToString()); 
			}
		}
	}
}