// =================================================
// <copyright file="IResultProcessor.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using enovaIntegrator.Client.enovaIntegratorService;

namespace enovaIntegrator.Client.Models
{
    interface IResultProcessor
    {
        object Process( InvokeServiceMethodResult result );
    }
}