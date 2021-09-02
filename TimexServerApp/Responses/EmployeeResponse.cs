using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimexServerApp.Responses
{
    public class EmployeeResponse
    {
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
