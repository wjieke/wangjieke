using AutoMapper;
using Model.Entity.System;
using Model.ModelView;

namespace Library.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>();
            //CreateMap<Menu, MenuViewModel>();
            //CreateMap<Permission, PermissionViewModel>();
            //CreateMap<Role, RoleViewModel>();
        }
    }
}
