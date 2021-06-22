using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using AutoMapper;

namespace AssignmentManager.Server.Mapping
{
    public class ResourceToModelProfiler : Profile
    {
        public ResourceToModelProfiler()
        {
            CreateMap<SaveSpeciality, Speciality>();
            CreateMap<SaveGroupResource, Group>();
            CreateMap<SaveStudentResource, Student>();
            CreateMap<SaveSolutionResource, Solution>();
        }
    }
}