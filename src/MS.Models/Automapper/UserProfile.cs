using AutoMapper;
using MS.Entities;
using MS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Models.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<UserViewModel, User>();
        }
    }
}
