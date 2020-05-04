using DinkToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WalkingDinner.Models;

namespace WalkingDinner.PDF {

    public static class Letter {

        // public static string GenerateHTML()

        public static void Generate( Couple subject, Couple destination ) {

            string HTML = $@"<html>
                            <head>
                            </head>
                            <body>
                                <h1>Beste { subject.GetName() }</h1>
                                <h3>Jullie volgende bestemming is:</h3>
                                <h2>{ destination.GetName() }</h2>
                            </body>
                            </html>";

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = @"X:\test.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                //PagesCount = true,
                HtmlContent = HTML,
                //WebSettings     = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine( Directory.GetCurrentDirectory(), "assets", "styles.css") },
                //HeaderSettings  = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings  = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var converter = new BasicConverter(new PdfTools());
            //converter.Convert( pdf );

            byte[] pdf = converter.Convert(doc);
        }
    }
}
