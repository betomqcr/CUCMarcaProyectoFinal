using CUCMarca.BusinessServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUCMarca.Procesos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {

                        FuncionarioService service = new FuncionarioService();
                        await service.GenerarInconsistencias(new DateTime(2022, 05, 11), 2);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
