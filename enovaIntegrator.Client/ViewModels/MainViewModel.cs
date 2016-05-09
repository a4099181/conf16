// =================================================
// <copyright file="MainViewModel.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using enovaIntegrator.Client.Models;
using enovaIntegrator.Client.Models.Listeners;

namespace enovaIntegrator.Client.ViewModels
{
    [Export]
    sealed class MainViewModel : DependencyObject
    {
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty
            .Register(
                "FontSize",
                typeof( int ),
                typeof( MainViewModel ),
                new PropertyMetadata( 12 ) );

        readonly ServiceInvoker _invoker;

        [ImportingConstructor]
        MainViewModel(
            [Import] ServiceInvoker invoker,
            [ImportMany] IRequestInfo[] requestInfos,
            [Import] MyXmlListener xmlListener,
            [Import] MyListListener listListener )
        {
            _invoker = invoker;
            XmlListener = xmlListener;
            ListListener = listListener;
            RequestInfos = new CollectionView( requestInfos );
        }

        public int FontSize
        {
            get { return ( int ) GetValue( FontSizeProperty ); }
            set { SetValue( FontSizeProperty, value ); }
        }

        public MyListListener ListListener { get; }

        public CollectionView RequestInfos { get; }

        public MyCommand RunCommand => new MyCommand(
            p =>
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    _invoker.Invoke( ( IRequestInfo ) p );
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            } );

        public MyXmlListener XmlListener { get; }
    }
}