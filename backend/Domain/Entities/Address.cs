﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Address : Entity
    {
        public string Line1 { get; protected set; }
        public string Line2 { get; protected set; }
        public int Number { get; protected set; }
        public string PostalCode { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string District { get; protected set; }
        public bool Principal { get; protected set; }
        public Guid UserId { get; protected set; }
        public virtual User User { get; set; }
        public bool IsValid { get; set; }

        public Address()
        {
            IsValid = false;
        }

        public Address(string line1, string postalCode, string city, string state, string district)
        {
            Line1 = line1;
            PostalCode = postalCode;
            City = city;
            State = state;
            District = district;
            IsValid = true;
        }

        public Address(string line1, string line2, int number, string postalCode, string city, string state, string district, bool principal, Guid userId)
        {
            Id = Guid.NewGuid();
            Line1 = line1;
            Line2 = line2;
            Number = number;
            PostalCode = postalCode;
            City = city;
            State = state;
            District = district;
            Principal = principal;
            UserId = userId;
            IsValid = true;
        }
    }
}
