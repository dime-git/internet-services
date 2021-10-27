using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Survey.Models.Models.Option
{
    public class OptionModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int? Order { get; set; }
        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Data.Entities.Question Question { get; set; }
    }
}
