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
        double z0 ;
        double zn;
        int split = 0;
        public Form4(double Z0,double Zn, GenValues Elem)
        {
            Form3 main = this.Owner as Form3;
            InitializeComponent();
            z0 = Z0;
            zn = Zn;
            label4.Text = Convert.ToString(Math.Round(z0, 3));
            label5.Text = Convert.ToString(Math.Round(zn, 3));
            textBox3.Text = "- бесконечность";
            elem = new GenValues(Elem.sigma, Elem.num);
            elem.Clone(Elem);
        }
        
        double[] q;
        double[] z;
        int[] nj;

        int flag = 1;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                z = new double[Convert.ToInt32(textBox1.Text) + 1];
                q = new double[Convert.ToInt32(textBox1.Text)];
            }
            z[0] = 0;
        }

        //ввод промежутков и формирование массива промежутков
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null)
                MessageBox.Show("Error", "Интервал не задан");
            if (flag < Convert.ToInt32(textBox1.Text))
            {
                if (flag == 0)
                {
                    string str = "interval " + flag.ToString() + "    (" + textBox3.Text + " , " + "- бесконечность)";
                    listBox1.Items.Add(str);
                    z[flag] = 0;
                    textBox3.Text = textBox4.Text;
                    textBox4.Text = null;
                    flag++;
                }
                else
                {
                    string str = "interval " + flag.ToString() + "    (" + textBox3.Text + " , " + textBox4.Text + ")";
                    listBox1.Items.Add(str);
                    z[flag] = Convert.ToDouble(textBox4.Text);
                    textBox3.Text = textBox4.Text;
                    textBox4.Text = null;
                    flag++;
                }
                if (flag == Convert.ToInt32(textBox1.Text))
                {
                    button1.Enabled = false;
                    textBox4.Text = label5.Text;
                    z[flag] = Convert.ToDouble(zn);
                    string str = "interval " + flag.ToString() + "    (" + textBox3.Text + " , " + "+ бесконечность)";
                    listBox1.Items.Add(str);
                    //ГИПОТЕЗА
                    for (int i = 1; i < Convert.ToInt32(textBox1.Text) + 1; i++)
                    {
                        q[i - 1] += metodTrapezium1(z[i - 1], z[i]);
                        listBox2.Items.Add(Convert.ToString(q[i - 1]));
                    }
                    button2.Enabled = true;
                    label6.Text = Convert.ToString(Math.Round(valueRO(), 4));
                }
            }
        }

        //мера расхождения
        private double valueRO()
        {
            NumbersOfValuesInIntervalMas();
            double R0 = 0;
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                R0 += ((Math.Pow((nj[i] - elem.num * q[i]), 2)) / (elem.num * q[i]));
            }
            return R0;
        }

        //значения на промежутках
        private void NumbersOfValuesInIntervalMas()
        {
            nj = new int[Convert.ToInt32(textBox1.Text)];
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                nj[i] = 0;
            int j = 0;
            double zF = z[j + 1];
            for (int i = 0; i < elem.num; i++)
            {
                if (elem.val[i] < zF)
                    nj[j]++;
                else
                {
                    j++;
                    nj[j]++;
                    zF = z[j + 1];
                }
            }
        }

        private double metodTrapezium1(double a, double b)
        {
            double integral = 0;
            int n = 1000;
            for (int i = 1; i <= n; i++)
            {
                //integral += ((double)(elem.functionDisribution2((float)(a + (b - a) * ((i - 1) / n)), elem.sigma) + elem.functionDisribution2((float)(a + (b - a) * ( i / n)), elem.sigma)) *  (b - a) / (2.0 * n));
                integral += (double)(elem.functionDistributionDensity2((float)(a + (double)(b - a) * ((double)(i - 1) / (double)n)), elem.sigma) + elem.functionDistributionDensity2((float)(a + (double)(b - a) * ((double)i / (double)n)), elem.sigma)) * (double)((double)b - (double)a) / (2.0 * (double)n);
            }
            return integral;
        }
        
        //плотность распределения chi2
        public double densityFunctionOfChiDistribution(double x)
        {
            int r = Convert.ToInt32(textBox1.Text);
            if (x > 0)
                return Math.Pow(2, (-r / 2)) * Math.Pow(GammaFunction(r / 2), (-1)) * Math.Pow(x, (r / (2 - 1))) * Math.Exp(-x / 2);
            else return 0;
        }
        
        private double GammaFunction(double value)
        {
            double gamma = 1;
            if (value == 0)
            {
                return 1;
            }
            if ((int)value == value)
            {
                for (int i = 2; i < value; i++)
                {
                    gamma *= i;
                }
            }
            else
            {
                gamma = (double)Math.Pow(Math.PI, 0.5);
                for (double i = (1.0 / 2.0); i < value; i++)
                {
                    gamma *= i;
                }
            }
            return gamma;
        }

        //ПРИНЯТИЕ.ОТКЛОНЕНИЕ
        private void button2_Click(object sender, EventArgs e)
        {
            split = Convert.ToInt32(textBox1.Text);
            double F = 0;
            double gamma = 1 / GammaFunction(split / 2);
            double g = methodOfTrapezium2(valueRO());
            for (int j = 1; j < 300; j++)
                for (int k = 0; k < 1000; k++)
                    F = 1 - Math.Pow(2, -split / 2) * gamma * g;
            label11.Text = Convert.ToString(F);
            double alfa = Convert.ToDouble(textBox2.Text);
            if (alfa <= 0 || alfa > 1)
                MessageBox.Show("Error", "Значение альфа должно принадлежать промежутку (0, 1]");
            else
            {
                if (alfa > F)
                    label7.Text = "ПРИНЯТА";
                else label7.Text = "ОТКЛОНЕНА";
            }
        }

        double methodOfTrapezium2(double R0)
        {
            double integral = 0;
            int n = 1000;
            for (int i = 1; i <= n; i++)
            {
                integral += (double)(gFunction(R0 * (i - 1) / n) + gFunction(R0 * i / n));
            }
            return (R0 /( 2 * n)) * integral;
        }

        public double gFunction(double u)
        {
            return Math.Pow(u, (Convert.ToInt32(textBox1.Text) / 2 - 1)) * Math.Exp((-1) * u / 2);
        }
    }
}
