using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DBKnow
{
    public partial class GrillaFinal : Form
    {
        public GrillaFinal()
        {
            InitializeComponent();
        }

        private void GrillaFinal_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'knowledgeDBDataSet.SP_Incidentes' Puede moverla o quitarla según sea necesario.
            try { 
            this.sP_IncidentesTableAdapter.Fill(this.knowledgeDBDataSet.SP_Incidentes);
            }
            catch (Exception ex)
            {
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            //ValidaGrilla();

        }

        private void ValidaGrilla()
        {
            string select = String.Empty;
            select += " TOP(1000) ";
            select += " I.PkIncidente Incidente ";
            select += " ,I.Titulo Titulo";
            select += " ,SUBSTRING(I.Descripcion, 0, 100) as Descripcion ";
            //select += " ,U.Usuario ";
            //select += " ,E.Estado ";
            //select += " ,C.Categoria ";
            //select += " ,P.Prioridad ";
            //select += " ,T.NombreCompleto ";
            //select += " ,Tipo.Tipo ";
            //select += " ,S.NombreSupervisor ";
            //select += " ,I.FechaInicio ";
            //select += " ,I.FechaFin ";
            //select += " ,SUBSTRING(I.Solucion, 0, 100) as Solucion ";
            string from = string.Empty;
            from += " Incidentes I ";
            from += " inner join Usuario U on U.PkUsuario = I.FkUsuario and U.Activo <> 0 ";
            from += " inner join Estado E on E.PkEstado = I.FkEstado and E.Activo <> 0 ";
            from += " inner join Categoria C on C.PkCategoria = I.FkCategoria and C.Activo <> 0 ";
            from += " inner join Prioridad P on P.Nivel = I.FkPrioridad and P.Activo <> 0 ";
            from += " inner join Tecnico T on T.PkTecnico = I.FkTecnico and T.Activo <> 0 ";
            from += " inner join Tipo on Tipo.PkTipo = I.FkTipo and Tipo.Activo <> 0 ";
            from += " inner join Supervisor S on S.PkSupervisor = I.FkSupervisor and S.Activo <> 0 ";
            string where = string.Empty;
            where = " I.Activo <> 0 ";
            string order_by = " I.FechaInicio asc, P.Nivel asc ";
            DataTable Valores = ClaseVariable.LlenaDT_Grilla(select, from, where, order_by);
            //dataGridView.DataSource = Valores;
            

            var bindingSource = new System.Windows.Forms.BindingSource();
            bindingSource.DataSource = Valores.DataSet;
            dataGridView.DataSource = bindingSource;
            dataGridView.Show();
            this.Refresh();
            
            //excel: http://csharp.net-informations.com/datagridview/csharp-datagridview-export-excel.htm

        }

        private void crearIncidenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Formulario formulario = new Formulario();
            formulario.Usuario = ClaseVariable.Usuario;
            formulario.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            this.Dispose();
        }

        private void masInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            Int16 i, j;

            //xlApp = new Excel.ApplicationClass();
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            for (i = 0; i <= dataGridView.RowCount -1; i++)//-2
            {
                for (j = 0; j <= dataGridView.ColumnCount -1; j++) //-1
                {
                    xlWorkSheet.Cells[i + 1, j + 1] = dataGridView[j, i].Value.ToString();
                }
            }

            xlWorkBook.SaveAs(@"D:\Sena\ejercicio.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.Show();
        }

        private void informaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
