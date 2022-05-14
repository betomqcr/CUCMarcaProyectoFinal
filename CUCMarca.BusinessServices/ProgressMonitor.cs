using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public interface ProgressMonitor
    {

        void SetWork(int work);
        void Reset();
        void Increment();
        void Decrement();
        void DisplayMessage(string message);

    }
}
