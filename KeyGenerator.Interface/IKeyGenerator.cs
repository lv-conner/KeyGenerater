using System;
using System.Threading.Tasks;

namespace KeyGenerator.Interface
{
    public interface IKeyGenerator: Orleans.IGrainWithIntegerKey
    {
        Task<int> GetNextKeyAsync();
    }
}
