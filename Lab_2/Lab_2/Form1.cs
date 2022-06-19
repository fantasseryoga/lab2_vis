using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b'))
                e.Handled = true;
            if (e.KeyChar == '.')
            {
                if ((sender as TextBox).Text.Contains("."))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (e.KeyChar == '-')
            {
                if ((sender as TextBox).Text.Length == 0)
                    e.Handled = false;
                else

                    e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double UpBorder = 0, DownBorder = 0;
            int Tabs=0;
            try
            {
                UpBorder = double.Parse(textBoxLowerBound.Text);
                DownBorder = double.Parse(textBoxUpperBound.Text);
                Tabs = int.Parse(textBoxPoints.Text);

            }
            catch (Exception exception)
            {
                statusStrip1.Text = exception.Message;
                return;
            }
            if(radioButtonImplicit.Checked)
                FillDataTableImplicitFunction(UpBorder, DownBorder, Tabs);
            else if(radioButtonExplicit.Checked)
                FillDataTableExplicitFunction(UpBorder,DownBorder,Tabs);
            chart1.DataBind();

        }

        private void FillDataTableImplicitFunction(double upBorder, double downBorder, int tabs)
        {
            double step = (upBorder - downBorder) / (tabs - 1);
            double t,x,y;
            double max=0, min=0;
            dataTable.Rows.Clear();
            for (int i = 0; i < tabs; i++)
            {
                t=downBorder+i*step;
                x = Math.Pow(Math.Cos(t), 4);
                y = Math.Pow(Math.Sin(t), 4);
                if(i==0)
                {
                    min = max = y;
                }
                if (min > y)
                    min = y;
                if (max < y)
                    max = y;
                DataRow dr= dataTable.NewRow();
                dr["t"] = t;
                dr["x"] = x;
                dr ["y"] = y;
                dataTable.Rows.Add(dr);
            }
            labelMin.Text =Math.Round(min,2).ToString();
            labelMax.Text =Math.Round(max,2).ToString();

        }

        private void FillDataTableExplicitFunction(double upBorder, double downBorder,int tabs)
        {
            
            double x, y;
            double step = (upBorder - downBorder) / (tabs - 1);
            double max = 0, min = 0;
            dataTable.Rows.Clear();
            for (int i = 0; i < tabs; i++)
            {
                
                x = downBorder+i*step;
                y = (Math.Sin(x)/(2+ Math.Cos(x)));
                if (i == 0)
                {
                    min = max = y;
                }
                if (min > y)
                    min = y;
                if (max < y)
                    max = y;
                DataRow dr = dataTable.NewRow();
                
                dr["x"] = x;
                dr["y"] = y;
                dataTable.Rows.Add(dr);
            }

            labelMin.Text = Math.Round(min, 2).ToString();
            labelMax.Text = Math.Round(max, 2).ToString();
        }

        private void radioButtonExplicit_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as RadioButton).Checked)
                columnT.Visible = false;
               
        }

        private void radioButtonImplicit_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                columnT.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButtonExplicit.Checked = true;
        }
    }
}

