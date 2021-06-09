using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DatingService.Models
{
    public class Couple
    {
        [Key]
        [Display(Name = "Номер пары")]
        [Required(ErrorMessage = "Введите номер пары")]
        public int IDCouple { get; set; }

        [Display(Name = "Клиент")]
        [StringLength(13, MinimumLength = 3)]
        [Required(ErrorMessage = "Укажите клиента")]
        public string LoginClient { get; set; }

        [Display(Name = "Партнёр")]
        [StringLength(13, MinimumLength = 3)]
        [Required(ErrorMessage = "Укажите партнёра")]
        public string LoginPartner { get; set; }

        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

    }
}
