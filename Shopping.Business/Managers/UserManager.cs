using Microsoft.AspNetCore.DataProtection;
using Shopping.Business.Dtos;
using Shopping.Business.Services;
using Shopping.Business.Types;
using Shopping.Data.Entities;
using Shopping.Data.Repositories;

namespace Shopping.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;

        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == addUserDto.Email.ToLower()).ToList();

            if (hasMail.Any())
            {
                return new ServiceMessage()
                {
                    IsSucceed = false,
                    Message = "Bu Eposta adresli bir kullanıcı zaten mevcut."
                };
            }

            var entity = new UserEntity()
            {
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Password = _dataProtector.Protect(addUserDto.Password),
                UserType = Data.Enums.UserTypeEnum.User
            };

            _userRepository.Add(entity);

            return new ServiceMessage()
            {
                IsSucceed = true,
                Message = "Hesap oluşturuldu."
            };
        }
        public UserInfoDto LoginUser(LoginDto loginDto)
        {
            var userEntity = _userRepository.Get(x => x.Email == loginDto.Email);

            if (userEntity is null)
            {
                return null;
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password);

            if (loginDto.Password == rawPassword)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType
                };
            }
            else
            {
                return null;
            }

        }
    }
}
