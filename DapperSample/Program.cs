using DapperSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new BasicSelectSample().Execute();

            new CRUDSample().Execute();

            new SelectSample().Execute();
        }
    }
}
