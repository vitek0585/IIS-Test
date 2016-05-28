using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarContracts;

namespace MAF
{
    [AddInAdapter]
    public class CarAddIn : ContractBase, ICar
    {
        private string _model;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public CarAddIn()
        {
            _model = "BMW X 5";
        }
    }
    [AddInBase]
    public abstract class CarBase
    {
        public abstract string GetCarModel();
    }
    [HostAdapter]
    public class CarHost : CarBase
    {
        private ICar _car;
        public override string GetCarModel()
        {
            return _car.Model;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
