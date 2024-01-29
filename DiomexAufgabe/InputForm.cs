using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiomexAufgabe
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }
        public string Wert;
        public string WertName;

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Geben Sie den Wert für die Variable "+WertName+" ein.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Wert = textBox1.Text;
            this.Close();
        }
    }
}
