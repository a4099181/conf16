// =================================================
// <copyright file="MyXmlListener.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace enovaIntegrator.Client.Models.Listeners
{
    [Export]
    [Export( typeof( TraceListener ) )]
    sealed class MyXmlListener : TraceListener, INotifyPropertyChanged
    {
        public string Text { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void Write( string message )
        {
            Text = message;
            OnPropertyChanged( nameof( Text ) );
        }

        public override void WriteLine( string message )
        {
            Write( message );
        }

        public override void WriteLine( object message, string category )
        {
            var msg = message as string;
            if (msg != null && category == "XML")
            {
                WriteLine( msg );
            }
        }

        void OnPropertyChanged( string propertyName )
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs( propertyName ) );
        }
    }
}