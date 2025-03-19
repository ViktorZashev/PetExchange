using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPresentationLayer.Models
{
    public class PetManage
    {
        [Required(ErrorMessage = "задължително")]
        [DisplayName("Име")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Добавен на")]
        public DateTime AddedOn { get; set; }

        [DisplayName("Осиновен на")]
        public DateTime? AdoptedOn { get; set; }

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Роден на")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Порода")]
        public string Breed { get; set; } = string.Empty;

        [DisplayName("Смени Снимка")]
        public IFormFile? Image { get; set; } = null;

        public string? PhotoPath { get; set; } = null;

        [DisplayName("Активност на профила")]
        public bool isActive { get; set; } = true;

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Тип домашен любимец")]
        public PetTypeEnum PetType { get; set; } = PetTypeEnum.Other;

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Пол")]
        public GenderEnum Gender { get; set; } = GenderEnum.Other;

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Описание")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "задължително")]
        [DisplayName("Включена клетка")]
        public bool IncludesCage { get; set; } = false;
        [DisplayName("Направени искания към животното")]
        public List<UserRequest> UserRequests { get; set; } = new();
    }
}
