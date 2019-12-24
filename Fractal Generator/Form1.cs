using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal_Generator
{
    public partial class MyForm : Form
    {
        private readonly Settings s;

        public MyForm(Settings s, List<Fractal> fractals, List<Shader> shaders)
        {
            InitializeComponent();

            for (int i = 0; i < fractals.Count; i++)
            {
                listBox1.Items.Add(fractals[i].name);
            }

            for (int i = 0; i < shaders.Count; i++)
            {
                listBox2.Items.Add(shaders[i].name);

            }

            this.s = s;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            s.FractalIndex = 0;
            Console.WriteLine(s);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            s.ShaderIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pixels = fractals[choose_fractal].Render(properties);
            //Auxiliary.Save(pixels, properties.Name);
            //pixels = Auxiliary.Load(properties.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
