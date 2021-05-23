using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Core.Tools
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }
        public ApiResponse(bool status,string message, T data )
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
        }
        public T Data{ get; set; }
        public bool Status{ get; set; }
        public string Message{ get; set; }
    }
}
