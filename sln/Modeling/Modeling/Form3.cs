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
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

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

            //заполнение таблицы числовых характеристик
            //значения
            dataGridView2.RowCount = 2;
            dataGridView2.ColumnCount = 8;
            dataGridView2.Rows[1].Cells[0].Value = 0;
            dataGridView2.Rows[1].Cells[1].Value = elem.SampleMean().ToString();
            dataGridView2.Rows[1].Cells[2].Value = 0;
            dataGridView2.Rows[1].Cells[3].Value = elem.SampleDispersion().ToString();
            dataGridView2.Rows[1].Cells[4].Value = elem.SampleNDispersion().ToString();
            dataGridView2.Rows[1].Cells[5].Value = 0;
            dataGridView2.Rows[1].Cells[6].Value = elem.SampleMedian().ToString();
            dataGridView2.Rows[1].Cells[7].Value = elem.SampleScope().ToString();
            //наименивания
            string s1 = "Eη",
                   s2 = "x",
                   s3 = "|Eη - x|",
                   s4 = "Dη",
                   s5 = "S2",
                   s6 = "|Dη - S2|",
                   s7 = "Me",
                   s8 = "R";
            dataGridView2.Rows[0].Cells[0].Value = s1;
            dataGridView2.Rows[0].Cells[1].Value = s2;
            dataGridView2.Rows[0].Cells[2].Value = s3;
            dataGridView2.Rows[0].Cells[3].Value = s4;
            dataGridView2.Rows[0].Cells[4].Value = s5;
            dataGridView2.Rows[0].Cells[5].Value = s6;
            dataGridView2.Rows[0].Cells[6].Value = s7;
            dataGridView2.Rows[0].Cells[7].Value = s8;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////------------------------------------------------------------------------------------------------
            //// grafic
            //int wX;
            //int hX;
            //double xF = 0, yF = 0, yF1 = 0;
            //int y;
            //int scale = 100;

            //wX = pictureBox2.Width;
            //hX = pictureBox2.Height;
            //pictureBox2.BackColor = Color.White;
            
            //System.Drawing.Pen myPen1;
            //System.Drawing.Pen myPen2;
            //System.Drawing.Pen myPen3;
            //myPen1 = new System.Drawing.Pen(System.Drawing.Color.Black);
            //myPen2 = new System.Drawing.Pen(System.Drawing.Color.Blue);
            //myPen3 = new System.Drawing.Pen(System.Drawing.Color.Red);
           
            ////system cord
            //Bitmap flag = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            //Graphics flagGraphics = Graphics.FromImage(flag);
            //flagGraphics.DrawLine(myPen1, 0, hX - 2, 0, 0);
            //flagGraphics.DrawLine(myPen1, 0, hX - 2, wX, hX - 2);
            

            ////values cord
            //for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            //{
            //    flagGraphics.DrawLine(myPen1, i, hX - 2, i, hX + 2);
            //    i += wX / Convert.ToInt32(textBox2.Text);
            //}

            //// grafic 
            //int X2 = 0;
            //int Y2 = hX - 2;
            //int Y2_1 = hX - 2;
            //for (y = 0; y < Convert.ToInt32(textBox2.Text); y++)
            //{
            //    xF = (y + 1) * (wX / Convert.ToInt32(textBox2.Text));
            //    float tmp = elem.functionDisribution2(elem.val[y], (float)Convert.ToDouble(textBox1.Text));
            //    float tmp1 = (1 / Convert.ToInt32(textBox2.Text)) * elem.functionDisribution2(elem.val[y], (float)Convert.ToDouble(textBox1.Text));
            //    yF = hX - tmp * scale; 
            //    yF1 = hX - tmp1 * scale;
            //    //flag.SetPixel((int)xF, (int)yF, Color.Red);
            //    flagGraphics.DrawLine(myPen2, X2, Y2, (int)xF, (int)yF);
            //    flagGraphics.DrawLine(myPen3, X2, Y2_1, (int)xF, (int)yF1);
            //    X2 = (int)xF;
            //    Y2 = (int)yF;
            //    Y2_1 = (int)yF1;
            //}
            
            //myPen1.DashStyle = DashStyle.Dash;
            ////1
            //flagGraphics.DrawLine(myPen1, new Point(0, hX - scale), new Point(wX, hX - scale));
            ////mesh
            ///*for (int i = 0; i < Convert.ToInt32(textBox2.Text); i += (wX / Convert.ToInt32(textBox2.Text)))
            //    if (i == 0)
            //        continue;
            //    else
            //        flagGraphics.DrawLine(myPen1, new Point(i, 0), new Point(i, hX));*/

            //myPen1.Dispose();
            //pictureBox2.Image = flag;

            //--------------------------------------------------------------------------------------------------
            //выборочная функция распределения
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //chart2.Series.Add("Функция распределения");
            //chart2.Series["Функция распределения"].Points.Clear();
            double Xmin = 0;
            double Xmax = double.Parse(textBox2.Text);
            double step = 1;
            int count = (int)Math.Ceiling((Xmax - Xmin) / step);
            double[] x1 = new double[count + 1];
            double[] k1 = new double[count + 1];

            x1[0] = Xmin;
            x1[count] = Xmin + step * (count);
            k1[0] = 0;
            k1[count] = 1;
            for (int i = 1; i < count; i++)
            {
                x1[i] = Xmin + step * i;
                k1[i] = elem.functionDisribution2(elem.val[i], (float)Convert.ToDouble(textBox1.Text));
                //chart2.Series[0].Points.AddXY(x[i], k[i]);
            }

            chart2.ChartAreas[0].AxisX.Minimum = Xmin;
            chart2.ChartAreas[0].AxisX.Maximum = Xmax;
            chart2.ChartAreas[0].AxisX.MajorGrid.Interval = step;
            chart2.Series[0].Points.DataBindXY(x1, k1);

            //--------------------------------------------------------------------------------------------------
            ////теоретическая функция распределения
            ////chart2.Series.Add("Функция распределения");
            ////chart2.Series["Функция распределения"].Points.Clear();
            //chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //Xmin = 0;
            //Xmax = double.Parse(textBox2.Text);
            //step = 1;
            //float stp = 0.1f;
            //double[] x2 = new double[count + 1];
            //double[] k2 = new double[count + 1];

            //x2[0] = Xmin;
            //x2[count] = Xmin + step * (count);
            //k2[0] = 0;
            //k2[count] = 1;
            //for (int i = 1; i < count; i++)
            //{
            //    x2[i] = Xmin + step * i;
            //    k2[i] = elem.functionDisribution2(stp, (float)Convert.ToDouble(textBox1.Text));
            //    //chart2.Series[0].Points.AddXY(x[i], k[i]);
            //}

            //chart2.ChartAreas[0].AxisX.Minimum = Xmin;
            //chart2.ChartAreas[0].AxisX.Maximum = Xmax;
            //chart2.ChartAreas[0].AxisX.MajorGrid.Interval = step;
            //chart2.Series[0].Points.DataBindXY(x2, k2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //gistogramm
            //chart1.SetBounds((int)elem.val[0],(int)elem.val[elem.num - 1], 1, 1);
            int gist = Convert.ToInt32(textBox3.Text);
            int step = (int)((elem.GetMaxPeriod() - elem.GetMinPeriod()) / gist);
            //ChartArea area = new ChartArea();
            //area.AxisX.Minimum = elem.GetMinPeriod();
            //area.AxisX.Maximum = elem.GetMaxPeriod();
            //area.AxisX.MajorGrid.Interval = step;
            //chart1.ChartAreas.Add(area);

            //Series series1 = new Series();
            //series1.ChartType = SeriesChartType.Column;
            //series1.LegendText = "Колличество элементов на интервале";
            //chart1.Series.Add("Экспериментов на интервале");

            chart1.Series.Add("Экспериментов на интервале");
            chart1.Series["Экспериментов на интервале"].Points.Clear();
            float[] k = new float[gist];
            float limit = elem.GetMinPeriod() + step;
            for (int i = 0; i < gist; i++)
            {
                int ki = 0;
                for (int j = 0; j < elem.num; j++)
                {
                    if (elem.val[j] <= limit && elem.val[j] > limit - step)
                    {
                        ki++;
                        k[i] = ki;
                    }
                    else
                        limit += step;
                    break;
                }
                chart1.Series["Экспериментов на интервале"].Points.AddXY(Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * i), 2)) + "-" + Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * (i + 1)), 2)), k[i]);
            //chart1.DataBind();

            }
        
            //chart1.Series["Экспериментов на интервале"].Points.AddXY(Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * i), 2)) + "-" + Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * (i + 1)), 2)), gist);
            //chart1.Series[0].Points.DataBindXY(x, k);

            //chart1.Series.Add("Экспериментов на интервале");
            //chart1.Series["Экспериментов на интервале"].Points.Clear();
            //int count = Int32.Parse(textBox2.Text);

            //for (int i = 0; i < Int32.Parse(textBox3.Text); i++)
            //{
            //    int gist = 0;
            //    for (int j = 0; j < count; j++)
            //    {
            //        if (j == count - 1)
            //        {
            //            if (Math.Round(elem.val[j], 2) <= ( && Math.Round(elem.val[j], 2) >= (Math.Round((elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * i)), 2)))
            //                gist++;
            //        }
            //        else
            //        {
            //            if (Math.Round(elem.val[j], 2) <= Math.Round((elem.val[0] + Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * (i + 1))) && Math.Round(elem.val[j], 2) >= Math.Round((elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * i)), 2))
            //                gist++;
            //        }
            //    }
            //    chart1.Series["Экспериментов на интервале"].Points.AddXY(Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * i), 2)) + "-" + Convert.ToString(Math.Round(elem.val[0] + (Math.Round(Math.Round(elem.val[Convert.ToInt32(textBox2.Text) - 1] - elem.val[0], 2) / Int32.Parse(textBox3.Text), 2) * (i + 1)), 2)), gist);

            //}
        }

    }
}
