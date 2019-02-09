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
    public class ingresosController : Controller
    {

        DataTable dtingresos = new DataTable();
        DataTable A24dtcamas = new DataTable();
        DataTable A25dthistoria_clinica = new DataTable();

        // GET: /ingresos/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Num Habitacion" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Num_habitacionSortParm"] = sortOrder == "Num_habitacion_asc" ? "Num_habitacion_desc" : "Num_habitacion_asc";
            ViewData["ComentarioSortParm"] = sortOrder == "Comentario_asc" ? "Comentario_desc" : "Comentario_asc";
            ViewData["Fecha_ingresoSortParm"] = sortOrder == "Fecha_ingreso_asc" ? "Fecha_ingreso_desc" : "Fecha_ingreso_asc";
            ViewData["Fecha_salidaSortParm"] = sortOrder == "Fecha_salida_asc" ? "Fecha_salida_desc" : "Fecha_salida_asc";
            ViewData["id_camaSortParm"] = sortOrder == "id_cama_asc" ? "id_cama_desc" : "id_cama_asc";
            ViewData["id_historiaSortParm"] = sortOrder == "id_historia_asc" ? "id_historia_desc" : "id_historia_asc";

            dtingresos = ingresosData.SelectAll();
            A24dtcamas = ingresos_camasData.SelectAll();
            A25dthistoria_clinica = ingresos_historia_clinicaData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtingresos = ingresosData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowingresos in dtingresos.AsEnumerable()
                        join A24rowcamas in A24dtcamas.AsEnumerable() on rowingresos.Field<Int32?>("id_cama") equals A24rowcamas.Field<Int32>("id_cama")
                        join A25rowhistoria_clinica in A25dthistoria_clinica.AsEnumerable() on rowingresos.Field<Int32?>("id_historia") equals A25rowhistoria_clinica.Field<Int32>("id_historia")
                        select new ingresos() {
                            Num_habitacion = rowingresos.Field<Int32>("Num_habitacion")
                           ,Comentario = rowingresos.Field<String>("Comentario")
                           ,Fecha_ingreso = rowingresos.Field<DateTime?>("Fecha_ingreso")
                           ,Fecha_salida = rowingresos.Field<DateTime?>("Fecha_salida")
                           ,
                            camas = new camas() 
                            {
                                   id_cama = A24rowcamas.Field<Int32>("id_cama")
                                  ,Num_cama = A24rowcamas.Field<Int32>("Num_cama")
                            }
                           ,
                            historia_clinica = new historia_clinica() 
                            {
                                   id_historia = A25rowhistoria_clinica.Field<Int32>("id_historia")
                                  ,Cedula = A25rowhistoria_clinica.Field<String>("Cedula")
                            }
                        };

            switch (sortOrder)
            {
                case "Num_habitacion_desc":
                    Query = Query.OrderByDescending(s => s.Num_habitacion);
                    break;
                case "Num_habitacion_asc":
                    Query = Query.OrderBy(s => s.Num_habitacion);
                    break;
                case "Comentario_desc":
                    Query = Query.OrderByDescending(s => s.Comentario);
                    break;
                case "Comentario_asc":
                    Query = Query.OrderBy(s => s.Comentario);
                    break;
                case "Fecha_ingreso_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_ingreso);
                    break;
                case "Fecha_ingreso_asc":
                    Query = Query.OrderBy(s => s.Fecha_ingreso);
                    break;
                case "Fecha_salida_desc":
                    Query = Query.OrderByDescending(s => s.Fecha_salida);
                    break;
                case "Fecha_salida_asc":
                    Query = Query.OrderBy(s => s.Fecha_salida);
                    break;
                case "id_cama_desc":
                    Query = Query.OrderByDescending(s => s.camas.Num_cama);
                    break;
                case "id_cama_asc":
                    Query = Query.OrderBy(s => s.camas.Num_cama);
                    break;
                case "id_historia_desc":
                    Query = Query.OrderByDescending(s => s.historia_clinica.Cedula);
                    break;
                case "id_historia_asc":
                    Query = Query.OrderBy(s => s.historia_clinica.Cedula);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Num_habitacion);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Num Habitacion", typeof(string));
                dt.Columns.Add("Comentario", typeof(string));
                dt.Columns.Add("Fecha Ingreso", typeof(string));
                dt.Columns.Add("Fecha Salida", typeof(string));
                dt.Columns.Add("Num Cama", typeof(string));
                dt.Columns.Add("Historia", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Num_habitacion
                       ,item.Comentario
                       ,item.Fecha_ingreso
                       ,item.Fecha_salida
                       ,item.camas.Num_cama
                       ,item.historia_clinica.Cedula
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

        // GET: /ingresos/Details/<id>
        public ActionResult Details(
                                      Int32? Num_habitacion
                                   )
        {
            if (
                    Num_habitacion == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A24dtcamas = ingresos_camasData.SelectAll();
            A25dthistoria_clinica = ingresos_historia_clinicaData.SelectAll();

            ingresos ingresos = new ingresos();
            ingresos.Num_habitacion = System.Convert.ToInt32(Num_habitacion);
            ingresos = ingresosData.Select_Record(ingresos);
            ingresos.camas = new camas()
            {
                id_cama = (Int32)ingresos.id_cama
               ,Num_cama = (from DataRow A24rowcamas in A24dtcamas.Rows
                      where ingresos.id_cama == (int)A24rowcamas["id_cama"]
                      select (Int32)A24rowcamas["Num_cama"]).FirstOrDefault()
            };
            ingresos.historia_clinica = new historia_clinica()
            {
                id_historia = (Int32)ingresos.id_historia
               ,Cedula = (from DataRow A25rowhistoria_clinica in A25dthistoria_clinica.Rows
                      where ingresos.id_historia == (int)A25rowhistoria_clinica["id_historia"]
                      select (String)A25rowhistoria_clinica["Cedula"]).FirstOrDefault()
            };

            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // GET: /ingresos/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["id_cama"] = new SelectList(ingresos_camasData.List(), "id_cama", "Num_cama");
            ViewData["id_historia"] = new SelectList(ingresos_historia_clinicaData.List(), "id_historia", "Cedula");

            return View();
        }

        // POST: /ingresos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Comentario"
				   + "," + "Fecha_ingreso"
				   + "," + "Fecha_salida"
				   + "," + "id_cama"
				   + "," + "id_historia"
				  )] ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ingresosData.Add(ingresos);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
        // ComboBox
            ViewData["id_cama"] = new SelectList(ingresos_camasData.List(), "id_cama", "Num_cama", ingresos.id_cama);
            ViewData["id_historia"] = new SelectList(ingresos_historia_clinicaData.List(), "id_historia", "Cedula", ingresos.id_historia);

            return View(ingresos);
        }

        // GET: /ingresos/Edit/<id>
        public ActionResult Edit(
                                   Int32? Num_habitacion
                                )
        {
            if (
                    Num_habitacion == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ingresos ingresos = new ingresos();
            ingresos.Num_habitacion = System.Convert.ToInt32(Num_habitacion);
            ingresos = ingresosData.Select_Record(ingresos);

            if (ingresos == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["id_cama"] = new SelectList(ingresos_camasData.List(), "id_cama", "Num_cama", ingresos.id_cama);
            ViewData["id_historia"] = new SelectList(ingresos_historia_clinicaData.List(), "id_historia", "Cedula", ingresos.id_historia);

            return View(ingresos);
        }

        // POST: /ingresos/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ingresos ingresos)
        {

            ingresos oingresos = new ingresos();
            oingresos.Num_habitacion = System.Convert.ToInt32(ingresos.Num_habitacion);
            oingresos = ingresosData.Select_Record(ingresos);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ingresosData.Update(oingresos, ingresos);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
        // ComboBox
            ViewData["id_cama"] = new SelectList(ingresos_camasData.List(), "id_cama", "Num_cama", ingresos.id_cama);
            ViewData["id_historia"] = new SelectList(ingresos_historia_clinicaData.List(), "id_historia", "Cedula", ingresos.id_historia);

            return View(ingresos);
        }

        // GET: /ingresos/Delete/<id>
        public ActionResult Delete(
                                     Int32? Num_habitacion
                                  )
        {
            if (
                    Num_habitacion == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A24dtcamas = ingresos_camasData.SelectAll();
            A25dthistoria_clinica = ingresos_historia_clinicaData.SelectAll();

            ingresos ingresos = new ingresos();
            ingresos.Num_habitacion = System.Convert.ToInt32(Num_habitacion);
            ingresos = ingresosData.Select_Record(ingresos);
            ingresos.camas = new camas()
            {
                id_cama = (Int32)ingresos.id_cama
               ,Num_cama = (from DataRow A24rowcamas in A24dtcamas.Rows
                      where ingresos.id_cama == (int)A24rowcamas["id_cama"]
                      select (Int32)A24rowcamas["Num_cama"]).FirstOrDefault()
            };
            ingresos.historia_clinica = new historia_clinica()
            {
                id_historia = (Int32)ingresos.id_historia
               ,Cedula = (from DataRow A25rowhistoria_clinica in A25dthistoria_clinica.Rows
                      where ingresos.id_historia == (int)A25rowhistoria_clinica["id_historia"]
                      select (String)A25rowhistoria_clinica["Cedula"]).FirstOrDefault()
            };

            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // POST: /ingresos/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Num_habitacion
                                            )
        {

            ingresos ingresos = new ingresos();
            ingresos.Num_habitacion = System.Convert.ToInt32(Num_habitacion);
            ingresos = ingresosData.Select_Record(ingresos);

            bool bSucess = false;
            bSucess = ingresosData.Delete(ingresos);
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
            SelectListItem Item1 = new SelectListItem { Text = "Num Habitacion", Value = "Num Habitacion" };
            SelectListItem Item2 = new SelectListItem { Text = "Comentario", Value = "Comentario" };
            SelectListItem Item3 = new SelectListItem { Text = "Fecha Ingreso", Value = "Fecha Ingreso" };
            SelectListItem Item4 = new SelectListItem { Text = "Fecha Salida", Value = "Fecha Salida" };
            SelectListItem Item5 = new SelectListItem { Text = "Num Cama", Value = "Num Cama" };
            SelectListItem Item6 = new SelectListItem { Text = "Historia", Value = "Historia" };

                 if (select == "Num Habitacion") { Item1.Selected = true; }
            else if (select == "Comentario") { Item2.Selected = true; }
            else if (select == "Fecha Ingreso") { Item3.Selected = true; }
            else if (select == "Fecha Salida") { Item4.Selected = true; }
            else if (select == "Num Cama") { Item5.Selected = true; }
            else if (select == "Historia") { Item6.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo.Ingresos", "Many");
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
 
