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
            dtpProcess.Value = DateTime.Now.AddDays(-1);
            dtpProcess.MaxDate = DateTime.Now.AddDays(-1);
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

        private void lockGUI()
        {
            this.Enabled = false;
        }
        private void unlockGUI()
        {
            this.Enabled = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Reset();
            lockGUI();
            try
            {
                //La fecha debe ser sin minutos ni segundos
                DateTime dt = dtpProcess.Value.Date;
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
            finally
            {
                unlockGUI();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
