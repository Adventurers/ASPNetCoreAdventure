using System.ComponentModel.DataAnnotations;

namespace ASPNetCore.Localization.Models
{
    public class ContactViewModel
    {

        [Required]
        [MaxLength(50)]
        public string Emaill { get; set; }


        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }
    }
}
