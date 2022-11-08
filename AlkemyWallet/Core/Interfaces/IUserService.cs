using AlkemyWallet.Core.Models;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetById(int id);
        Task AddUser(UserForCreatoionDto userDTO);
        Task<bool> UpdateUser(int id, UserForUpdateDto userDTO);
        Task<bool> DeleteUser(int id);
    }
}
