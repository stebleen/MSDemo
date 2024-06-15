using AutoMapper;
using MS.Entities;
using MS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Models.Automapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryViewModel, Category>();
        }

    }
}
