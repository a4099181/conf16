// =================================================
// <copyright file="MyCommand.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System;
using System.Windows.Input;

namespace enovaIntegrator.Client.Models
{
    class MyCommand : ICommand
    {
        readonly Action<object> _execute;

        internal MyCommand( Action<object> execute )
        {
            _execute = execute;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add { }
            remove { }
        }

        bool ICommand.CanExecute( object parameter ) => true;

        void ICommand.Execute( object parameter )
        {
            _execute( parameter );
        }
    }
}