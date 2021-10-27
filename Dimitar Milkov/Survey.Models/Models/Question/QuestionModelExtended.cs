using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Survey.Data.Entities;

namespace Survey.Models.Models.Question
{
    public class QuestionModelExtended
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Text for the Question is required")]
        [MaxLength(500)]
        public string Text { get; set; }
        [Required(ErrorMessage = "A description for the Question is required")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        public virtual ICollection<Data.Entities.Option> Options { get; set; }
    }
}
