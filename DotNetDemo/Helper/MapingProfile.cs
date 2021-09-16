using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataModel.DTO;
using DataModel.Entity;

namespace API.Helper
{ 
    public class MapingProfile: Profile
{
    public MapingProfile(){
        CreateMap<Employee, CreateEmployeeDto>();
        }
}
}