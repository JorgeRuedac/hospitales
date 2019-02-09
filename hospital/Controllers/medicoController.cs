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
    public class medicoController : Controller
    {

        DataTable dtmedico = new DataTable();

        // GET: /medico/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Cod Medico" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Cod_medicoSortParm"] = sortOrder == "Cod_medico_asc" ? "Cod_medico_desc" : "Cod_medico_asc";
            ViewData["CedulaSortParm"] = sortOrder == "Cedula_asc" ? "Cedula_desc" : "Cedula_asc";
            ViewData["Apellido_medicoSortParm"] = sortOrder == "Apellido_medico_asc" ? "Apellido_medico_desc" : "Apellido_medico_asc";
            ViewData["Fecha_nacimienSortParm"] = sortOrder == "Fecha_nacimien_asc" ? "Fecha_nacimien_desc" : "Fecha_nacimien_asc";

            dtmedico = medicoData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtmedico = medicoData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowmedico in dtmedico.AsEnumerable()
                        select new medico() {
                            Cod_medico = rowmedico.Field<Int32>("Cod_medico")
                           ,Cedula = rowmedico.Field<String>("Cedula")
                           ,Apellido_medico = rowmedico.Field<String>("Apellido_medico")
                           ,Fecha_nacimien = rowmedico.Field<DateTime?>("Fecha_nacimien")
                        };

            switch (sortOrder)
            {
                case "Cod_medico_desc":
                    Query = Query.OrderByDescending(s => s.Cod_medico);
                    break;
                case "Cod_medico_asc":
                    Query = Query.OrderBy(s => s.Cod_medico);
                    break;
                case "Cedula_desc":
                    Query = Query.OrderByDescending(s => s.Cedula);
                    break;
                case "Cedula_asc":
                    Query = Query.OrderBy(s => s.Cedula);
                    break;
                case "Apellido_medico_desc":
                    Query = Query.OrderByDescending(s => s.Apellido_medico);
                    break;
                case "Apellido_medico_asc":
                    Query = Query.OrderBy(s => s.Apellido_medico);
                    break;
                case "Fecha_nacimien_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_nacimien);
                    break;
                case "Fecha_nacimien_asc":
                    Query = Query.OrderBy(s => s.Fecha_nacimien);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Cod_medico);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cod Medico", typeof(string));
                dt.Columns.Add("Cedula", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Fecha Nacimien", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Cod_medico
                       ,item.Cedula
                       ,item.Apellido_medico
                       ,item.Fecha_nacimien
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

        // GET: /medico/Details/<id>
        public ActionResult Details(
                                      Int32? Cod_medico
                                   )
        {
            if (
                    Cod_medico == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            medico medico = new medico();
            medico.Cod_medico = System.Convert.ToInt32(Cod_medico);
            medico = medicoData.Select_Record(medico);

            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: /medico/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /medico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Cedula"
				   + "," + "Apellido_medico"
				   + "," + "Fecha_nacimien"
				  )] medico medico)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = medicoData.Add(medico);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(medico);
        }

        // GET: /medico/Edit/<id>
        public ActionResult Edit(
                                   Int32? Cod_medico
                                )
        {
            if (
                    Cod_medico == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            medico medico = new medico();
            medico.Cod_medico = System.Convert.ToInt32(Cod_medico);
            medico = medicoData.Select_Record(medico);

            if (medico == null)
            {
                return HttpNotFound();
            }

            return View(medico);
        }

        // POST: /medico/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(medico medico)
        {

            medico omedico = new medico();
            omedico.Cod_medico = System.Convert.ToInt32(medico.Cod_medico);
            omedico = medicoData.Select_Record(medico);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = medicoData.Update(omedico, medico);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(medico);
        }

        // GET: /medico/Delete/<id>
        public ActionResult Delete(
                                     Int32? Cod_medico
                                  )
        {
            if (
                    Cod_medico == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            medico medico = new medico();
            medico.Cod_medico = System.Convert.ToInt32(Cod_medico);
            medico = medicoData.Select_Record(medico);

            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: /medico/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Cod_medico
                                            )
        {

            medico medico = new medico();
            medico.Cod_medico = System.Convert.ToInt32(Cod_medico);
            medico = medicoData.Select_Record(medico);

            bool bSucess = false;
            bSucess = medicoData.Delete(medico);
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
            SelectListItem Item1 = new SelectListItem { Text = "Cod Medico", Value = "Cod Medico" };
            SelectListItem Item2 = new SelectListItem { Text = "Cedula", Value = "Cedula" };
            SelectListItem Item3 = new SelectListItem { Text = "Nombre", Value = "Nombre" };
            SelectListItem Item4 = new SelectListItem { Text = "Fecha Nacimien", Value = "Fecha Nacimien" };

                 if (select == "Cod Medico") { Item1.Selected = true; }
            else if (select == "Cedula") { Item2.Selected = true; }
            else if (select == "Nombre") { Item3.Selected = true; }
            else if (select == "Fecha Nacimien") { Item4.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Medico", "Many");
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
 
