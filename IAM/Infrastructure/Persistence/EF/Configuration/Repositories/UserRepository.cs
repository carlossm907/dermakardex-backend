using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using dermakardex_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using IAM.Domain.Model.Aggregates;
using IAM.Domain.Model.ValueObjects;
using IAM.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Persistence.EF.Configuration.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>()
            .Any(u => u.Username == new Username(username));
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == new Username(username));
    }
}

