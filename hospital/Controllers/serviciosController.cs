using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using hospital.Models;
using hospital.Data;

namespace hospital.Controllers
{
    public class serviciosController : Controller
    {

        DataTable dtservicios = new DataTable();

        // GET: /servicios/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Show All") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Id Servicio" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Id_servicioSortParm"] = sortOrder == "Id_servicio_asc" ? "Id_servicio_desc" : "Id_servicio_asc";
            ViewData["Nombre_servicioSortParm"] = sortOrder == "Nombre_servicio_asc" ? "Nombre_servicio_desc" : "Nombre_servicio_asc";

            dtservicios = serviciosData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtservicios = serviciosData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowservicios in dtservicios.AsEnumerable()
                        select new servicios() {
                            Id_servicio = rowservicios.Field<Int32>("Id_servicio")
                           ,Nombre_servicio = rowservicios.Field<String>("Nombre_servicio")
                        };

            switch (sortOrder)
            {
                case "Id_servicio_desc":
                    Query = Query.OrderByDescending(s => s.Id_servicio);
                    break;
                case "Id_servicio_asc":
                    Query = Query.OrderBy(s => s.Id_servicio);
                    break;
                case "Nombre_servicio_desc":
                    Query = Query.OrderByDescending(s => s.Nombre_servicio);
                    break;
                case "Nombre_servicio_asc":
                    Query = Query.OrderBy(s => s.Nombre_servicio);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Id_servicio);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id Servicio", typeof(string));
                dt.Columns.Add("Nombre Servicio", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Id_servicio
                       ,item.Nombre_servicio
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /servicios/Details/<id>
        public ActionResult Details(
                                      Int32? Id_servicio
                                   )
        {
            if (
                    Id_servicio == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            servicios servicios = new servicios();
            servicios.Id_servicio = System.Convert.ToInt32(Id_servicio);
            servicios = serviciosData.Select_Record(servicios);

            if (servicios == null)
            {
                return HttpNotFound();
            }
            return View(servicios);
        }

        // GET: /servicios/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Nombre_servicio"
				  )] servicios servicios)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = serviciosData.Add(servicios);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(servicios);
        }

        // GET: /servicios/Edit/<id>
        public ActionResult Edit(
                                   Int32? Id_servicio
                                )
        {
            if (
                    Id_servicio == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            servicios servicios = new servicios();
            servicios.Id_servicio = System.Convert.ToInt32(Id_servicio);
            servicios = serviciosData.Select_Record(servicios);

            if (servicios == null)
            {
                return HttpNotFound();
            }

            return View(servicios);
        }

        // POST: /servicios/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(servicios servicios)
        {

            servicios oservicios = new servicios();
            oservicios.Id_servicio = System.Convert.ToInt32(servicios.Id_servicio);
            oservicios = serviciosData.Select_Record(servicios);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = serviciosData.Update(oservicios, servicios);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(servicios);
        }

        // GET: /servicios/Delete/<id>
        public ActionResult Delete(
                                     Int32? Id_servicio
                                  )
        {
            if (
                    Id_servicio == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            servicios servicios = new servicios();
            servicios.Id_servicio = System.Convert.ToInt32(Id_servicio);
            servicios = serviciosData.Select_Record(servicios);

            if (servicios == null)
            {
                return HttpNotFound();
            }
            return View(servicios);
        }

        // POST: /servicios/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Id_servicio
                                            )
        {

            servicios servicios = new servicios();
            servicios.Id_servicio = System.Convert.ToInt32(Id_servicio);
            servicios = serviciosData.Select_Record(servicios);

            bool bSucess = false;
            bSucess = serviciosData.Delete(servicios);
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Delete");
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Id Servicio", Value = "Id Servicio" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre Servicio", Value = "Nombre Servicio" };

                 if (select == "Id Servicio") { Item1.Selected = true; }
            else if (select == "Nombre Servicio") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Servicios", "Many");
                Document document = pdfForm.CreateDocument();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;
                renderer.RenderDocument();

                MemoryStream stream = new MemoryStream();
                renderer.PdfDocument.Save(stream, false);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
                Response.ContentType = "application/Pdf.pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.ClearContent();
                Response.Buffer = true;
                if (Export == "Excel")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
                    Response.ContentType = "application/Excel.xls";
                }
                else if (Export == "Word")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
                    Response.ContentType = "application/Word.doc";
                }
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }
}
 
