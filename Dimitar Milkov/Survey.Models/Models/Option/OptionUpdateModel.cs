using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Survey.Models.Models.Option
{
    public class OptionUpdateModel
    {
        [Required(ErrorMessage = "An option ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Text for the option is required")]
        public string Text { get; set; }
        public int? Order { get; set; }
        [Required(ErrorMessage = "A corresponding question is required")]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Data.Entities.Question Question { get; set; }
    }
}
