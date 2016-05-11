// =================================================
// <copyright file="ContractorsList.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2016 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =================================================

using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using enovaIntegrator.Client.enovaIntegratorService;

namespace enovaIntegrator.Client.Models.ResultProcessors
{
    [Export( "contractors list", typeof( IResultProcessor ) )]
    class ContractorsList : IResultProcessor
    {
        object IResultProcessor.Process( InvokeServiceMethodResult result )
        {
            var xml = XDocument.Parse( ( string ) result.ResultInstance );
            var items = from row in xml.Descendants( "Row" )
                let innerXml = row.Element( "Xml" )
                let innerXmlText = innerXml.Value.Substring( 1 )
                let kontrahent =
                    XDocument.Parse( innerXmlText ).Element( "Kontrahent" )
                select new
                {
                    id = ( string ) kontrahent.Element( "ID" ),
                    kod = ( string ) kontrahent.Element( "Kod" ),
                    nazwa = ( string ) kontrahent.Element( "Nazwa" )
                };

            return items.Aggregate(
                new StringBuilder(),
                ( sb, i ) => sb.AppendLine(
                    $"| {i.id,3} | {i.kod,-14} | {i.nazwa}" ) )
                .ToString();
        }
    }
}