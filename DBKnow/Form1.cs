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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            if (!this.txtNombre.Text.ToString().Equals("") && !this.txtClave.Text.ToString().Equals(""))
            {
                string Usuario = this.txtNombre.Text.ToString().ToLower();
                string Clave = this.txtClave.Text.ToString();
                if (ClaseVariable.Ingreso(Usuario, Clave) != 0)
                {
                        this.Hide();
                        GrillaFinal grillaFinal = new GrillaFinal();
                        ClaseVariable.Usuario = Usuario.ToString().ToLowerInvariant();
                        grillaFinal.Show();
                }
                else
                {
                    ClaseVariable.AgregarLog("No Ingreso: Usuario: " + Usuario + " Clave: " + Clave);
                    this.lblError.Text = "Error al trata de ingresar: " + Usuario;
                    this.lblError.Enabled = true;
                    this.lblError.Visible = true;
                }

            }
            else {
                ClaseVariable.AgregarLog("Error al tratar de ingresar!!!");
            }
            }
            catch (Exception ex)
            {
                ClaseVariable.AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
