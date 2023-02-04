using System.ComponentModel.DataAnnotations;

namespace ERP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}