// =================================================
// <copyright file="ExceptionHandlingHelper.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System;
using System.Reflection;
using System.Text;

namespace enovaIntegrator.Client.ViewModels
{
    public static class ExceptionHandlingHelper
    {
        internal static string Format(
            this Exception ex,
            StringBuilder builder )
        {
            if (ex == null)
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                var fileVersion = ( AssemblyFileVersionAttribute ) entryAssembly
                    .GetCustomAttributes(
                        typeof( AssemblyFileVersionAttribute ),
                        false )[ 0 ];
                builder
                    .Append( $"{entryAssembly}" )
                    .Append(
                        $", FileVersion={fileVersion.Version}{Environment.NewLine}" );

                return builder.ToString();
            }

            builder
                .Append( ex.GetType().Name )
                .AppendLine( ex.Message );
            builder.AppendLine( ex.StackTrace );
            builder.AppendLine();

            return ex.InnerException.Format( builder );
        }
    }
}