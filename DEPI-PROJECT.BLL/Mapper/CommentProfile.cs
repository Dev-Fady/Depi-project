using AutoMapper;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddCommentDto, Comment>()
                    .ForMember(dest => dest.DateComment, opt => opt.Ignore());
            CreateMap<UpdateCommentDto, Comment>()
                    .ForMember(dest => dest.UserID, opt => opt.Ignore())
                    .ForMember(dest => dest.PropertyId, opt => opt.Ignore());
            CreateMap<Comment, GetCommentDto>();
        }
    }
}
