﻿using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MyEdit.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var w = new MainWindow();
            w.Show();

            main.renderApp(w);
        }

        [STAThread]
        [EntryPoint]
        public static void Main()
        {
            //RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
