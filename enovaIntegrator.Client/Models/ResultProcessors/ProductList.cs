// =================================================
// <copyright file="ProductList.cs" company="Soneta sp. z o.o.">
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
    [Export( "products list", typeof( IResultProcessor ) )]
    class ProductList : IResultProcessor
    {
        object IResultProcessor.Process( InvokeServiceMethodResult result )
        {
            var xml = XDocument.Parse( ( string ) result.ResultInstance );
            var items = from row in xml.Descendants( "Row" )
                let innerXml = row.Element( "Xml" )
                let innerXmlText = innerXml.Value.Substring( 1 )
                let towar =
                    XDocument.Parse( innerXmlText ).Element( "Artykul" )
                select new
                {
                    id = ( string ) towar.Element( "ID" ),
                    kod = ( string ) towar.Element( "Kod" ),
                    cenaPodstawowa = GetPrice( towar, "Podstawowa" ),
                    cenaHurtowa = GetPrice( towar, "Hurtowa" )
                };

            return items.Aggregate(
                new StringBuilder(),
                ( sb, i ) => sb.AppendLine(
                    $"| {i.id,3} | {i.kod,-14} " +
                    $"| {i.cenaPodstawowa,12} | {i.cenaHurtowa,12}" ) )
                .ToString();
        }

        static string GetPrice( XContainer towar, string priceName )
        {
            var prices =
                from price in towar.Element( "Ceny" )?.Descendants( "Cena" )
                where ( string ) price.Element( "Nazwa" ) == priceName
                select price;

            return ( string ) prices.Single().Element( "Netto" );
        }
    }
}