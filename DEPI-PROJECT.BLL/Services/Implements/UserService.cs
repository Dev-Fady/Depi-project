using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
                           IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResponseDto<PagedResultDto<UserResponseDto>>> GetAllUsersAsync(UserQueryDto userQueryDto)
        {
            var searchBy = userQueryDto.SearchText;
            var query = _userManager.Users
                              .IF(searchBy != null, a => a.UserName.Contains(searchBy) ||
                                                        a.Email.Contains(searchBy))
                              .OrderByExtended(new List<Tuple<bool, Expression<Func<User, object>>>>
                              {
                                  new (userQueryDto.OrderByOption == OrderByUserOptions.DataJoind, a => a.DateJoined)
                              }, userQueryDto.IsDesc)
                              .AsQueryable();

            var TotalCount = query.Count(); 

            var users = await query
                                .Paginate(new PagedQueryDto {PageNumber = userQueryDto.PageNumber, PageSize = userQueryDto.PageSize})
                                .ToListAsync();
            if (users.Count == 0)
            {
                return new ResponseDto<PagedResultDto<UserResponseDto>>
                {
                    Message = "No users found",
                    IsSuccess = true
                };
            }

            var userResponseDtos = _mapper.Map<List<User>, List<UserResponseDto>>(users);

            var PagedResult = new PagedResultDto<UserResponseDto>(userResponseDtos, userQueryDto.PageNumber, query.Count(), userQueryDto.PageSize);

            return new ResponseDto<PagedResultDto<UserResponseDto>>
            {
                Data = PagedResult,
                Message = "All users retrived successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<UserResponseDto>> GetUserByIdAsync(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user == null)
            {
                return new ResponseDto<UserResponseDto>
                {
                    Message = $"No user found with Id {UserId}",
                    IsSuccess = false
                };
            }

            var userResponseDto = _mapper.Map<User, UserResponseDto>(user);

            return new ResponseDto<UserResponseDto>
            {
                Data = userResponseDto,
                Message = "User Returned Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var User = await _userManager.FindByIdAsync(userUpdateDto.UserId.ToString());

            if (User == null)
            {
                return new ResponseDto<bool>
                {
                    Message = $"No user found with the given ID {userUpdateDto.UserId}",
                    IsSuccess = false
                };
            }
            _mapper.Map<UserUpdateDto, User>(userUpdateDto, User);

            var identityResult = await _userManager.UpdateAsync(User);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    Message = identityResult.Errors.ElementAt(0).Description
                                ?? "An error occured while updating user, please try again",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                Data = true,
                Message = "User Updated successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<bool>> DeleteUserAsync(Guid UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());

            if(user == null)
            {
                return new ResponseDto<bool>
                {
                    Message = $"No user found with Id {UserId}, please try again",
                    IsSuccess = false
                };
            }

            var identityResult = await _userManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    Message = identityResult.Errors.ElementAt(0).Description 
                                ?? "An error occured while Deleting user, please try again",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                Data = true,
                Message = "User Deleted successfully",
                IsSuccess = true
            };
        }
    }
}