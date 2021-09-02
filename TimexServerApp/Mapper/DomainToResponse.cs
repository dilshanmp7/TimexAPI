using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;
using TimexServerApp.Responses;

namespace TimexServerApp.Mapper
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Department, DepartmentResponse>();
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest=>dest.DepartmentName,opt=>
                opt.MapFrom(src=>src.Department.DepartmentName));
        }

    }
}
