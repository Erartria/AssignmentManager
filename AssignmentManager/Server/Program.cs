﻿using AssignmentManager.Server.Persistence.Contexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentManager.Server
{
    public class Program
    {
        public static void Main(string[] args) {  
            BuildWebHost(args).Run();
        }  
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup < Startup > ().Build();
    }
}