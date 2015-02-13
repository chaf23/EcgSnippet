using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using EcgSnippet.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace EcgSnippet.Snippets
{
    public class DrawPdfPage
    {
        private int YPos { get; set; }
        private int XLeftPos { get; set; }
        private int XRightPos { get; set; }
        private int YBottomPos { get; set; }
        private int YTopPost { get; set; }

        public DrawPdfPage()
        {
            //Page Margins
            XLeftPos = 40;
            YBottomPos = 40;
            XRightPos = 555;
            YTopPost = 802;
        }

        public void Generate(Document doc, PdfWriter pdfWriter, Patient patient)
        {
            var cb = pdfWriter.DirectContent;
            DocumentHeader(cb, patient.PatientDetails);

            foreach (var ecg in patient.PatientEcg)
            {
                if (YPos < YBottomPos)
                {
                    doc.NewPage();
                    DocumentHeader(cb, patient.PatientDetails);
                }
                if (!string.IsNullOrEmpty(ecg.EcgImage))
                {
                    cb.AddImage(ConvertBase64ToElement(ecg.EcgImage));
                }
                LineTimeAndLead(cb, ecg);
            }
        }

        private void DocumentHeader(PdfContentByte cb, PatientDetails patientDetails)
        {
            YPos = 678;

            HeaderText(cb, patientDetails);

            YPos = 678;
            //Vertical Line
            cb.MoveTo(296, YPos);
            cb.LineTo(296, YTopPost);
            cb.Stroke();

            //Horizontal Line
            cb.MoveTo(XLeftPos, YPos);
            cb.LineTo(XRightPos, YPos);
            cb.Stroke();

            YPos -= 105;
        }

        private void HeaderText(PdfContentByte cb, PatientDetails patientDetails)
        {
            YPos = YTopPost;
            TextAlignedPdfConfig textAlignedPdf = new TextAlignedPdfConfig();

            YPos -= 18;
            textAlignedPdf.Text = patientDetails.Name;
            textAlignedPdf.X = 320;
            textAlignedPdf.Y = YPos;
            PrintText(cb, textAlignedPdf);

            YPos -= 18;
            textAlignedPdf.Text = "MRN: " + patientDetails.Mrn;
            textAlignedPdf.X = 320;
            textAlignedPdf.Y = YPos;
            PrintText(cb, textAlignedPdf);

            YPos -= 18;
            textAlignedPdf.Text = "DOB: " + patientDetails.Dob.ToString("MM/dd/yyyy");
            textAlignedPdf.X = 320;
            textAlignedPdf.Y = YPos;
            PrintText(cb, textAlignedPdf);

            textAlignedPdf.Text = "Date:" + DateTime.Now.Date.ToString("MM/dd/yyyy");
            textAlignedPdf.X = 50;
            textAlignedPdf.Y = YPos;
            PrintText(cb, textAlignedPdf);
        }

        private Image ConvertBase64ToElement(string base64String)
        {
            Image img = null;

            try
            {
                //  Convert base64string to bytes array
                var bytes = Convert.FromBase64String(base64String);
                img = Image.GetInstance(bytes);

            }
            catch (DocumentException dex)
            {
                //log exception here
            }
            catch (IOException ioex)
            {
                //log exception here
            }
            if (img != null)
            {
                img.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
                img.Border = iTextSharp.text.Rectangle.NO_BORDER;
                img.BorderColor = iTextSharp.text.BaseColor.WHITE;
                //Scale to size
                img.ScaleAbsolute(XRightPos - XLeftPos, 104);
                //XY Coordinates
                img.SetAbsolutePosition(XLeftPos, YPos);
            }

            return img;
        }

        private void LineTimeAndLead(PdfContentByte cb, PatientEcg ecg)
        {
            YPos -= 1;

            //Top Border Line
            cb.MoveTo(XLeftPos, YPos);
            cb.LineTo(XRightPos, YPos);
            cb.Stroke();

            //Time: & Lead: Text
            TimeLeadText(cb, ecg);

            //Bottom Border Line
            YPos -= 46;
            cb.MoveTo(XLeftPos, YPos);
            cb.LineTo(XRightPos, YPos);
            cb.Stroke();

            YPos -= 106;
        }

        private void TimeLeadText(PdfContentByte cb, PatientEcg ecg)
        {
            YPos -= 58;
            var textAlignedPdf = new TextAlignedPdfConfig {Y = YPos};

            var text = "Lead:" + ecg.Lead;
            textAlignedPdf.Text = text;
            textAlignedPdf.X = 320;
            PrintText(cb, textAlignedPdf);

            text = "Time:" + ecg.EndTime.ToString("hh:mm:ss tt") + "-" + ecg.BeginTime.ToString("hh:mm:ss tt");
            textAlignedPdf.Text = text;
            textAlignedPdf.X = 50;
            PrintText(cb, textAlignedPdf);
        }

        private static void PrintText(PdfContentByte cb, TextAlignedPdfConfig parms)
        {
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(bf, 18);

            cb.BeginText();
            cb.ShowTextAligned(0, parms.Text, parms.X, parms.Y, 0);
            cb.EndText();
        }

        private class TextAlignedPdfConfig
        {
            //public int Alignment { get; set; }
            public string Text { get; set; }
            public float X { get; set; }
            public float Y { get; set; }
            //public float Rotation { get; set; }
        }
    }
}