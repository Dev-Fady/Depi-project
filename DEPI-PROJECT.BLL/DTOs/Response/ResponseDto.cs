using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.Response
{
    public class ResponseDto<T>
    {
        public required bool IsSuccess { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
    }
}
