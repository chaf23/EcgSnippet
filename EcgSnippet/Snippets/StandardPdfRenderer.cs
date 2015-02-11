using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using EcgSnippet.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EcgSnippet.Snippets
{
    public class StandardPdfRenderer
    {
        public void Render(Patient patient)
        {
            if (patient.PatientDetails != null && patient.PatientEcg != null)
            {
                RenderPdf(patient);
            }
        }

        private static void RenderPdf(Patient patient)
        {
            var filePath = HostingEnvironment.MapPath("~/Content/Pdf/");
            using (var outputMemoryStream = new FileStream(filePath + @"\\pdf-" + "Test.pdf", FileMode.Create))
            {
                using (var doc = new Document(PageSize.A4))
                {
                    var pdfWriter = PdfWriter.GetInstance(doc, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter {Title = "RHYTHM STRIPS"};
                    doc.Open();
                    var drawPdfPage = new DrawPdfPage();
                    drawPdfPage.Generate(doc, pdfWriter, patient);
                    doc.Close();
                }
            }
        } 
    }
}