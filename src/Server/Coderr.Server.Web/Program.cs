﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Coderr.Client;
using Coderr.Client.ContextCollections;
using Coderr.Server.SqlServer.ReportAnalyzer;
using Griffin.Data.Mapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Coderr.Server.Web
{
    public class Program
    {
        private static ILog _logger;

        public static void Main(string[] args)
        {
            ConfigureLog4Net();
            var provider = new AssemblyScanningMappingProvider();
            provider.Scan(typeof(AnalyticsRepository).Assembly);
            EntityMappingProvider.Provider = provider;

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static void ConfigureLog4Net()
        {
            var env = Environment.GetEnvironmentVariable("WEB_ENVIRONMENT")?.ToLower();

            string logPath;
            if (string.IsNullOrEmpty(env))
            {
                logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            }
            else
            {
                logPath = $"log4net.{env}.config";
                if (!File.Exists(logPath))
                {
                    logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"log4net.{env}.config");
                    if (!File.Exists(logPath))
                        logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                }
            }


            var repos = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(repos, new FileInfo(logPath));
            _logger = LogManager.GetLogger(typeof(Program));
            _logger.Info($"Started {env} from path {logPath}.");
        }
    }


}
