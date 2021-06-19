﻿using AssignmentManager.Server.Models;
using AssignmentManager.Server.Resources;
using AutoMapper;

namespace AssignmentManager.Server.Mapping
{
    public class ResourceToModelProfiler : Profile
    {
        public ResourceToModelProfiler()
        {
            CreateMap<SaveSpecialityBriefly, Speciality>();
            CreateMap<SaveGroupResource, Group>();
        }
    }
}