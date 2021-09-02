using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace TimexServerApp.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        [JsonIgnore] // Collection navigation
        public virtual ICollection<Employee> Employees { get; set; }
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

       
    }
}
