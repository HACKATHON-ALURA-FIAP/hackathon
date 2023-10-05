using CidadaoAlerta.Interfaces;
using Domain.Entities;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(ApiCadastroContext dbContext) : base(dbContext) { }
    }
}
