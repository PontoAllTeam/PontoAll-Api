using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoAll.WebAPI.Objects.Dtos.Entities
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string BusinessPhone { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public int Number { get; set; }
        public int Status { get; set; }
    }
}
