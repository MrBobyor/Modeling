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
            pictureBox2.Image = new Bitmap(@"F:\Inf\sashka einstein\education\семестры\весенний семестр 2017\ТВиМС (лаб)\лаб.раб\Modeling\pictures\E1.png");
        }

        GenValues elem;

        private void button1_Click(object sender, EventArgs e)
        {
            elem = new GenValues((float)Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text));
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                elem.GenVal(elem.GetRandomValue());
            elem.GetVal();

            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                listBox1.Items.Add("x" + (i + 1) + "  " + elem.val[i].ToString());

            //заполнение таблицы числовых характеристик
            //значения
            dataGridView2.RowCount = 2;
            dataGridView2.ColumnCount = 8;
            dataGridView2.Rows[1].Cells[0].Value = elem.MathExpectation().ToString();
            dataGridView2.Rows[1].Cells[1].Value = elem.SampleMean().ToString();
            dataGridView2.Rows[1].Cells[2].Value = Math.Abs(elem.MathExpectation() - elem.SampleMean()).ToString();
            dataGridView2.Rows[1].Cells[3].Value = elem.SampleDispersion().ToString();
            dataGridView2.Rows[1].Cells[4].Value = elem.TheoreticalDispersion().ToString();
            dataGridView2.Rows[1].Cells[5].Value = Math.Abs(elem.SampleDispersion() - elem.TheoreticalDispersion()).ToString();
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
            double Xmin = 0;
            double Xmax = double.Parse(textBox2.Text);
            double step = 0.5;
            int count = Convert.ToInt32(textBox2.Text);

            chart2.ChartAreas[0].AxisX.Minimum = Xmin;
            chart2.ChartAreas[0].AxisX.Maximum = 10;
            chart2.ChartAreas[0].AxisX.MajorGrid.Interval = step;
          
            //теоретическая функция распределения
            chart2.Series.Add("теоретическая");
            chart2.Series["теоретическая"].Points.Clear();
            chart2.Series["теоретическая"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (int i = 0; i < 100; i++)
            {
                chart2.Series["теоретическая"].Points.AddXY(i, elem.functionDisribution2(i, (float)Convert.ToDouble(textBox1.Text)));
            }
            
            //выборочная функция распределения
            chart2.Series[0].LegendText = "выборочная";
            chart2.Series[0].Points.Clear();
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            double y = 0;
            double max = 0;
            int z = -1;
            float[] valu = new float[count+1];
            valu[0] = 0;
            for (int i = 0; i < count; i++)
            {
                valu[i + 1] = elem.val[i];
            }
            for (int i = 0; i < count + 1; i++)
            {
                max = ((double)(i) / (double)(count) - elem.functionDisribution2(i, (float)Convert.ToDouble(textBox1.Text)));
                z++;
                y = (double)(z) / (double)(count);
                if (elem.functionDisribution2(valu[i], (float)Convert.ToDouble(textBox1.Text)) - ((double)(i - 1) / (double)(count)) > max)
                    max = (elem.functionDisribution2(valu[i], (float)Convert.ToDouble(textBox1.Text))) - ((double)(i - 1) / (double)(count));
                chart2.Series[0].Points.AddXY(valu[i], y);
            }

            //величина D
            double D = 0;
            for (int j = 0; j < Convert.ToInt32(textBox2.Text); j++)
            {
                if((j / Convert.ToInt32(textBox2.Text)) - elem.functionDisribution2(elem.val[j] ,Convert.ToInt32(textBox2.Text)) > elem.functionDisribution2(elem.val[j] ,Convert.ToInt32(textBox2.Text)) - ((j - 1)/Convert.ToInt32(textBox2.Text)))
                    D = (j / Convert.ToInt32(textBox2.Text)) - elem.functionDisribution2(elem.val[j] ,Convert.ToInt32(textBox2.Text));
                else
                    D = elem.functionDisribution2(elem.val[j] ,Convert.ToInt32(textBox2.Text)) - ((j - 1)/Convert.ToInt32(textBox2.Text));
            }
            label9.Text = Convert.ToString(Math.Round(D,3));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //гистограмма
            int count = Int32.Parse(textBox2.Text);
            double step = Math.Round(Math.Round(elem.val[count - 1], 10) / Int32.Parse(textBox3.Text), 10); 
            chart1.Series[0].LegendText = "Элементов на интервале";
            //chart1.Series[0].LegendText = "Гистограмма относительно частот";
            chart1.Series[0].Points.Clear(); 
            int []gist = new int[Int32.Parse(textBox3.Text) + 1]; 
            Array.Sort(elem.val); 
            int k = 0; 
            
            for (int i = 0; i < count; i++) 
            { 
                if ((elem.val[i] > k * step) && (elem.val[i] < (k + 1) * step)) 
                { 
                    gist[k]++; 
                } 
                else 
                { 
                    k++; 
                    i--; 
                }
            }

            for (int j = 0; j < Int32.Parse(textBox3.Text); j++)
                chart1.Series[0].Points.Add(gist[j]);
            chart1.DataBind();    
 
            //таблица результатов
            //значения
            double result = 0;
            dataGridView3.RowCount = 3;
            dataGridView3.ColumnCount = Convert.ToInt32(textBox3.Text);
            for(int i = 0; i < Convert.ToInt32(textBox3.Text); i++)
            {
                double value1 = Math.Round(elem.GetMinPeriod() + step * (i + (1/2)), 3);
                double value2 = Math.Round(elem.functionDistributionDensity2(value1, Convert.ToDouble(textBox1.Text)), 3);
                double value3 = Math.Round((gist[i] / (Convert.ToInt32(textBox3.Text) * step)), 3);
                dataGridView3.Rows[0].Cells[i].Value = value1.ToString();
                dataGridView3.Rows[1].Cells[i].Value = value2.ToString();
                dataGridView3.Rows[2].Cells[i].Value = value3.ToString();
                if(result < Math.Abs(value3 - value2))
                    result = Math.Abs(value3 - value2);
                //chart1.Series[0].Points.Add(value3);   //гистограмма относительно частот
            }
            label7.Text = Convert.ToString(result);
            //chart1.DataBind();
        }

    }
}
