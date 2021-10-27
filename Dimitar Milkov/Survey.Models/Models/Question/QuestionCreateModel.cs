using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Survey.Data.Entities;

namespace Survey.Models.Models.Question
{
    public class QuestionCreateModel
    {
        [Required(ErrorMessage = "Text for the Question is required")]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required(ErrorMessage = "A description for the Question is required")]
        [MaxLength(1000)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Text == Description)
            {
                yield return new ValidationResult("You cannot have the same text in both Text and Description");
            }
        }
    }
}
