// =================================================
// <copyright file="MainWindow.xaml.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using enovaIntegrator.Client.ViewModels;

namespace enovaIntegrator.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var catalog = new AssemblyCatalog( GetType().Assembly ))
            using (var resolver = new CompositionContainer( catalog ))
            {
                foreach (var l in resolver.GetExportedValues<TraceListener>())
                {
                    Debug.Listeners.Add( l );
                }

                resolver.ComposeParts( this );
            }
        }

        [Import( typeof( MainViewModel ) )]
        internal object ViewModel
        {
            get { return DataContext; }
            set { DataContext = value; }
        }
    }
}