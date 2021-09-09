using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimexServerApp.Requests
{
    public class EmployeeRequest
    {
        [Required]
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
