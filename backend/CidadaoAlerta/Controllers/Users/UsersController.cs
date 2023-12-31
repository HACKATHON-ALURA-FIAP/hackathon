﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CidadaoAlerta.Controllers.Users
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IAddressesService _addressesService;
        public UsersController(IUsersService usersService, IAddressesService AddressService)
        {
            _usersService = usersService;
            _addressesService = AddressService;
        }

        [HttpPost("CriarUsuario")]
        public IActionResult Create(UsersRequest request)
        {
            Guid userId = Guid.NewGuid();

            var userResponse = _usersService.Create(
                userId,
                request.Name,
                request.PersonalDocument,
                request.BirthDate,
                request.Email,
                request.Phone
                );

            if (!userResponse.IsValid)
            {
                return BadRequest(userResponse.Errors);
            }

            bool principalTrue = false;
            foreach (var item in request.Address)
            {
                if (item == null || item.PostalCode == "")
                {
                    return Ok(_usersService.GetById(userId));
                }

                if (item.Principal == true)
                {
                    if (principalTrue == true)
                    {
                        item.Principal = false;
                    }
                    else
                    {
                        principalTrue = true;
                    }
                }

                var viaCep = _addressesService.GetAddress(item.PostalCode);
                if (viaCep.City == null)
                {
                    return BadRequest("Usuário cadastrado com sucesso. Erro ao cadastrar o endereço: cep inválido:" + item.PostalCode);
                }

                var addressResponse = _addressesService.Create(
                    viaCep.Line1,
                    item.Line2,
                    item.Number,
                    item.PostalCode,
                    viaCep.City,
                    viaCep.State,
                    viaCep.District,
                    item.Principal,
                    userId
                    );
            }

            IEnumerable<Address> userAddresses = _addressesService.GetAddresses(userId);
            User user = _usersService.GetById(userId);

            foreach (var item in userAddresses)
            {
                item.User = null;
            }

            var response = new
            {
                id = user.Id,
                name = user.Name,
                personalDocument = user.PersonalDocument,
                birthDate = user.BirthDate,
                email = user.Email,
                phone = user.Phone,
                addresses = userAddresses
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UsersRequest request)
        {
            var user = _usersService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = request.Name;
            user.PersonalDocument = request.PersonalDocument;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.BirthDate = request.BirthDate;

            _usersService.Modify(user);
            var modifiedUser = _usersService.GetById(id);

            return Ok(modifiedUser);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _usersService.GetById(id);

            if (user == null || user.RemovedAt != null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _usersService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.RemovedAt = DateTime.Now;
            _usersService.Modify(user);

            return Ok("Usuario " + user.Name + " removido com sucesso");
        }

        [HttpGet()]
        public IActionResult GetByParameter([FromQuery] Dictionary<string, string> model)
        {
            var users = _usersService.GetAll(x =>
            {
                bool matches = true;
                if (model.TryGetValue("name", out string name))
                {
                    matches = matches && x.Name == name;
                }
                if (model.TryGetValue("personalDocument", out string personalDocument))
                {
                    matches = matches && x.PersonalDocument == personalDocument;
                }
                if (model.TryGetValue("email", out string email))
                {
                    matches = matches && x.Email == email;
                }
                return matches;
            });

            if (users == null)
            {
                return NotFound();
            }

            var userList = new List<User>();
            foreach (var user in users)
            {
                if (user.RemovedAt == null)
                {
                    userList.Add(user);
                }
            }

            double page = Math.Ceiling(userList.Count() / 5.0);
            var result = new
            {
                users = userList.OrderBy(x => x.Name),
                total = userList.Count(),
                pages = page,
                actual = 1
            };

            return Ok(result);
        }
    }
}
