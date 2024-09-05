using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class TranslatorSearchDto
    {
        public int TranslatorId { get; set; }
        public string TranslatorName { get; set; }
    }
}
