using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TimexServerApp.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(200)]
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        [Required]
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
        public virtual Department Department { get; set; }
    }
}
