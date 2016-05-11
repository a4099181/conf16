// =================================================
// <copyright file="FilteredListRequest.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace enovaIntegrator.Client.Models.RequestInfos
{
    [Export( typeof( IRequestInfo ) )]
    sealed class FilteredListRequest : IRequestInfo
    {
        public string Caption => "Filtrowana lista towarów";

        [Import( "products list" )]
        public IResultProcessor ResultProcessor { get; private set; }

        string IRequestInfo.MethodName => "Get";

        Dictionary<string, object> IRequestInfo.MethodArgs
            =>
                new Dictionary<string, object>
                {
                    {"TableName", "Towary"},
                    {"SchemaName", "Produkty"},
                    {"Condition", "[Kod] LIKE 'N%'"}
                };
    }
}