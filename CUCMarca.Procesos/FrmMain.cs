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
    public partial class FrmMain : Form, ProgressMonitor
    {
        public FrmMain()
        {
            InitializeComponent();
            Reset();
            dtpProcess.Value = DateTime.Now;
        }

        public void Decrement()
        {
            pbMain.Value -= pbMain.Step;
        }

        public void DisplayMessage(string message)
        {
            lblMessages.Text = message;
        }

        public void Increment()
        {
            pbMain.PerformStep();
        }

        public void Reset()
        {
            pbMain.Step = 1;
            pbMain.Maximum = 1;
            pbMain.Minimum = 0;
            pbMain.Value = 0;
           
        }

        public void SetWork(int work)
        {
            pbMain.Maximum = work;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Reset();
            try
            {
                DateTime dt = dtpProcess.Value;
                String[] arr = txtPeriods.Text.Split(' ', ',');
                int[] periods = Array.ConvertAll(arr, int.Parse);
                FuncionarioService service = new FuncionarioService();
                await service.GenerarInconsistencias(dt, 
                    this,
                    periods);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
