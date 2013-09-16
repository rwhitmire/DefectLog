using System;
using DefectLog.Core.Models;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Areas.Admin.ViewModels;
using DefectLog.Web.Forms;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Defect, DefectListItem>()
                    .ForMember(dest => dest.DeveloperName, opt => opt.MapFrom(src => FormatAbbrevName(src.Developer.FirstName, src.Developer.LastName)))
                    .ForMember(dest => dest.TesterName, opt => opt.MapFrom(src => FormatAbbrevName(src.Tester.FirstName, src.Tester.LastName)))
                    .ForMember(dest => dest.CssClass, opt => opt.MapFrom(src => src.Status.CssClass))
                    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.PriorityLevel.PriorityName))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

                cfg.CreateMap<User, UserListItem>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => FormatProperName(src.FirstName, src.LastName)));

                cfg.CreateMap<Status, StatusListItem>();
                cfg.CreateMap<AppVersion, VersionListItem>();
                cfg.CreateMap<Comment, CommentListItem>();


                cfg.CreateMap<DefectForm, Defect>()
                   .AfterMap(MapComment);

                cfg.CreateMap<Category, CategoryListItem>();
                cfg.CreateMap<PriorityLevel, PriorityLevelItem>();

                cfg.CreateMap<RegisterForm, User>();
                cfg.CreateMap<UserProfileForm, User>();
                cfg.CreateMap<User, UserProfileForm>();

                cfg.CreateMap<AdminAppVersionForm, AppVersion>();
                cfg.CreateMap<AppVersion, AdminAppVersionForm>();

                cfg.CreateMap<AppVersion, AdminAppVersionListItem>()
                   .ForMember(dest => dest.DefectCount, opt => opt.MapFrom(src => src.Defects.Count));

                cfg.CreateMap<User, AdminUserListItem>()
                   .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));

                cfg.CreateMap<User, AdminUserForm>();
                cfg.CreateMap<AdminUserForm, User>();

                cfg.CreateMap<AdminCategoryForm, Category>();
                cfg.CreateMap<Category, AdminCategoryForm>();
            });
        }

        private static string FormatAbbrevName(string firstName, string lastName)
        {
            return firstName.Substring(0, 1) + ". " + lastName;
        }

        private static string FormatProperName(string firstName, string lastName)
        {
            return lastName + ", " + firstName;
        }

        private static void MapComment(DefectForm form, Defect defect)
        {
            if (string.IsNullOrEmpty(form.Comment)) return;

            var comment = new Comment
            {
                CommentDate = DateTime.Now,
                DefectId = form.Id,
                CommentText = form.Comment,
                UserId = form.UserId
            };

            defect.Comments.Add(comment);
        }
    }
}