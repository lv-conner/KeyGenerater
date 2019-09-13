using KeyGenerator.Interface;
using System;
using System.Threading.Tasks;

namespace KeyGenerator.Implement
{
    public class KeyGenerator : Orleans.Grain<int>, IKeyGenerator
    {
        private int _current;
        public Task<int> GetNextKeyAsync()
        {
            _current++;
            return Task.FromResult(_current);
        }
        protected override Task ReadStateAsync()
        {
            _current = 0;
            return Task.CompletedTask;
        }
        protected override Task WriteStateAsync()
        {
            return base.WriteStateAsync();
        }
    }
}
