using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeleccionAleatoria
{
    public partial class SeleccionAleatoria : System.Web.UI.Page
    {
        string filenameInput;
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Write($"Bienvenido/a {HttpContext.Current.User.Identity.Name}");



        }

        protected void Button1_Click(object sender, EventArgs e)//btnProcesar
        {   //validation
            if (!FileUpload1.HasFile)
            {
               
            }
            else
            {//proceso
                if (Page.IsValid)
                {
               // Thread.Sleep(3000);
                List<Empleador> listaEmpleadoR = new List<Empleador>();

                
                Dictionary<Empleador, int> dicci = new Dictionary<Empleador, int>();

                filenameInput = FileUpload1.FileName;

                FileUpload1.SaveAs(Server.MapPath($"~/FilesInput/{filenameInput}"));

                FileStream fs = new FileStream(Server.MapPath($"~/FilesInput/{filenameInput}"), FileMode.Open, FileAccess.Read);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage exp = new ExcelPackage(fs))
                {
                    var ws = exp.Workbook.Worksheets[0];

                    for (int fila = 2; ; fila++)
                    {
                        Registro r = new Registro(ws, fila);

                        var auxEmpleadorEnlista = listaEmpleadoR.Find(x => x.CUITEmpleador == r.Empleador.CUITEmpleador);

                        if (auxEmpleadorEnlista != null)//lo encontro
                        {

                            auxEmpleadorEnlista.ListaEmpleados.Add(r.EmpleadoCuit);

                        }
                        else
                        {
                            listaEmpleadoR.Add(r.Empleador);
                        }

                        if (r.EsRegistroVacio)
                        {
                            break;//corte
                        }

                      
                    }

                }

                fs.Close();

                    int cantempleados = Convert.ToInt32(txtCantEmp.Text);

                string fileNameOutput = $"Salida_{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

                StreamWriter sw = new StreamWriter(Server.MapPath($"~/FilesOutput/{fileNameOutput}"), false, Encoding.UTF8);

                foreach (var empleadorcuit1 in listaEmpleadoR)
                {                      

                        foreach (var empleadocuit2 in empleadorcuit1.ListaEmpleados.Take(cantempleados))
                        {
                            sw.WriteLine(empleadocuit2);
                        }                  

                }
                sw.Close();
                sw.Dispose();

                   

                    Response.Clear();

                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", fileNameOutput));
                Response.ContentType = "text/plain";

                Response.WriteFile(Server.MapPath($"~/FilesOutput/{fileNameOutput}"));

                    btnProcesar.Enabled = true;
                    btnProcesar.Text = "Procesar";

                   

                    Response.End();

                    

                }
            }
        }

        public class Empleador
        {
            List<string> listaEmpleadosCuit = new List<string>();

            public string CUITEmpleador { get; set; }
            public List<string> ListaEmpleados
            {

                get { return listaEmpleadosCuit; }
                set
                {
                    listaEmpleadosCuit = value;

                }
            }
        }


        internal class Registro : ExcelRegistroBase
        {

            Empleador empleador = new Empleador();

            string empleadoCuit;
           
            public Empleador Empleador
            {
                get { return empleador; }
                set { empleador = value; }
            }

          

            public string EmpleadoCuit
            {
                get { return empleadoCuit; }
               
            }




            public Registro(ExcelWorksheet ws, int fila):base(ws,fila)
            {
                empleador.CUITEmpleador = ObtenerTexto(1, "CuitEmpleador", ws);

                empleador.ListaEmpleados.Add(ObtenerTexto(2, "CuitEmpleado", ws));

                empleadoCuit = ObtenerTexto(2, "CuitEmpleado", ws);

               
            }


            public override bool EsRegistroVacio
            {
                get { return string.IsNullOrEmpty(empleador.CUITEmpleador); }
            }

            protected string ObtenerTexto(int col, string descrip, ExcelWorksheet ws)
            {
                ExcelRange celda = ws.Cells[Fila, col];

                var value = celda.Value;

                if (value != null)
                {
                    return value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int numberaux;
            string txt = args.Value;
            if (int.TryParse(txt,out numberaux)){
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

      
    }
}
