// =================================================
// <copyright file="MainViewModel.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System;
using System.ComponentModel.Composition;
using System.Text;
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

        public static readonly DependencyProperty ExceptionInfoProperty =
            DependencyProperty.Register(
                "ExceptionInfo",
                typeof( string ),
                typeof( MainViewModel ),
                new PropertyMetadata( default(string) ) );

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

        public string ExceptionInfo
        {
            get { return ( string ) GetValue( ExceptionInfoProperty ); }
            set { SetValue( ExceptionInfoProperty, value ); }
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
                    ExceptionInfo = string.Empty;
                    Mouse.OverrideCursor = Cursors.Wait;
                    _invoker.Invoke( ( IRequestInfo ) p );
                }
                catch (Exception ex)
                {
                    ExceptionInfo = ex.Format( new StringBuilder() );
                }
                finally
                {
                    Mouse.OverrideCursor = null;
                }
            } );

        public MyXmlListener XmlListener { get; }
    }
}