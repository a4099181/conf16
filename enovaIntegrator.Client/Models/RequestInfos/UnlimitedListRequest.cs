// =================================================
// <copyright file="UnlimitedListRequest.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace enovaIntegrator.Client.Models.RequestInfos
{
    [Export( typeof( IRequestInfo ) )]
    sealed class UnlimitedListRequest : IRequestInfo
    {
        public string Caption => "Nieograniczona lista kontrahentów";

        string IRequestInfo.MethodName => "Get";

        Dictionary<string, object> IRequestInfo.MethodArgs
            =>
                new Dictionary<string, object>
                {
                    {"TableName", "Kontrahenci"},
                    {"SchemaName", "Kontrahenci"}
                };

        [Import( "contractors list" )]
        public IResultProcessor ResultProcessor { get; private set; }
    }
}