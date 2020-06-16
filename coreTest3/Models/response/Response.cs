using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreTest3.Models.response
{
    public class Response
    {
        public Response(bool success, string message, dynamic data)
        {
            this.success = success;
            this.message = message;
            this.data = data;
        }

        public bool success { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}
