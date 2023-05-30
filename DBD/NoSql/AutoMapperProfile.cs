using AutoMapper;
using NoSql.Entities.Dtos;
using NoSql.Entities.Entities;

namespace NoSql;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateStudentDto, StudentEntity>();
        CreateMap<CreateClassDto, ClassEntity>();
    }
}