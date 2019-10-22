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
    public partial class Inicio : Form
    {
        private System.Windows.Forms.Timer timer1;
        private int counter = 5;
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            label4.Text = counter.ToString();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0) { 
                timer1.Stop();
                this.Hide();
                Form1 form = new Form1();
                form.Show();
                label4.Text = counter.ToString();
            }
            
        }
    }
}
