﻿// =================================================
// <copyright file="UpdateRequest.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace enovaIntegrator.Client.Models.RequestInfos
{
    [Export( typeof( IRequestInfo ) )]
    sealed class NewStockRequest : IRequestInfo
    {
        const string XmlData = @"<?xml version=""1.0"" encoding=""utf-8""?>
  <Rows>
    <Row><Xml><![CDATA[﻿<?xml version=""1.0"" encoding=""utf-8""?>
        <Towar>
            <Kod>Nowy</Kod><Nazwa>Nowy</Nazwa>
            <Ceny>
                <Cena>
                    <Nazwa>Podstawowa</Nazwa>
                    <Netto>100.00</Netto>
                </Cena>
                <Cena>
                    <Nazwa>Hurtowa</Nazwa>
                    <Netto>110.00</Netto>
                </Cena>
            </Ceny>
        </Towar>]]></Xml></Row></Rows>";

        public string Caption => "Dodawanie towaru (nowego)";

        string IRequestInfo.MethodName => "Update";

        public IResultProcessor ResultProcessor { get; }

        Dictionary<string, object> IRequestInfo.MethodArgs
            =>
                new Dictionary<string, object>
                {
                    {"TableName", "Towary"},
                    {"SchemaName", "Towary"},
                    {"Data", XmlData}
                };
    }
}