using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            var users = await _unitOfWork.UserRepository.GetAll();
            users = users.OrderBy(x=>x.First_name);
            return users;
        }
        public async Task<User> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            return user;
        }
        public async Task AddUser(UserForCreatoionDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
           await _unitOfWork.UserRepository.Insert(user);
        }
        public async Task<bool> UpdateUser(int id,UserForUpdateDto userDTO)
        {
            var userEntity = await _unitOfWork.UserRepository.GetById(id);
            if (userEntity != null)
            {
                userEntity.First_name = userDTO.First_name;
                userEntity.Last_name = userDTO.Last_name;
                userEntity.Rol_id = userDTO.Rol_id;

                await _unitOfWork.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> DeleteUser(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            return true;
           
        }
        
    }
}
