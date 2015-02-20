﻿/// <summary>
/// ---------------------------------------
/// Battlefield 2 Statistics Control Center
/// ---------------------------------------
/// Created By: Steven Wilson <Wilson212>
/// Copyright (C) 2013-2015, Steven Wilson. All Rights Reserved
/// ---------------------------------------
/// </summary>

using System;
using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using BF2Statistics.Logging;

namespace BF2Statistics
{
    static class Program
    {
        /// <summary>
        /// Specifies the Program Version
        /// </summary>
        public static readonly Version Version = new Version(2, 0, 1);

        /// <summary>
        /// Specifies the installation directory of this program
        /// </summary>
        public static readonly string RootPath = Application.StartupPath;

        /// <summary>
        /// The program wide error log file
        /// </summary>
        public static LogWritter ErrorLog;

        /// <summary>
        /// Returns whether this application is running in administrator mode.
        /// </summary>
        public static bool IsAdministrator
        {
            get
            {
                WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                return wp.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Enable application visual styling
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set Exception Handler
            Application.ThreadException += ExceptionHandler.OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.OnUnhandledException;

            // Create Error Log Writter object
            ErrorLog = new LogWritter(Path.Combine(Application.StartupPath, "Logs", "Error.log"));

            // We only allow 1 instance of this application to run at a time, to prevent all kinds of issues with sockets and such
            // A Mutex will allow us to easily require 1 instance
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, "BF2Statistics Control Center", out createdNew))
            {
                if (createdNew)
                {
                    // Load the main form!
                    Application.Run(new MainForm());
                }
                else
                {
                    // Alert the user
                    MessageBox.Show(
                        "BF2Statistics Control Center is already running. Only one instance of this application can run at a time.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning
                    );
                }
            }
        }
    }
}
