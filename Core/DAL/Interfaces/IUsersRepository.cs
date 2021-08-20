using moo_server.Core.Entities;

namespace moo_server.Core.DAL.Interfaces
{
    public interface IUsersRepository
    {
        // Возвращает или создаёт пользователя по идетификатору Телеграм
        User GetOrCreateUserByTgId(UserModel userModel);
        // Возвращает пользователя по идетификатору Телеграм
        User GetUser(long tgId);
        // Обновляет пользователя
        bool UpdateUser(User user);
        // Обновляет количество Му и дату последнего Му пользователя
        bool UpdateUserMooCountAndLastMooDateById(User user);
    }
}
