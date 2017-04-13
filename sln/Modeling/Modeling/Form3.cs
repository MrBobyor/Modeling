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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(@"F:\Inf\sashka einstein\education\семестры\весенний семестр 2017\ТВиМС (лаб)\лаб.раб\Modeling\pictures\E2.png");
        }

        GenValues elem;

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 2;
            dataGridView1.ColumnCount = Convert.ToInt32(textBox2.Text);
            elem = new GenValues((float)Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text));
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                elem.GenVal(elem.GetRandomValue());
            elem.GetVal();

            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {  
                dataGridView1.Rows[0].Cells[i].Value = "x" + (i + 1);
                dataGridView1.Rows[1].Cells[i].Value =  elem.val[i].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int wX;
            int hX;
            double xF = 0, yF = 0;
            int y;

            wX = pictureBox2.Width;
            hX = pictureBox2.Height;
            
            //syscord
            Bitmap flag = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics flagGraphics = Graphics.FromImage(flag);
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            flagGraphics.DrawLine(myPen, 0, hX - 2, 0, 0);
            flagGraphics.DrawLine(myPen, 0, hX - 2, wX, hX - 2);

            int X = 0;
            int Y = hX - 2;

            // grafic F2
            for (y = 0; y < Convert.ToInt32(textBox2.Text); y++)
            {
                xF = y * 25;
                float tmp = elem.functionDisribution2(elem.val[y], (float)Convert.ToDouble(textBox1.Text));
                yF = hX - tmp * 225;
                //flag.SetPixel((int)xF, (int)yF, Color.Red);
                System.Drawing.Pen myPen2;
                myPen2 = new System.Drawing.Pen(System.Drawing.Color.Blue);
                flagGraphics.DrawLine(myPen2, X, Y, (int)xF, (int)yF);
                X = (int)xF;
                Y = (int)yF;
            }
            pictureBox2.Image = flag;
        }

   

    }
}
