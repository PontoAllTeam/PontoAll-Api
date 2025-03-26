using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Dtos.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string RecoveryEmail { get; set; }
        public string Registration { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
    }
}
