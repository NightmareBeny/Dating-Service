using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DatingService.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "Логин")]
        [StringLength(13, MinimumLength = 3)]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Display(Name = "Знак зодиака")]
        [StringLength(10)]
        public string Sign { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(10, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "ФИО")]
        [StringLength(30)]
        [Required(ErrorMessage = "Введите ФИО")]
        public string FIO { get; set; }

        [Display(Name = "Пол")]
        //[Required(ErrorMessage = "Укажите пол")]
        public char? Gender { get; set; }

        [Display(Name ="Фотография")]
        //[Required(ErrorMessage ="Выберите фотографию")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Введите откуда вы")]
        public string Adress { get; set; }

        [Display(Name ="Контакты")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Укажите способ связи с вами")]
        public string Contacts { get; set; }

        [Display(Name ="Образование")]
        public string Education { get; set; }

        [Display(Name = "Рост в см")]
        public  string Height { get; set; }

        [Display(Name = "Вес в кг")]
        public string Weight { get; set; }

        [Display(Name = "Есть ли у вас дети?")]
        public bool Children { get; set; }

        [Display(Name = "Интересы")]
        [DataType(DataType.MultilineText)]
        public string Interests { get; set; }

        [Display(Name = "Дополнительная информация")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
    }
}
