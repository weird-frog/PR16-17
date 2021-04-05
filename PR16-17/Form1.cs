using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR16_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string from, to;
            from = listBox1.SelectedItem.ToString();
            to = listBox2.SelectedItem.ToString();
        
            if (from == to)
            {
                MessageBox.Show("Это одна и та же валюта", "Внимание");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Введите количество валюты","Внимание");
            }
            else
            {
                //ссылка из учебника не работала. Ладно? Ладно
                webBrowser1.Navigate("https://www.google.com/search?q= " + textBox1.Text + "+" + from + "+" + to);
            }
        }
    }
}
