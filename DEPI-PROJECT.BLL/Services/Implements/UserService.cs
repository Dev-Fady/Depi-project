using System.Reflection.Metadata.Ecma335;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class UserService : IUserService
    {

        public UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseDto<List<UserResponseDto>>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

            if(users.Count == 0)
            {
                return new ResponseDto<List<UserResponseDto>>
                {
                    Message = "No users found",
                    IsSuccess = true
                };
            }

            foreach (var user in users)
            {
                userResponseDtos.Add(new UserResponseDto
                {
                    UserId = user.UserId,
                    Username = user.UserName,
                    Email = user.Email,
                    phone = user.PhoneNumber,
                    DateJoined = user.DateJoined,
                    // RoleType = await _roleService.getUserRoleAsync()
                });
            }

            return new ResponseDto<List<UserResponseDto>>
            {
                Data = userResponseDtos,
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

            var userResponseDto = new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.UserName,
                Email = user.Email,
                phone = user.PhoneNumber,
                DateJoined = user.DateJoined,
                // RoleType = await _roleService.getUserRoleAsync()
            };

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
            User.UserName = userUpdateDto.Username;
            User.Email = userUpdateDto.Email;
            User.PhoneNumber = userUpdateDto.phone;

            var identityResult = await _userManager.UpdateAsync(User);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occured while updating user, please try again",
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
                    Message = "An error occured while Deleting user, please try again",
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