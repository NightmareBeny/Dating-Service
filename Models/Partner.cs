using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingService.Models
{
    public class Partner
    {
        [Key]
        [Display(Name = "Код партнёра")]
        [Required(ErrorMessage = "Укажите код партнёра")]
        public int CodePartner { get; set; }

        [Display(Name = "Логин клиента")]
        [StringLength(13, MinimumLength = 3)]
        [Required(ErrorMessage = "Введите логин клиента")]
        public string LoginClient { get; set; }

        [Display(Name = "Знак зодиака")]
        [StringLength(10)]
        public string Sign { get; set; }

        [Display(Name = "Пол")]
        //[Required(ErrorMessage = "Укажите пол")]
        public char? Gender { get; set; }

        [Display(Name = "Возраст")]
        //[Required(ErrorMessage = "Введите кол-во лет")]
        public int? Age { get; set; }

        [Display(Name = "Адрес")]
        //[Required(ErrorMessage = "Введите место, откуда должен быть ваш объект")]
        public string Adress { get; set; }

        [Display(Name = "Образование")]
        public string Education { get; set; }

        [Display(Name = "Рост в см")]
        public string Height { get; set; }

        [Display(Name = "Вес в кг")]
        public string Weight { get; set; }

        [Display(Name = "Есть ли дети?")]
        public bool? Children { get; set; }

        [Display(Name = "Интересы")]
        [DataType(DataType.MultilineText)]
        public string Hobbies { get; set; }
    }
}
