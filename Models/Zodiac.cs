using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingService.Models
{
    public class Zodiac
    {
        [Key]
        [Display(Name = "Номер по п/п знака зодиака")]
        [Required]
        public string IDZodiac { get; set; }

        [Display(Name = "Название знака")]
        [StringLength(8)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Примечание")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

    }
}
