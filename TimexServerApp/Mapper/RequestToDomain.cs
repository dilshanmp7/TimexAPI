using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimexServerApp.Models;
using TimexServerApp.Requests;

namespace TimexServerApp.Mapper
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            CreateMap<DepartmentRequest, Department>();
            CreateMap<EmployeeRequest, Employee>();
        }
    }
}
