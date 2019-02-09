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
    public class hospitales_serviciosController : Controller
    {

        DataTable dthospitales_servicios = new DataTable();
        DataTable A17dthospitales = new DataTable();
        DataTable A18dtservicios = new DataTable();

        // GET: /hospitales_servicios/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "I D Hospitales Servicios" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ID_hospitales_serviciosSortParm"] = sortOrder == "ID_hospitales_servicios_asc" ? "ID_hospitales_servicios_desc" : "ID_hospitales_servicios_asc";
            ViewData["Cod_hospitalSortParm"] = sortOrder == "Cod_hospital_asc" ? "Cod_hospital_desc" : "Cod_hospital_asc";
            ViewData["Id_servicioSortParm"] = sortOrder == "Id_servicio_asc" ? "Id_servicio_desc" : "Id_servicio_asc";
            ViewData["CodigoReferSortParm"] = sortOrder == "CodigoRefer_asc" ? "CodigoRefer_desc" : "CodigoRefer_asc";

            dthospitales_servicios = hospitales_serviciosData.SelectAll();
            A17dthospitales = hospitales_servicios_hospitalesData.SelectAll();
            A18dtservicios = hospitales_servicios_serviciosData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dthospitales_servicios = hospitales_serviciosData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowhospitales_servicios in dthospitales_servicios.AsEnumerable()
                        join A17rowhospitales in A17dthospitales.AsEnumerable() on rowhospitales_servicios.Field<Int32?>("Cod_hospital") equals A17rowhospitales.Field<Int32>("Cod_hospital")
                        join A18rowservicios in A18dtservicios.AsEnumerable() on rowhospitales_servicios.Field<Int32?>("Id_servicio") equals A18rowservicios.Field<Int32>("Id_servicio")
                        select new hospitales_servicios() {
                            ID_hospitales_servicios = rowhospitales_servicios.Field<Int32>("ID_hospitales_servicios")
                           ,
                            hospitales = new hospitales() 
                            {
                                   Cod_hospital = A17rowhospitales.Field<Int32>("Cod_hospital")
                                  ,Nombre = A17rowhospitales.Field<String>("Nombre")
                            }
                           ,
                            servicios = new servicios() 
                            {
                                   Id_servicio = A18rowservicios.Field<Int32>("Id_servicio")
                                  ,Nombre_servicio = A18rowservicios.Field<String>("Nombre_servicio")
                            }
                           ,CodigoRefer = rowhospitales_servicios.Field<String>("CodigoRefer")
                        };

            switch (sortOrder)
            {
                case "ID_hospitales_servicios_desc":
                    Query = Query.OrderByDescending(s => s.ID_hospitales_servicios);
                    break;
                case "ID_hospitales_servicios_asc":
                    Query = Query.OrderBy(s => s.ID_hospitales_servicios);
                    break;
                case "Cod_hospital_desc":
                    Query = Query.OrderByDescending(s => s.hospitales.Nombre);
                    break;
                case "Cod_hospital_asc":
                    Query = Query.OrderBy(s => s.hospitales.Nombre);
                    break;
                case "Id_servicio_desc":
                    Query = Query.OrderByDescending(s => s.servicios.Nombre_servicio);
                    break;
                case "Id_servicio_asc":
                    Query = Query.OrderBy(s => s.servicios.Nombre_servicio);
                    break;
                case "CodigoRefer_desc":
                    Query = Query.OrderByDescending(s => s.CodigoRefer);
                    break;
                case "CodigoRefer_asc":
                    Query = Query.OrderBy(s => s.CodigoRefer);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ID_hospitales_servicios);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("I D Hospitales Servicios", typeof(string));
                dt.Columns.Add("Cod Hospital", typeof(string));
                dt.Columns.Add("Id Servicio", typeof(string));
                dt.Columns.Add("Codigo Refer", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ID_hospitales_servicios
                       ,item.hospitales.Nombre
                       ,item.servicios.Nombre_servicio
                       ,item.CodigoRefer
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

        // GET: /hospitales_servicios/Details/<id>
        public ActionResult Details(
                                      Int32? ID_hospitales_servicios
                                   )
        {
            if (
                    ID_hospitales_servicios == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A17dthospitales = hospitales_servicios_hospitalesData.SelectAll();
            A18dtservicios = hospitales_servicios_serviciosData.SelectAll();

            hospitales_servicios hospitales_servicios = new hospitales_servicios();
            hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(ID_hospitales_servicios);
            hospitales_servicios = hospitales_serviciosData.Select_Record(hospitales_servicios);
            hospitales_servicios.hospitales = new hospitales()
            {
                Cod_hospital = (Int32)hospitales_servicios.Cod_hospital
               ,Nombre = (from DataRow A17rowhospitales in A17dthospitales.Rows
                      where hospitales_servicios.Cod_hospital == (int)A17rowhospitales["Cod_hospital"]
                      select (String)A17rowhospitales["Nombre"]).FirstOrDefault()
            };
            hospitales_servicios.servicios = new servicios()
            {
                Id_servicio = (Int32)hospitales_servicios.Id_servicio
               ,Nombre_servicio = (from DataRow A18rowservicios in A18dtservicios.Rows
                      where hospitales_servicios.Id_servicio == (int)A18rowservicios["Id_servicio"]
                      select (String)A18rowservicios["Nombre_servicio"]).FirstOrDefault()
            };

            if (hospitales_servicios == null)
            {
                return HttpNotFound();
            }
            return View(hospitales_servicios);
        }

        // GET: /hospitales_servicios/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["Cod_hospital"] = new SelectList(hospitales_servicios_hospitalesData.List(), "Cod_hospital", "Nombre");
            ViewData["Id_servicio"] = new SelectList(hospitales_servicios_serviciosData.List(), "Id_servicio", "Nombre_servicio");

            return View();
        }

        // POST: /hospitales_servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Cod_hospital"
				   + "," + "Id_servicio"
				   + "," + "CodigoRefer"
				  )] hospitales_servicios hospitales_servicios)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = hospitales_serviciosData.Add(hospitales_servicios);
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
            ViewData["Cod_hospital"] = new SelectList(hospitales_servicios_hospitalesData.List(), "Cod_hospital", "Nombre", hospitales_servicios.Cod_hospital);
            ViewData["Id_servicio"] = new SelectList(hospitales_servicios_serviciosData.List(), "Id_servicio", "Nombre_servicio", hospitales_servicios.Id_servicio);

            return View(hospitales_servicios);
        }

        // GET: /hospitales_servicios/Edit/<id>
        public ActionResult Edit(
                                   Int32? ID_hospitales_servicios
                                )
        {
            if (
                    ID_hospitales_servicios == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            hospitales_servicios hospitales_servicios = new hospitales_servicios();
            hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(ID_hospitales_servicios);
            hospitales_servicios = hospitales_serviciosData.Select_Record(hospitales_servicios);

            if (hospitales_servicios == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["Cod_hospital"] = new SelectList(hospitales_servicios_hospitalesData.List(), "Cod_hospital", "Nombre", hospitales_servicios.Cod_hospital);
            ViewData["Id_servicio"] = new SelectList(hospitales_servicios_serviciosData.List(), "Id_servicio", "Nombre_servicio", hospitales_servicios.Id_servicio);

            return View(hospitales_servicios);
        }

        // POST: /hospitales_servicios/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(hospitales_servicios hospitales_servicios)
        {

            hospitales_servicios ohospitales_servicios = new hospitales_servicios();
            ohospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(hospitales_servicios.ID_hospitales_servicios);
            ohospitales_servicios = hospitales_serviciosData.Select_Record(hospitales_servicios);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = hospitales_serviciosData.Update(ohospitales_servicios, hospitales_servicios);
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
            ViewData["Cod_hospital"] = new SelectList(hospitales_servicios_hospitalesData.List(), "Cod_hospital", "Nombre", hospitales_servicios.Cod_hospital);
            ViewData["Id_servicio"] = new SelectList(hospitales_servicios_serviciosData.List(), "Id_servicio", "Nombre_servicio", hospitales_servicios.Id_servicio);

            return View(hospitales_servicios);
        }

        // GET: /hospitales_servicios/Delete/<id>
        public ActionResult Delete(
                                     Int32? ID_hospitales_servicios
                                  )
        {
            if (
                    ID_hospitales_servicios == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A17dthospitales = hospitales_servicios_hospitalesData.SelectAll();
            A18dtservicios = hospitales_servicios_serviciosData.SelectAll();

            hospitales_servicios hospitales_servicios = new hospitales_servicios();
            hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(ID_hospitales_servicios);
            hospitales_servicios = hospitales_serviciosData.Select_Record(hospitales_servicios);
            hospitales_servicios.hospitales = new hospitales()
            {
                Cod_hospital = (Int32)hospitales_servicios.Cod_hospital
               ,Nombre = (from DataRow A17rowhospitales in A17dthospitales.Rows
                      where hospitales_servicios.Cod_hospital == (int)A17rowhospitales["Cod_hospital"]
                      select (String)A17rowhospitales["Nombre"]).FirstOrDefault()
            };
            hospitales_servicios.servicios = new servicios()
            {
                Id_servicio = (Int32)hospitales_servicios.Id_servicio
               ,Nombre_servicio = (from DataRow A18rowservicios in A18dtservicios.Rows
                      where hospitales_servicios.Id_servicio == (int)A18rowservicios["Id_servicio"]
                      select (String)A18rowservicios["Nombre_servicio"]).FirstOrDefault()
            };

            if (hospitales_servicios == null)
            {
                return HttpNotFound();
            }
            return View(hospitales_servicios);
        }

        // POST: /hospitales_servicios/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ID_hospitales_servicios
                                            )
        {

            hospitales_servicios hospitales_servicios = new hospitales_servicios();
            hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(ID_hospitales_servicios);
            hospitales_servicios = hospitales_serviciosData.Select_Record(hospitales_servicios);

            bool bSucess = false;
            bSucess = hospitales_serviciosData.Delete(hospitales_servicios);
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
            SelectListItem Item1 = new SelectListItem { Text = "I D Hospitales Servicios", Value = "I D Hospitales Servicios" };
            SelectListItem Item2 = new SelectListItem { Text = "Cod Hospital", Value = "Cod Hospital" };
            SelectListItem Item3 = new SelectListItem { Text = "Id Servicio", Value = "Id Servicio" };
            SelectListItem Item4 = new SelectListItem { Text = "Codigo Refer", Value = "Codigo Refer" };

                 if (select == "I D Hospitales Servicios") { Item1.Selected = true; }
            else if (select == "Cod Hospital") { Item2.Selected = true; }
            else if (select == "Id Servicio") { Item3.Selected = true; }
            else if (select == "Codigo Refer") { Item4.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo.Hospitales Servicios", "Many");
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
 
