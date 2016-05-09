// =================================================
// <copyright file="ServiceInvoker.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using enovaIntegrator.Client.enovaIntegratorService;

namespace enovaIntegrator.Client.Models
{
    [Export]
    class ServiceInvoker
    {
        internal void Invoke( IRequestInfo requestInfo )
        {
            var client = new MethodInvokerServiceClient();
            var @params = CreateServiceParams(
                requestInfo.MethodName,
                requestInfo.MethodArgs );

            var result = client.InvokeServiceMethod( @params );

            Trace.WriteLine( result.ResultInstance, "XML" );
            Trace.WriteLine(
                requestInfo.ResultProcessor?.Process( result ),
                "List" );
        }

        static ServiceMethodInvokerParams CreateServiceParams(
            string methodName,
            Dictionary<string, object> args )
            => new ServiceMethodInvokerParams
            {
                DatabaseHandle = Properties.Settings.Default.DatabaseHandle,
                Operator = "Administrator",
                Password = string.Empty,
                ServiceName = "enova.Integrator.IIntegrator, enova.Integrator",
                MethodName = methodName,
                MethodArgs = args
            };
    }
}