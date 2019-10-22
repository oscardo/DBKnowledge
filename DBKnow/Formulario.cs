using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBKnow
{
    public partial class Formulario : Form
    {
        public string Usuario { get; set; }
        public Formulario()
        {
            InitializeComponent();
        }

        private void Formulario_Load(object sender, EventArgs e)
        {
            this.txtUsuario.Text = ClaseVariable.Usuario; 
            LlenarCombos();
        }

        private void LlenarCombos()
        {
            try{
            DataSet tipos = ClaseVariable.LlenaDT("Tipo, PkTipo", "Tipo", "Activo <> 0", "Tipo ASC");
            cbTipo.DisplayMember = tipos.Tables[0].Columns[0].ToString();
            cbTipo.ValueMember = tipos.Tables[0].Columns[1].ToString();
            cbTipo.DataSource = tipos.Tables[0];

            DataSet Tecnico = ClaseVariable.LlenaDT("NombreCompleto, PkTecnico", "Tecnico", "Activo <> 0", "NombreCompleto ASC");
            cbTecnico.DisplayMember = Tecnico.Tables[0].Columns[0].ToString();
            cbTecnico.ValueMember = Tecnico.Tables[0].Columns[1].ToString();
            cbTecnico.DataSource = Tecnico.Tables[0];

            DataSet Supervisor = ClaseVariable.LlenaDT("NombreSupervisor, PkSupervisor", "Supervisor", "Activo <> 0", "NombreSupervisor ASC");
            cbSupervisor.DisplayMember = Supervisor.Tables[0].Columns[0].ToString();
            cbSupervisor.ValueMember = Supervisor.Tables[0].Columns[1].ToString();
            cbSupervisor.DataSource = Supervisor.Tables[0];

            DataSet Prioridad = ClaseVariable.LlenaDT("Prioridad, Nivel", "Prioridad", "Activo <> 0", "Nivel Desc");
            cbPrioridad.DisplayMember = Prioridad.Tables[0].Columns[0].ToString();
            cbPrioridad.ValueMember = Prioridad.Tables[0].Columns[1].ToString();
            cbPrioridad.DataSource = Prioridad.Tables[0];

            DataSet Estado = ClaseVariable.LlenaDT("Estado, PkEstado", "Estado", "Activo <> 0", "PkEstado asc");
            cbEstado.DisplayMember = Estado.Tables[0].Columns[0].ToString();
            cbEstado.ValueMember = Estado.Tables[0].Columns[1].ToString();
            cbEstado.DataSource = Estado.Tables[0];

            DataSet Categoria = ClaseVariable.LlenaDT("Categoria, PkCategoria", "Categoria", "Activo <> 0", "PkCategoria asc");
            cbCategoria.DisplayMember = Categoria.Tables[0].Columns[0].ToString();
            cbCategoria.ValueMember = Categoria.Tables[0].Columns[1].ToString();
            cbCategoria.DataSource = Categoria.Tables[0];

            DataSet Incidente = ClaseVariable.LlenaDT("top 1 PkIncidente + 1 as Incidente", "Incidentes", "Activo <> 0", "PkIncidente desc");
            int inc = 0;
            inc = int.Parse(Incidente.Tables[0].Rows[0][0].ToString());
            this.txtIncidente.Text = inc.ToString();
            }
            catch (Exception ex)
            {
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            bool entra = true;
            if (this.txtSolucion.Text.Equals("") || this.txtDescripcion.Text.Equals("")) {
                entra = false;
            }
            
            if (entra) { 
            int tipos = 1;
            int tecnico = 1;
            int supervisor = 1;
            int prioridad = 1;
            int estado = 1;
            int categoria = 1;
            int indicente = 1;
            string titulo = string.Empty;
            string descripcion = string.Empty;
            string fechaInicio, fechaFin;
            string solucion = string.Empty;
            bool activo = true;

            tipos = int.Parse(this.cbTipo.SelectedValue.ToString());
            tecnico = int.Parse(this.cbTecnico.SelectedValue.ToString());
            supervisor = int.Parse(this.cbSupervisor.SelectedValue.ToString());
            prioridad = int.Parse(this.cbPrioridad.SelectedValue.ToString());
            estado = int.Parse(this.cbEstado.SelectedValue.ToString());
            categoria = int.Parse(this.cbCategoria.SelectedValue.ToString());
            indicente = int.Parse(this.txtIncidente.Text.ToString());
            titulo = this.txtTitulo.Text.ToString();
            descripcion = this.txtDescripcion.Text.ToString();
            fechaInicio = this.dtFechaInicio.Value.ToString("yyyy-MM-dd");
            fechaFin = this.dtFechaFin.Value.ToString("yyyy-MM-dd");
            solucion = this.txtSolucion.Text.ToString();
            activo = true;

            ClaseVariable.Insertar_Incidente(indicente, 1, estado, categoria, prioridad, tecnico, tipos, supervisor, fechaInicio, fechaFin, titulo, descripcion, solucion, activo);
                    MessageBox.Show("La información esta correctamente agregada!", "Correcto");
                }//fin 
            else
            {
                MessageBox.Show("Debes incluir un nombre y/o una descripción", "Advertencia");
            }
            }
            catch (Exception ex)
            {
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GrillaFinal grillaFinal = new GrillaFinal();
            grillaFinal.Show();
        }
    }
}
