using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class DataResult<T> : Result
    {
        public DataResult(bool success, string message,T data) : base(success, message)
        {
            this.Data = data;
        }

        public T Data { get; set; }
    }
}
