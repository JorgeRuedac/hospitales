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
    public class camasController : Controller
    {

        DataTable dtcamas = new DataTable();
        DataTable dthospitales_servicios = new DataTable();

        // GET: /camas/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Id Cama" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["id_camaSortParm"] = sortOrder == "id_cama_asc" ? "id_cama_desc" : "id_cama_asc";
            ViewData["Num_camaSortParm"] = sortOrder == "Num_cama_asc" ? "Num_cama_desc" : "Num_cama_asc";
            ViewData["EstadoSortParm"] = sortOrder == "Estado_asc" ? "Estado_desc" : "Estado_asc";
            ViewData["ID_hospitales_serviciosSortParm"] = sortOrder == "ID_hospitales_servicios_asc" ? "ID_hospitales_servicios_desc" : "ID_hospitales_servicios_asc";

            dtcamas = camasData.SelectAll();
            dthospitales_servicios = camas_hospitales_serviciosData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtcamas = camasData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowcamas in dtcamas.AsEnumerable()
                        join rowhospitales_servicios in dthospitales_servicios.AsEnumerable() on rowcamas.Field<Int32?>("ID_hospitales_servicios") equals rowhospitales_servicios.Field<Int32>("ID_hospitales_servicios")
                        select new camas() {
                            id_cama = rowcamas.Field<Int32>("id_cama")
                           ,Num_cama = rowcamas.Field<Int32?>("Num_cama")
                           ,Estado = rowcamas.Field<String>("Estado")
                           ,
                            hospitales_servicios = new hospitales_servicios() 
                            {
                                   ID_hospitales_servicios = rowhospitales_servicios.Field<Int32>("ID_hospitales_servicios")
                                  ,CodigoRefer = rowhospitales_servicios.Field<String>("CodigoRefer")
                            }
                        };

            switch (sortOrder)
            {
                case "id_cama_desc":
                    Query = Query.OrderByDescending(s => s.id_cama);
                    break;
                case "id_cama_asc":
                    Query = Query.OrderBy(s => s.id_cama);
                    break;
                case "Num_cama_desc":
                    Query = Query.OrderByDescending(s => s.Num_cama);
                    break;
                case "Num_cama_asc":
                    Query = Query.OrderBy(s => s.Num_cama);
                    break;
                case "Estado_desc":
                    Query = Query.OrderByDescending(s => s.Estado);
                    break;
                case "Estado_asc":
                    Query = Query.OrderBy(s => s.Estado);
                    break;
                case "ID_hospitales_servicios_desc":
                    Query = Query.OrderByDescending(s => s.hospitales_servicios.CodigoRefer);
                    break;
                case "ID_hospitales_servicios_asc":
                    Query = Query.OrderBy(s => s.hospitales_servicios.CodigoRefer);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.id_cama);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id Cama", typeof(string));
                dt.Columns.Add("Num Cama", typeof(string));
                dt.Columns.Add("Estado", typeof(string));
                dt.Columns.Add("COD Hospital Servicio", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.id_cama
                       ,item.Num_cama
                       ,item.Estado
                       ,item.hospitales_servicios.CodigoRefer
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

        // GET: /camas/Details/<id>
        public ActionResult Details(
                                      Int32? id_cama
                                   )
        {
            if (
                    id_cama == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dthospitales_servicios = camas_hospitales_serviciosData.SelectAll();

            camas camas = new camas();
            camas.id_cama = System.Convert.ToInt32(id_cama);
            camas = camasData.Select_Record(camas);
            camas.hospitales_servicios = new hospitales_servicios()
            {
                ID_hospitales_servicios = (Int32)camas.ID_hospitales_servicios
               ,CodigoRefer = (from DataRow rowhospitales_servicios in dthospitales_servicios.Rows
                      where camas.ID_hospitales_servicios == (int)rowhospitales_servicios["ID_hospitales_servicios"]
                      select (String)rowhospitales_servicios["CodigoRefer"]).FirstOrDefault()
            };

            if (camas == null)
            {
                return HttpNotFound();
            }
            return View(camas);
        }

        // GET: /camas/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["ID_hospitales_servicios"] = new SelectList(camas_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer");

            return View();
        }

        // POST: /camas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Num_cama"
				   + "," + "Estado"
				   + "," + "ID_hospitales_servicios"
				  )] camas camas)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = camasData.Add(camas);
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
            ViewData["ID_hospitales_servicios"] = new SelectList(camas_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", camas.ID_hospitales_servicios);

            return View(camas);
        }

        // GET: /camas/Edit/<id>
        public ActionResult Edit(
                                   Int32? id_cama
                                )
        {
            if (
                    id_cama == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            camas camas = new camas();
            camas.id_cama = System.Convert.ToInt32(id_cama);
            camas = camasData.Select_Record(camas);

            if (camas == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["ID_hospitales_servicios"] = new SelectList(camas_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", camas.ID_hospitales_servicios);

            return View(camas);
        }

        // POST: /camas/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(camas camas)
        {

            camas ocamas = new camas();
            ocamas.id_cama = System.Convert.ToInt32(camas.id_cama);
            ocamas = camasData.Select_Record(camas);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = camasData.Update(ocamas, camas);
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
            ViewData["ID_hospitales_servicios"] = new SelectList(camas_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", camas.ID_hospitales_servicios);

            return View(camas);
        }

        // GET: /camas/Delete/<id>
        public ActionResult Delete(
                                     Int32? id_cama
                                  )
        {
            if (
                    id_cama == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dthospitales_servicios = camas_hospitales_serviciosData.SelectAll();

            camas camas = new camas();
            camas.id_cama = System.Convert.ToInt32(id_cama);
            camas = camasData.Select_Record(camas);
            camas.hospitales_servicios = new hospitales_servicios()
            {
                ID_hospitales_servicios = (Int32)camas.ID_hospitales_servicios
               ,CodigoRefer = (from DataRow rowhospitales_servicios in dthospitales_servicios.Rows
                      where camas.ID_hospitales_servicios == (int)rowhospitales_servicios["ID_hospitales_servicios"]
                      select (String)rowhospitales_servicios["CodigoRefer"]).FirstOrDefault()
            };

            if (camas == null)
            {
                return HttpNotFound();
            }
            return View(camas);
        }

        // POST: /camas/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? id_cama
                                            )
        {

            camas camas = new camas();
            camas.id_cama = System.Convert.ToInt32(id_cama);
            camas = camasData.Select_Record(camas);

            bool bSucess = false;
            bSucess = camasData.Delete(camas);
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
            SelectListItem Item1 = new SelectListItem { Text = "Id Cama", Value = "Id Cama" };
            SelectListItem Item2 = new SelectListItem { Text = "Num Cama", Value = "Num Cama" };
            SelectListItem Item3 = new SelectListItem { Text = "Estado", Value = "Estado" };
            SelectListItem Item4 = new SelectListItem { Text = "COD Hospital Servicio", Value = "COD Hospital Servicio" };

                 if (select == "Id Cama") { Item1.Selected = true; }
            else if (select == "Num Cama") { Item2.Selected = true; }
            else if (select == "Estado") { Item3.Selected = true; }
            else if (select == "COD Hospital Servicio") { Item4.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo.Camas", "Many");
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
 
