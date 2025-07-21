using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static OperationResult Ok()
        {
            return new OperationResult { Success = true, ErrorMessage = null };
        }
        public static OperationResult Error(string message)
        {
            return new OperationResult { Success = false, ErrorMessage = message };
        }
    }
}
