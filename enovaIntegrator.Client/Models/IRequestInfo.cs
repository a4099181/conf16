// =================================================
// <copyright file="IRequestInfo.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.Collections.Generic;

namespace enovaIntegrator.Client.Models
{
    interface IRequestInfo
    {
        string Caption { get; }

        Dictionary<string, object> MethodArgs { get; }

        string MethodName { get; }

        IResultProcessor ResultProcessor { get; }
    }
}