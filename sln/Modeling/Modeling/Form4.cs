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
    public partial class Form4 : Form
    {
        GenValues elem;
        public Form4(double Z0,double Zn, GenValues Elem)
        {
            Form3 main = this.Owner as Form3;
            InitializeComponent();
            double z0 = Z0;
            double zn = Zn;
            label4.Text = Convert.ToString(Math.Round(z0, 3));
            label5.Text = Convert.ToString(Math.Round(zn, 3));
            textBox3.Text = label4.Text;
            elem = new GenValues(Elem.sigma, Elem.num);
            elem.Clone(Elem);
        }

        double alfa = 0;
        double k = 0;
        double[] q;
        double[] z;
        double R0;
        double F;

        int flag = 1;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                z = new double[Convert.ToInt32(textBox1.Text) + 1];
                q = new double[Convert.ToInt32(textBox1.Text)];
            }
            z[0] = Convert.ToDouble(label4.Text);
        }

        //ввод промежутков и формирование массива промежутков
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null)
                MessageBox.Show("Error", "Интервал не задан");
            if (flag < Convert.ToInt32(textBox1.Text))
            {
                string str = "interval " + flag.ToString() + "    (" + textBox3.Text + " , " + textBox4.Text + ")";
                listBox1.Items.Add(str);
                z[flag] = Convert.ToDouble(textBox4.Text);
                textBox3.Text = textBox4.Text;
                textBox4.Text = null;
                flag++;
                if (flag == Convert.ToInt32(textBox1.Text))
                {
                    button1.Enabled = false;
                    textBox4.Text = label5.Text;
                    z[flag] = Convert.ToDouble(textBox4.Text);
                    str = "interval " + flag.ToString() + "    (" + textBox3.Text + " , " + textBox4.Text + ")";
                    listBox1.Items.Add(str);
                    //ГИПОТЕЗА
                    for (int i = 1; i < Convert.ToInt32(textBox1.Text) + 1; i++)
                    {
                        q[i - 1] += metodTrapezium1(z[i - 1], z[i]);
                        listBox2.Items.Add(Convert.ToString(q[i - 1]));
                    }
                }
            }
        }

        private double metodTrapezium1(double a, double b)
        {
            double integral = 0;
            int n = 1000;
            for (int i = 1; i <= n; i++)
            {
                integral += (double)(elem.functionDisribution2((float)(a + (double)(b - a) * ((double)(i - 1) / (double)n)), elem.sigma) + elem.functionDisribution2((float)(a + (double)(b - a) * ((double)i / (double)n)), elem.sigma)) * (double)((double)b - (double)a) / (2.0 * (double)n);
            }
            return integral;
        }

        //double MethodOfTrapezium2(double R0)
        //{
        //    double integral = 0;
        //    int n = 1000;
        //    for (int i = 1; i <= n; i++)
        //    {
        //        integral += (double)(gFunction(R0 * (i - 1) / n) + gFunction(R0 * i / n));
        //    }
        //    return (R0 / 2 * n) * integral;
        //}
    }
}
