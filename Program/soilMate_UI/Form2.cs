using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace soilMate_UI
{
    partial class Form2 : Form
    {
        CalculateResult result = new CalculateResult();

        public Form2(CalculateResult input_result)
        {
            InitializeComponent();
            result = input_result;
            
            label5.Text = result.plant1.name;
            label6.Text = result.plant2.name;
            label7.Text = result.plant3.name;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            //form1.result = new CalculateResult();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
