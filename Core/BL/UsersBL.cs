using moo_server.Core.DAL.Interfaces;
using moo_server.Core.Entities;

namespace moo_server.Core.BL.Interfaces
{
    public class UsersBL : IUsersBL
    {
        private readonly IUsersRepository _usersRepository;

        public UsersBL(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetOrCreateUserByTgId(UserModel userModel)
        {
            return _usersRepository.GetOrCreateUserByTgId(userModel);
        }

        public User GetUser(long tgId)
        {
            return _usersRepository.GetUser(tgId);
        }

        public User GetUserByTgUsername(string tgUsername)
        {
            return _usersRepository.GetUserByTgUsername(tgUsername);
        }

        public bool UpdateUser(User user)
        {
            return _usersRepository.UpdateUser(user);
        }

        public bool UpdateUserMooCountAndLastMooDateById(User user)
        {
            return _usersRepository.UpdateUserMooCountAndLastMooDateById(user);
        }
    }
}
