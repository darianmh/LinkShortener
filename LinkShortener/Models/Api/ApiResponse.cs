using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Models.Api
{
    public class ApiResponse<T> : ApiResponseBase
    {
        public T Data { get; set; }
    }

    public class ApiResponse : ApiResponse<string>
    {

    }
}
