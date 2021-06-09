using System;
using System.ComponentModel.DataAnnotations;

namespace DatingService.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Логин")]
        [StringLength(13, MinimumLength = 3)]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [StringLength(10, MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Display(Name = "ФИО")]
        [StringLength(30)]
        [Required(ErrorMessage = "Введите ФИО")]
        public string FIO { get; set; }

        [Display(Name = "Должность")]
        [StringLength(25)]
        [Required(ErrorMessage = "Введите вашу должность")]
        public string Post { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30)]
        public string Email { get; set; }

        [Display(Name = "День рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}"/*, ApplyFormatInEditMode = true*/)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Укажите ваш пол")]
        public char Gender { get; set; }

        [Display(Name = "Серия паспорта")]
        [StringLength(4)]
        [Required(ErrorMessage = "Введите серию паспорта")]
        public string Passportseries { get; set; }

        [Display(Name = "Номер паспорта")]
        [StringLength(6)]
        [Required(ErrorMessage = "Введите номер паспорта")]
        public string Passportnumber { get; set; }

        [Display(Name = "Адрес проживания")]
        [StringLength(50)]
        [Required(ErrorMessage = "Введите адрес проживания")]
        public string Adress { get; set; }

        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        [Required(ErrorMessage = "Введите телефон")]
        public string Telephon { get; set; }

        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
    }
}
