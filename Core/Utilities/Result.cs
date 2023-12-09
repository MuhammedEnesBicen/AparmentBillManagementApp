using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Result( bool success,string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
