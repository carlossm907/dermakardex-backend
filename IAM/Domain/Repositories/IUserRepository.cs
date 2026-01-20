using dermakardex_backend.Shared.Domain.Repositories;
using IAM.Domain.Model.Aggregates;

namespace IAM.Domain.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);

    bool ExistsByUsername(string username);

}