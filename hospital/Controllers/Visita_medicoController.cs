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
    public class Visita_medicoController : Controller
    {

        DataTable dtVisita_medico = new DataTable();
        DataTable A37dthospitales_servicios = new DataTable();
        DataTable A38dthistoria_clinica = new DataTable();

        // GET: /Visita_medico/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Cod Visita" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["Cod_visitaSortParm"] = sortOrder == "Cod_visita_asc" ? "Cod_visita_desc" : "Cod_visita_asc";
            ViewData["FechaSortParm"] = sortOrder == "Fecha_asc" ? "Fecha_desc" : "Fecha_asc";
            ViewData["HoraSortParm"] = sortOrder == "Hora_asc" ? "Hora_desc" : "Hora_asc";
            ViewData["DiagnosticoSortParm"] = sortOrder == "Diagnostico_asc" ? "Diagnostico_desc" : "Diagnostico_asc";
            ViewData["TratamientoSortParm"] = sortOrder == "Tratamiento_asc" ? "Tratamiento_desc" : "Tratamiento_asc";
            ViewData["ID_hospitales_serviciosSortParm"] = sortOrder == "ID_hospitales_servicios_asc" ? "ID_hospitales_servicios_desc" : "ID_hospitales_servicios_asc";
            ViewData["id_historiaSortParm"] = sortOrder == "id_historia_asc" ? "id_historia_desc" : "id_historia_asc";

            dtVisita_medico = Visita_medicoData.SelectAll();
            A37dthospitales_servicios = Visita_medico_hospitales_serviciosData.SelectAll();
            A38dthistoria_clinica = Visita_medico_historia_clinicaData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtVisita_medico = Visita_medicoData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowVisita_medico in dtVisita_medico.AsEnumerable()
                        join A37rowhospitales_servicios in A37dthospitales_servicios.AsEnumerable() on rowVisita_medico.Field<Int32?>("ID_hospitales_servicios") equals A37rowhospitales_servicios.Field<Int32>("ID_hospitales_servicios")
                        join A38rowhistoria_clinica in A38dthistoria_clinica.AsEnumerable() on rowVisita_medico.Field<Int32?>("id_historia") equals A38rowhistoria_clinica.Field<Int32>("id_historia")
                        select new Visita_medico() {
                            Cod_visita = rowVisita_medico.Field<Int32>("Cod_visita")
                           ,Fecha = rowVisita_medico.Field<DateTime?>("Fecha")
                           ,Hora = rowVisita_medico.Field<String>("Hora")
                           ,Diagnostico = rowVisita_medico.Field<String>("Diagnostico")
                           ,Tratamiento = rowVisita_medico.Field<String>("Tratamiento")
                           ,
                            hospitales_servicios = new hospitales_servicios() 
                            {
                                   ID_hospitales_servicios = A37rowhospitales_servicios.Field<Int32>("ID_hospitales_servicios")
                                  ,CodigoRefer = A37rowhospitales_servicios.Field<String>("CodigoRefer")
                            }
                           ,
                            historia_clinica = new historia_clinica() 
                            {
                                   id_historia = A38rowhistoria_clinica.Field<Int32>("id_historia")
                                  ,Cedula = A38rowhistoria_clinica.Field<String>("Cedula")
                            }
                        };

            switch (sortOrder)
            {
                case "Cod_visita_desc":
                    Query = Query.OrderByDescending(s => s.Cod_visita);
                    break;
                case "Cod_visita_asc":
                    Query = Query.OrderBy(s => s.Cod_visita);
                    break;
                case "Fecha_desc":
                    Query = Query.OrderByDescending(s => s.Fecha);
                    break;
                case "Fecha_asc":
                    Query = Query.OrderBy(s => s.Fecha);
                    break;
                case "Hora_desc":
                    Query = Query.OrderByDescending(s => s.Hora);
                    break;
                case "Hora_asc":
                    Query = Query.OrderBy(s => s.Hora);
                    break;
                case "Diagnostico_desc":
                    Query = Query.OrderByDescending(s => s.Diagnostico);
                    break;
                case "Diagnostico_asc":
                    Query = Query.OrderBy(s => s.Diagnostico);
                    break;
                case "Tratamiento_desc":
                    Query = Query.OrderByDescending(s => s.Tratamiento);
                    break;
                case "Tratamiento_asc":
                    Query = Query.OrderBy(s => s.Tratamiento);
                    break;
                case "ID_hospitales_servicios_desc":
                    Query = Query.OrderByDescending(s => s.hospitales_servicios.CodigoRefer);
                    break;
                case "ID_hospitales_servicios_asc":
                    Query = Query.OrderBy(s => s.hospitales_servicios.CodigoRefer);
                    break;
                case "id_historia_desc":
                    Query = Query.OrderByDescending(s => s.historia_clinica.Cedula);
                    break;
                case "id_historia_asc":
                    Query = Query.OrderBy(s => s.historia_clinica.Cedula);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Cod_visita);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cod Visita", typeof(string));
                dt.Columns.Add("Fecha", typeof(string));
                dt.Columns.Add("Hora", typeof(string));
                dt.Columns.Add("Diagnostico", typeof(string));
                dt.Columns.Add("Tratamiento", typeof(string));
                dt.Columns.Add("COD Hospital Servicio", typeof(string));
                dt.Columns.Add("Historia", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Cod_visita
                       ,item.Fecha
                       ,item.Hora
                       ,item.Diagnostico
                       ,item.Tratamiento
                       ,item.hospitales_servicios.CodigoRefer
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

        // GET: /Visita_medico/Details/<id>
        public ActionResult Details(
                                      Int32? Cod_visita
                                   )
        {
            if (
                    Cod_visita == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A37dthospitales_servicios = Visita_medico_hospitales_serviciosData.SelectAll();
            A38dthistoria_clinica = Visita_medico_historia_clinicaData.SelectAll();

            Visita_medico Visita_medico = new Visita_medico();
            Visita_medico.Cod_visita = System.Convert.ToInt32(Cod_visita);
            Visita_medico = Visita_medicoData.Select_Record(Visita_medico);
            Visita_medico.hospitales_servicios = new hospitales_servicios()
            {
                ID_hospitales_servicios = (Int32)Visita_medico.ID_hospitales_servicios
               ,CodigoRefer = (from DataRow A37rowhospitales_servicios in A37dthospitales_servicios.Rows
                      where Visita_medico.ID_hospitales_servicios == (int)A37rowhospitales_servicios["ID_hospitales_servicios"]
                      select (String)A37rowhospitales_servicios["CodigoRefer"]).FirstOrDefault()
            };
            Visita_medico.historia_clinica = new historia_clinica()
            {
                id_historia = (Int32)Visita_medico.id_historia
               ,Cedula = (from DataRow A38rowhistoria_clinica in A38dthistoria_clinica.Rows
                      where Visita_medico.id_historia == (int)A38rowhistoria_clinica["id_historia"]
                      select (String)A38rowhistoria_clinica["Cedula"]).FirstOrDefault()
            };

            if (Visita_medico == null)
            {
                return HttpNotFound();
            }
            return View(Visita_medico);
        }

        // GET: /Visita_medico/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["ID_hospitales_servicios"] = new SelectList(Visita_medico_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer");
            ViewData["id_historia"] = new SelectList(Visita_medico_historia_clinicaData.List(), "id_historia", "Cedula");

            return View();
        }

        // POST: /Visita_medico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "Fecha"
				   + "," + "Hora"
				   + "," + "Diagnostico"
				   + "," + "Tratamiento"
				   + "," + "ID_hospitales_servicios"
				   + "," + "id_historia"
				  )] Visita_medico Visita_medico)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = Visita_medicoData.Add(Visita_medico);
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
            ViewData["ID_hospitales_servicios"] = new SelectList(Visita_medico_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", Visita_medico.ID_hospitales_servicios);
            ViewData["id_historia"] = new SelectList(Visita_medico_historia_clinicaData.List(), "id_historia", "Cedula", Visita_medico.id_historia);

            return View(Visita_medico);
        }

        // GET: /Visita_medico/Edit/<id>
        public ActionResult Edit(
                                   Int32? Cod_visita
                                )
        {
            if (
                    Cod_visita == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Visita_medico Visita_medico = new Visita_medico();
            Visita_medico.Cod_visita = System.Convert.ToInt32(Cod_visita);
            Visita_medico = Visita_medicoData.Select_Record(Visita_medico);

            if (Visita_medico == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["ID_hospitales_servicios"] = new SelectList(Visita_medico_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", Visita_medico.ID_hospitales_servicios);
            ViewData["id_historia"] = new SelectList(Visita_medico_historia_clinicaData.List(), "id_historia", "Cedula", Visita_medico.id_historia);

            return View(Visita_medico);
        }

        // POST: /Visita_medico/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Visita_medico Visita_medico)
        {

            Visita_medico oVisita_medico = new Visita_medico();
            oVisita_medico.Cod_visita = System.Convert.ToInt32(Visita_medico.Cod_visita);
            oVisita_medico = Visita_medicoData.Select_Record(Visita_medico);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = Visita_medicoData.Update(oVisita_medico, Visita_medico);
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
            ViewData["ID_hospitales_servicios"] = new SelectList(Visita_medico_hospitales_serviciosData.List(), "ID_hospitales_servicios", "CodigoRefer", Visita_medico.ID_hospitales_servicios);
            ViewData["id_historia"] = new SelectList(Visita_medico_historia_clinicaData.List(), "id_historia", "Cedula", Visita_medico.id_historia);

            return View(Visita_medico);
        }

        // GET: /Visita_medico/Delete/<id>
        public ActionResult Delete(
                                     Int32? Cod_visita
                                  )
        {
            if (
                    Cod_visita == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            A37dthospitales_servicios = Visita_medico_hospitales_serviciosData.SelectAll();
            A38dthistoria_clinica = Visita_medico_historia_clinicaData.SelectAll();

            Visita_medico Visita_medico = new Visita_medico();
            Visita_medico.Cod_visita = System.Convert.ToInt32(Cod_visita);
            Visita_medico = Visita_medicoData.Select_Record(Visita_medico);
            Visita_medico.hospitales_servicios = new hospitales_servicios()
            {
                ID_hospitales_servicios = (Int32)Visita_medico.ID_hospitales_servicios
               ,CodigoRefer = (from DataRow A37rowhospitales_servicios in A37dthospitales_servicios.Rows
                      where Visita_medico.ID_hospitales_servicios == (int)A37rowhospitales_servicios["ID_hospitales_servicios"]
                      select (String)A37rowhospitales_servicios["CodigoRefer"]).FirstOrDefault()
            };
            Visita_medico.historia_clinica = new historia_clinica()
            {
                id_historia = (Int32)Visita_medico.id_historia
               ,Cedula = (from DataRow A38rowhistoria_clinica in A38dthistoria_clinica.Rows
                      where Visita_medico.id_historia == (int)A38rowhistoria_clinica["id_historia"]
                      select (String)A38rowhistoria_clinica["Cedula"]).FirstOrDefault()
            };

            if (Visita_medico == null)
            {
                return HttpNotFound();
            }
            return View(Visita_medico);
        }

        // POST: /Visita_medico/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? Cod_visita
                                            )
        {

            Visita_medico Visita_medico = new Visita_medico();
            Visita_medico.Cod_visita = System.Convert.ToInt32(Cod_visita);
            Visita_medico = Visita_medicoData.Select_Record(Visita_medico);

            bool bSucess = false;
            bSucess = Visita_medicoData.Delete(Visita_medico);
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
            SelectListItem Item1 = new SelectListItem { Text = "Cod Visita", Value = "Cod Visita" };
            SelectListItem Item2 = new SelectListItem { Text = "Fecha", Value = "Fecha" };
            SelectListItem Item3 = new SelectListItem { Text = "Hora", Value = "Hora" };
            SelectListItem Item4 = new SelectListItem { Text = "Diagnostico", Value = "Diagnostico" };
            SelectListItem Item5 = new SelectListItem { Text = "Tratamiento", Value = "Tratamiento" };
            SelectListItem Item6 = new SelectListItem { Text = "COD Hospital Servicio", Value = "COD Hospital Servicio" };
            SelectListItem Item7 = new SelectListItem { Text = "Historia", Value = "Historia" };

                 if (select == "Cod Visita") { Item1.Selected = true; }
            else if (select == "Fecha") { Item2.Selected = true; }
            else if (select == "Hora") { Item3.Selected = true; }
            else if (select == "Diagnostico") { Item4.Selected = true; }
            else if (select == "Tratamiento") { Item5.Selected = true; }
            else if (select == "COD Hospital Servicio") { Item6.Selected = true; }
            else if (select == "Historia") { Item7.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Visita Medico", "Many");
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
 
