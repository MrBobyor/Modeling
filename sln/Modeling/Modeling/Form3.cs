using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            int scale = 100;



            wX = pictureBox2.Width;
            hX = pictureBox2.Height;
            
            System.Drawing.Pen myPen1;
            System.Drawing.Pen myPen2;
            System.Drawing.Pen myPen3;
            myPen1 = new System.Drawing.Pen(System.Drawing.Color.Black);
            myPen2 = new System.Drawing.Pen(System.Drawing.Color.Blue);
            myPen3 = new System.Drawing.Pen(System.Drawing.Color.Red);
           
            //system cord
            Bitmap flag = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics flagGraphics = Graphics.FromImage(flag);
            flagGraphics.DrawLine(myPen1, 0, hX - 2, 0, 0);
            flagGraphics.DrawLine(myPen1, 0, hX - 2, wX, hX - 2);
            

            //values cord
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {
                flagGraphics.DrawLine(myPen1, i, hX - 2, i, hX + 2);
                i += wX / Convert.ToInt32(textBox2.Text);
            }

            // grafic F2
            int X2 = 0;
            int Y2 = hX - 2;
            for (y = 0; y < Convert.ToInt32(textBox2.Text); y++)
            {
                xF = (y + 1) * (wX / Convert.ToInt32(textBox2.Text));
                float tmp = elem.functionDisribution2(elem.val[y], (float)Convert.ToDouble(textBox1.Text));
                yF = hX - tmp * scale;
                //flag.SetPixel((int)xF, (int)yF, Color.Red);
                flagGraphics.DrawLine(myPen2, X2, Y2, (int)xF, (int)yF);
                X2 = (int)xF;
                Y2 = (int)yF;
            }
            
            myPen1.DashStyle = DashStyle.Dash;
            //1
            flagGraphics.DrawLine(myPen1, new Point(0, hX - scale), new Point(wX, hX - scale));
            //mesh
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i += (wX / Convert.ToInt32(textBox2.Text)))
                /*if (i == 0)
                    continue;
                else*/
                    flagGraphics.DrawLine(myPen1, new Point(i, 0), new Point(i, hX));

                myPen1.Dispose();
            pictureBox2.Image = flag;
        }

   

    }
}
