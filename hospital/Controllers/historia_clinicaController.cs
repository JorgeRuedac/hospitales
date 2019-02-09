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
    public class historia_clinicaController : Controller
    {

        DataTable dthistoria_clinica = new DataTable();

        // GET: /historia_clinica/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Id Historia" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["id_historiaSortParm"] = sortOrder == "id_historia_asc" ? "id_historia_desc" : "id_historia_asc";
            ViewData["CedulaSortParm"] = sortOrder == "Cedula_asc" ? "Cedula_desc" : "Cedula_asc";
            ViewData["ApellidoSortParm"] = sortOrder == "Apellido_asc" ? "Apellido_desc" : "Apellido_asc";
            ViewData["NombreSortParm"] = sortOrder == "Nombre_asc" ? "Nombre_desc" : "Nombre_asc";
            ViewData["Fecha_nacimSortParm"] = sortOrder == "Fecha_nacim_asc" ? "Fecha_nacim_desc" : "Fecha_nacim_asc";
            ViewData["Num_seguridad_socialSortParm"] = sortOrder == "Num_seguridad_social_asc" ? "Num_seguridad_social_desc" : "Num_seguridad_social_asc";

            dthistoria_clinica = historia_clinicaData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dthistoria_clinica = historia_clinicaData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowhistoria_clinica in dthistoria_clinica.AsEnumerable()
                        select new historia_clinica() {
                            id_historia = rowhistoria_clinica.Field<Int32>("id_historia")
                           ,Cedula = rowhistoria_clinica.Field<String>("Cedula")
                           ,Apellido = rowhistoria_clinica.Field<String>("Apellido")
                           ,Nombre = rowhistoria_clinica.Field<String>("Nombre")
                           ,Fecha_nacim = rowhistoria_clinica.Field<DateTime?>("Fecha_nacim")
                           ,Num_seguridad_social = rowhistoria_clinica.Field<String>("Num_seguridad_social")
                        };

            switch (sortOrder)
            {
                case "id_historia_desc":
                    Query = Query.OrderByDescending(s => s.id_historia);
                    break;
                case "id_historia_asc":
                    Query = Query.OrderBy(s => s.id_historia);
                    break;
                case "Cedula_desc":
                    Query = Query.OrderByDescending(s => s.Cedula);
                    break;
                case "Cedula_asc":
                    Query = Query.OrderBy(s => s.Cedula);
                    break;
                case "Apellido_desc":
                    Query = Query.OrderByDescending(s => s.Apellido);
                    break;
                case "Apellido_asc":
                    Query = Query.OrderBy(s => s.Apellido);
                    break;
                case "Nombre_desc":
                    Query = Query.OrderByDescending(s => s.Nombre);
                    break;
                case "Nombre_asc":
                    Query = Query.OrderBy(s => s.Nombre);
                    break;
                case "Fecha_nacim_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_nacim);
                    break;
                case "Fecha_nacim_asc":
                    Query = Query.OrderBy(s => s.Fecha_nacim);
                    break;
                case "Num_seguridad_social_desc":
                    Query = Query.OrderByDescending(s => s.Num_seguridad_social);
                    break;
                case "Num_seguridad_social_asc":
                    Query = Query.OrderBy(s => s.Num_seguridad_social);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.id_historia);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id Historia", typeof(string));
                dt.Columns.Add("Cedula", typeof(string));
                dt.Columns.Add("Apellido", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Fecha Nacim", typeof(string));
                dt.Columns.Add("Num Seguridad Social", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.id_historia
                       ,item.Cedula
                       ,item.Apellido
                       ,item.Nombre
                       ,item.Fecha_nacim
                       ,item.Num_seguridad_social
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

        // GET: /historia_clinica/Details/<id>
        public ActionResult Details(
                                      Int32? id_historia
                                   )
        {
            if (
                    id_historia == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            historia_clinica historia_clinica = new historia_clinica();
            historia_clinica.id_historia = System.Convert.ToInt32(id_historia);
            historia_clinica = historia_clinicaData.Select_Record(historia_clinica);

            if (historia_clinica == null)
            {
                return HttpNotFound();
            }
            return View(historia_clinica);
        }

        // GET: /historia_clinica/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /historia_clinica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Cedula"
				   + "," + "Apellido"
				   + "," + "Nombre"
				   + "," + "Fecha_nacim"
				   + "," + "Num_seguridad_social"
				  )] historia_clinica historia_clinica)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = historia_clinicaData.Add(historia_clinica);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(historia_clinica);
        }

        // GET: /historia_clinica/Edit/<id>
        public ActionResult Edit(
                                   Int32? id_historia
                                )
        {
            if (
                    id_historia == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            historia_clinica historia_clinica = new historia_clinica();
            historia_clinica.id_historia = System.Convert.ToInt32(id_historia);
            historia_clinica = historia_clinicaData.Select_Record(historia_clinica);

            if (historia_clinica == null)
            {
                return HttpNotFound();
            }

            return View(historia_clinica);
        }

        // POST: /historia_clinica/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(historia_clinica historia_clinica)
        {

            historia_clinica ohistoria_clinica = new historia_clinica();
            ohistoria_clinica.id_historia = System.Convert.ToInt32(historia_clinica.id_historia);
            ohistoria_clinica = historia_clinicaData.Select_Record(historia_clinica);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = historia_clinicaData.Update(ohistoria_clinica, historia_clinica);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(historia_clinica);
        }

        // GET: /historia_clinica/Delete/<id>
        public ActionResult Delete(
                                     Int32? id_historia
                                  )
        {
            if (
                    id_historia == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            historia_clinica historia_clinica = new historia_clinica();
            historia_clinica.id_historia = System.Convert.ToInt32(id_historia);
            historia_clinica = historia_clinicaData.Select_Record(historia_clinica);

            if (historia_clinica == null)
            {
                return HttpNotFound();
            }
            return View(historia_clinica);
        }

        // POST: /historia_clinica/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? id_historia
                                            )
        {

            historia_clinica historia_clinica = new historia_clinica();
            historia_clinica.id_historia = System.Convert.ToInt32(id_historia);
            historia_clinica = historia_clinicaData.Select_Record(historia_clinica);

            bool bSucess = false;
            bSucess = historia_clinicaData.Delete(historia_clinica);
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
            SelectListItem Item1 = new SelectListItem { Text = "Id Historia", Value = "Id Historia" };
            SelectListItem Item2 = new SelectListItem { Text = "Cedula", Value = "Cedula" };
            SelectListItem Item3 = new SelectListItem { Text = "Apellido", Value = "Apellido" };
            SelectListItem Item4 = new SelectListItem { Text = "Nombre", Value = "Nombre" };
            SelectListItem Item5 = new SelectListItem { Text = "Fecha Nacim", Value = "Fecha Nacim" };
            SelectListItem Item6 = new SelectListItem { Text = "Num Seguridad Social", Value = "Num Seguridad Social" };

                 if (select == "Id Historia") { Item1.Selected = true; }
            else if (select == "Cedula") { Item2.Selected = true; }
            else if (select == "Apellido") { Item3.Selected = true; }
            else if (select == "Nombre") { Item4.Selected = true; }
            else if (select == "Fecha Nacim") { Item5.Selected = true; }
            else if (select == "Num Seguridad Social") { Item6.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Historia Clinica", "Many");
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
 
