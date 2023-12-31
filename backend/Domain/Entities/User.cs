﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string PersonalDocument { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RemovedAt { get; set; } = null;

        public User(Guid id, string name, string personalDocument, string birthDate, string email, string phone)
        {
            Id = id;
            Name = name;
            PersonalDocument = personalDocument;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
        }

        protected bool ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            var words = Name.Split(' ');
            if (words.Length < 2)
            {
                return false;
            }

            foreach (var word in words)
            {
                if (word.Trim().Length < 2)
                {
                    return false;
                }
                if (word.Any(x => !char.IsLetter(x)))
                {
                    return false;
                }
            }

            return true;
        }

        protected bool ValidateDocument()
        {
            if (string.IsNullOrEmpty(PersonalDocument)) return false;

            var personalDocument = PersonalDocument.Replace(".", "").Replace("-", "");
            if (personalDocument.Length != 11) return false;

            if (!personalDocument.All(char.IsNumber)) return false;

            var first = personalDocument[0];
            if (personalDocument.Substring(1, 10).All(x => x == first)) return false;

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp;
            string digit;
            int sum;
            int rest;

            temp = personalDocument.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier1[i];
            }

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;

            digit = rest.ToString();
            temp += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit += rest.ToString();

            if (personalDocument.EndsWith(digit)) return true;

            return false;
        }

        protected bool ValidateEmail()
        {
            if (Email != null)
            {
                return Regex.IsMatch(
                Email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase
                );
            }

            return false;
        }

        protected bool ValidatePhone()
        {
            if (Phone != null && Phone != "")
            {
                var phone = Phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(".", "");
                if (phone.All(char.IsNumber))
                {
                    if (phone[2] == 57 && phone.Length == 11)
                    {
                        return true;
                    }
                    else if (Convert.ToInt32(phone[2].ToString()) != 9 && phone.Length == 10)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public (IList<string> errors, bool isValid) Validate()
        {
            var errors = new List<string>();
            if (!ValidateDocument())
            {
                errors.Add("CPF inválido.");
            }
            if (!ValidateName())
            {
                errors.Add("Nome inválido.");
            }
            if (!ValidateEmail())
            {
                errors.Add("E-mail inválido.");
            }
            if (!ValidatePhone())
            {
                errors.Add("Telefone inválido.");
            }
            return (errors, errors.Count == 0);
        }
    }
}
