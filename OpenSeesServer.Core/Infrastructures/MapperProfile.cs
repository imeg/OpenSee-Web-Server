using AutoMapper;
using OpenSees.Tcl;
using OpenSeesServer.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSeesServer.Core.Infrastructures
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TclExecutionResult, TclExecutionResultViewModel>()
                .ForMember(dist=> dist.ErrorMessage, src=> src.MapFrom(i=> i.ErrorMessage))
                .ForMember(dist => dist.ExecutionStatus, src => src.MapFrom(i => i.ExecutionStatus))
                .ForMember(dist => dist.Result, src => src.MapFrom(i => i.Result));
        }
    }
}
