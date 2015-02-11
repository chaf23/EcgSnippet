using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EcgSnippet.Models;
using EcgSnippet.Snippets;

namespace EcgSnippet.Controllers
{
    public class EcgStripController : Controller
    {
        private readonly StandardPdfRenderer _standardPdfRenderer;

        public EcgStripController()
        {
            _standardPdfRenderer = new StandardPdfRenderer();
        }

        //
        // GET: /EcgStrip/

        public void Index()
        {
            
        }

        public void Render(Patient patient)
        {
            _standardPdfRenderer.Render(patient);
        }
    }
}
