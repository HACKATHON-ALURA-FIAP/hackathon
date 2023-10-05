using CidadaoAlerta.Controllers.Addresses;

namespace CidadaoAlerta.Controllers.Users
{
    public class UsersRequest
    {
        public string Name { get; set; }
        public string PersonalDocument { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RemovedAt { get; set; } = null;
        public List<AddressesRequest> Address { get; set; }
    }
}
