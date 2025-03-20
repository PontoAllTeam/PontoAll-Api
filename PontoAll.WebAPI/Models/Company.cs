using PontoAll.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PontoAll.WebAPI.Models
{
    [Table("company")]
    public class Company
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("corporatename")]
        public string CorporateName { get; set; }

        [Column("fantasyname")]
        public string FantasyName { get; set; }

        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("businessphone")]
        public string BusinessPhone { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("cep")]
        public string Cep { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("neighborhood")]
        public string Neighborhood { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("status")]
        public CompanyStatus Status { get; set; }

        public Company () 
        {

        }

        public Company(int id, string corporateName, string fantasyname, string cnpj, string email, string businessphone, string state, string city, string cep, string street, string neighborhood, int number, CompanyStatus status)
        {
            Id = id;
            CorporateName = corporateName;
            FantasyName = fantasyname;
            Cnpj = cnpj;
            Email = email;
            BusinessPhone = businessphone;
            State = state;
            City = city;
            Cep = cep;
            Street = street;
            Neighborhood = neighborhood;
            Number = number;
            Status = status;    
        }
    }
}
