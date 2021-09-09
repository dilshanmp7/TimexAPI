using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimexServerApp.Requests
{
    public class DepartmentRequest
    {
        [Required]
        public string DepartmentName { get; set; }
    }
}
