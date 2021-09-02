using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TimexServerApp.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(200)]
        public string EmployeeName { get; set; }
        // Foreign key
        public int? DepartmentId { get; set; }
        [Required]
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
        [ForeignKey("DepartmentId")]
        // Reference navigation
        public virtual Department Department { get; set; }
    }
}
