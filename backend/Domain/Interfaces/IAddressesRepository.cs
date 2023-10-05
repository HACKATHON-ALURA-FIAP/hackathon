using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressesRepository : IGenericRepository<Address>
    {
        IEnumerable<Address> GetByUserId(Guid id);
    }
}
