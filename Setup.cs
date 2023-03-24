﻿using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace Test_Notissimus
{
	public class Setup : MvxAndroidSetup<App>
	{
		protected override ILoggerProvider CreateLogProvider()
		{
			return new SerilogLoggerProvider();
		}

		protected override ILoggerFactory CreateLogFactory()
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.CreateLogger();

			return new SerilogLoggerFactory();
		}
	}
}