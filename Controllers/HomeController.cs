using Aspose.Pdf;
using Banking.Models;
using ClosedXML.Excel;
using GemBox.Document;
using GemBox.Document.Tables;
using iTextSharp.text.pdf.parser;
using Rebus.Config;
using Spire.Pdf.General.Find;
using Spire.Pdf.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Banking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(FileModel model)
        {
            model.uploaded = false;
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.ToString("dd-MM-yyyy");
            string folderpath = @"C:\Users\Public\Documents\" + date;

            bool directoryExist = Directory.Exists(folderpath);
            if (!directoryExist)
            {
                Directory.CreateDirectory(folderpath);
            }
                string pdfPath = folderpath+@"\" + model.file.FileName;
            // Load PDF document
            string pathToExcel = System.IO.Path.ChangeExtension(pdfPath, ".xls");

            //PdfDocument pdf = new PdfDocument();

            // Convert only tables from PDF to XLS spreadsheet and skip all textual data.
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

            // This property is necessary only for registered version
            //f.Serial = "XXXXXXXXXXX";

            // 'true' = Convert all data to spreadsheet (tabular and even textual).
            // 'false' = Skip textual data and convert only tabular (tables) data.
            f.ExcelOptions.ConvertNonTabularDataToSpreadsheet = false;

            // 'true'  = Preserve original page layout.
            // 'false' = Place tables before text.
            f.ExcelOptions.PreservePageLayout = true;

            // The information includes the names for the culture, the writing system, 
            // the calendar used, the sort order of strings, and formatting for dates and numbers.
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            ci.NumberFormat.NumberDecimalSeparator = ",";
            ci.NumberFormat.NumberGroupSeparator = ".";
            f.ExcelOptions.CultureInfo = ci;
            f.Password = model.password;
            f.OpenPdf(model.file.InputStream);

            if (f.PageCount > 0)
            {
                int result = f.ToExcel(pathToExcel);

                // Open the resulted Excel workbook.
                if (result == 0)
                {
                    System.Diagnostics.Process.Start(pathToExcel);
                }
                model.uploaded = true;
            }

            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}