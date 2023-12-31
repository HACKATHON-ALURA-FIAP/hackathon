﻿using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsersService
    {
        UserDTO Create(
            Guid userId,
            string name,
            string personalDocument,
            string birthDate,
            string email,
            string phone
        );

        User GetById(Guid id);

        void Modify(User user);

        IEnumerable<User> GetAll(Func<User, bool> predicate);
    }
}
