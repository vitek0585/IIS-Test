using System.AddIn.Contract;
using System.AddIn.Pipeline;

namespace CarContracts
{
    [AddInContract]
    public interface ICar : IContract
    {
        string Model { get; set; }
    }
}
