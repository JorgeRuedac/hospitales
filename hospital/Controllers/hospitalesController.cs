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
    public class hospitalesController : Controller
    {

        DataTable dthospitales = new DataTable();
        DataTable dtmedico = new DataTable();

        // GET: /hospitales/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Cod Hospital" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Cod_hospitalSortParm"] = sortOrder == "Cod_hospital_asc" ? "Cod_hospital_desc" : "Cod_hospital_asc";
            ViewData["NombreSortParm"] = sortOrder == "Nombre_asc" ? "Nombre_desc" : "Nombre_asc";
            ViewData["CiudadSortParm"] = sortOrder == "Ciudad_asc" ? "Ciudad_desc" : "Ciudad_asc";
            ViewData["TlefonoSortParm"] = sortOrder == "Tlefono_asc" ? "Tlefono_desc" : "Tlefono_asc";
            ViewData["Cod_medicoSortParm"] = sortOrder == "Cod_medico_asc" ? "Cod_medico_desc" : "Cod_medico_asc";

            dthospitales = hospitalesData.SelectAll();
            dtmedico = hospitales_medicoData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dthospitales = hospitalesData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowhospitales in dthospitales.AsEnumerable()
                        join rowmedico in dtmedico.AsEnumerable() on rowhospitales.Field<Int32?>("Cod_medico") equals rowmedico.Field<Int32>("Cod_medico")
                        select new hospitales() {
                            Cod_hospital = rowhospitales.Field<Int32>("Cod_hospital")
                           ,Nombre = rowhospitales.Field<String>("Nombre")
                           ,Ciudad = rowhospitales.Field<String>("Ciudad")
                           ,Tlefono = rowhospitales.Field<String>("Tlefono")
                           ,
                            medico = new medico() 
                            {
                                   Cod_medico = rowmedico.Field<Int32>("Cod_medico")
                                  ,Cedula = rowmedico.Field<String>("Cedula")
                            }
                        };

            switch (sortOrder)
            {
                case "Cod_hospital_desc":
                    Query = Query.OrderByDescending(s => s.Cod_hospital);
                    break;
                case "Cod_hospital_asc":
                    Query = Query.OrderBy(s => s.Cod_hospital);
                    break;
                case "Nombre_desc":
                    Query = Query.OrderByDescending(s => s.Nombre);
                    break;
                case "Nombre_asc":
                    Query = Query.OrderBy(s => s.Nombre);
                    break;
                case "Ciudad_desc":
                    Query = Query.OrderByDescending(s => s.Ciudad);
                    break;
                case "Ciudad_asc":
                    Query = Query.OrderBy(s => s.Ciudad);
                    break;
                case "Tlefono_desc":
                    Query = Query.OrderByDescending(s => s.Tlefono);
                    break;
                case "Tlefono_asc":
                    Query = Query.OrderBy(s => s.Tlefono);
                    break;
                case "Cod_medico_desc":
                    Query = Query.OrderByDescending(s => s.medico.Cedula);
                    break;
                case "Cod_medico_asc":
                    Query = Query.OrderBy(s => s.medico.Cedula);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Cod_hospital);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cod Hospital", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Ciudad", typeof(string));
                dt.Columns.Add("Tlefono", typeof(string));
                dt.Columns.Add("Cod Medico", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Cod_hospital
                       ,item.Nombre
                       ,item.Ciudad
                       ,item.Tlefono
                       ,item.medico.Cedula
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

        // GET: /hospitales/Details/<id>
        public ActionResult Details(
                                      Int32? Cod_hospital
                                   )
        {
            if (
                    Cod_hospital == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtmedico = hospitales_medicoData.SelectAll();

            hospitales hospitales = new hospitales();
            hospitales.Cod_hospital = System.Convert.ToInt32(Cod_hospital);
            hospitales = hospitalesData.Select_Record(hospitales);
            hospitales.medico = new medico()
            {
                Cod_medico = (Int32)hospitales.Cod_medico
               ,Cedula = (from DataRow rowmedico in dtmedico.Rows
                      where hospitales.Cod_medico == (int)rowmedico["Cod_medico"]
                      select (String)rowmedico["Cedula"]).FirstOrDefault()
            };

            if (hospitales == null)
            {
                return HttpNotFound();
            }
            return View(hospitales);
        }

        // GET: /hospitales/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["Cod_medico"] = new SelectList(hospitales_medicoData.List(), "Cod_medico", "Cedula");

            return View();
        }

        // POST: /hospitales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Nombre"
				   + "," + "Ciudad"
				   + "," + "Tlefono"
				   + "," + "Cod_medico"
				  )] hospitales hospitales)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = hospitalesData.Add(hospitales);
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
            ViewData["Cod_medico"] = new SelectList(hospitales_medicoData.List(), "Cod_medico", "Cedula", hospitales.Cod_medico);

            return View(hospitales);
        }

        // GET: /hospitales/Edit/<id>
        public ActionResult Edit(
                                   Int32? Cod_hospital
                                )
        {
            if (
                    Cod_hospital == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            hospitales hospitales = new hospitales();
            hospitales.Cod_hospital = System.Convert.ToInt32(Cod_hospital);
            hospitales = hospitalesData.Select_Record(hospitales);

            if (hospitales == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["Cod_medico"] = new SelectList(hospitales_medicoData.List(), "Cod_medico", "Cedula", hospitales.Cod_medico);

            return View(hospitales);
        }

        // POST: /hospitales/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(hospitales hospitales)
        {

            hospitales ohospitales = new hospitales();
            ohospitales.Cod_hospital = System.Convert.ToInt32(hospitales.Cod_hospital);
            ohospitales = hospitalesData.Select_Record(hospitales);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = hospitalesData.Update(ohospitales, hospitales);
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
            ViewData["Cod_medico"] = new SelectList(hospitales_medicoData.List(), "Cod_medico", "Cedula", hospitales.Cod_medico);

            return View(hospitales);
        }

        // GET: /hospitales/Delete/<id>
        public ActionResult Delete(
                                     Int32? Cod_hospital
                                  )
        {
            if (
                    Cod_hospital == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtmedico = hospitales_medicoData.SelectAll();

            hospitales hospitales = new hospitales();
            hospitales.Cod_hospital = System.Convert.ToInt32(Cod_hospital);
            hospitales = hospitalesData.Select_Record(hospitales);
            hospitales.medico = new medico()
            {
                Cod_medico = (Int32)hospitales.Cod_medico
               ,Cedula = (from DataRow rowmedico in dtmedico.Rows
                      where hospitales.Cod_medico == (int)rowmedico["Cod_medico"]
                      select (String)rowmedico["Cedula"]).FirstOrDefault()
            };

            if (hospitales == null)
            {
                return HttpNotFound();
            }
            return View(hospitales);
        }

        // POST: /hospitales/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Cod_hospital
                                            )
        {

            hospitales hospitales = new hospitales();
            hospitales.Cod_hospital = System.Convert.ToInt32(Cod_hospital);
            hospitales = hospitalesData.Select_Record(hospitales);

            bool bSucess = false;
            bSucess = hospitalesData.Delete(hospitales);
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
            SelectListItem Item1 = new SelectListItem { Text = "Cod Hospital", Value = "Cod Hospital" };
            SelectListItem Item2 = new SelectListItem { Text = "Nombre", Value = "Nombre" };
            SelectListItem Item3 = new SelectListItem { Text = "Ciudad", Value = "Ciudad" };
            SelectListItem Item4 = new SelectListItem { Text = "Tlefono", Value = "Tlefono" };
            SelectListItem Item5 = new SelectListItem { Text = "Cod Medico", Value = "Cod Medico" };

                 if (select == "Cod Hospital") { Item1.Selected = true; }
            else if (select == "Nombre") { Item2.Selected = true; }
            else if (select == "Ciudad") { Item3.Selected = true; }
            else if (select == "Tlefono") { Item4.Selected = true; }
            else if (select == "Cod Medico") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Hospitales", "Many");
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
 
