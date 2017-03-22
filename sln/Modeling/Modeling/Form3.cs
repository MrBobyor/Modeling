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
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 2;
            dataGridView1.ColumnCount = Convert.ToInt32(textBox2.Text);
            ConditionalFunc elem = new ConditionalFunc((float)Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text));
            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
                elem.SetVal(elem.GetRandomValue());
            elem.GetVal();

            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {  
                dataGridView1.Rows[0].Cells[i].Value = "x" + (i + 1);
                dataGridView1.Rows[1].Cells[i].Value =  elem.val[i].ToString();
            }
        }

    }
}
