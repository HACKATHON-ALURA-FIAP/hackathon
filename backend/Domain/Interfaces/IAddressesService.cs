using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressesService
    {
        AddressDTO Create(
            string line1,
            string line2,
            int number,
            string postalCode,
            string city,
            string state,
            string district,
            bool principal,
            Guid userId
        );

        Address GetById(Guid id);

        Address GetAddress(string postalCode);

        IEnumerable<Address> GetAddresses(Guid userId);

        void Delete(Guid id);
    }
}
