using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modeling
{
    public partial class Form2 : Form
    {
        public float aver;  //average value
        public float dis;   //dispersion
        public int num;   //namber of people

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            textBox1.Text = "0";
            int x = Convert.ToInt32(textBox1.Text);
            num = x;
            label2.Text = Convert.ToString(num);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
                textBox1.Text = "0";
            float x = (float)Convert.ToDouble(textBox2.Text);
            dis = x;
            label5.Text = Convert.ToString(dis);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
                textBox1.Text = "0";
            float x = (float)Convert.ToDouble(textBox3.Text);
            aver = x;
            label6.Text = Convert.ToString(aver);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void raffle()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            float[] raf = new float[num];
            for (int i = 0; i < num - 1; i++)
                raf[i] = rnd.Next(num);
        }
        
    }
}
