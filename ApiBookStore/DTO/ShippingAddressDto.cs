using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class ShippingAddressDto
    {
        public int ShippingAddressId { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Country { get; set; }

        public int UserId { get; set; }
    }
}
