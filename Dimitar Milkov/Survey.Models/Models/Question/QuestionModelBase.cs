using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Survey.Models.Models.Question
{
    public class QuestionModelBase
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Text for the Question is required")]
        [MaxLength(500)]
        public string Text { get; set; }
        [Required(ErrorMessage = "A description for the Question is required")]
        [MaxLength(1000)]
        public string Description { get; set; }
        
    }
}
