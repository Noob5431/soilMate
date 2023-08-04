using GMap.NET.MapProviders;
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
    
    
    partial class Form1 : Form
    {
        public float longitude;
        public float latitude;
        public int id;
        public MapMarker location = new MapMarker();
        public CalculateResult result = new CalculateResult();

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void longText_TextChanged(object sender, EventArgs e)
        {
            longitude = float.Parse( longText.Text);

        }


        public void latText_TextChanged(object sender, EventArgs e)
        {
            latitude = float.Parse( latText.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            
            location.latitude =  latitude.ToString("N8");
            location.longitude = longitude.ToString("N8");
            location.id = id.ToString("N8");
            
        }
        
        

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void calculateButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program1 program = new Program1();
            result = program.Calculate((long)longitude*1000000, (long)latitude*1000000, id);
            Form2 form2 = new Form2(result);
            form2.Show();
        }

        private void mapPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            id = int.Parse(idText.Text);
        }
    }
   
}

