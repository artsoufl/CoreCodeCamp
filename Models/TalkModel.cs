using CoreCodeCamp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreCodeCamp.Models
{
    public class TalkModel
    {
        public int TalkId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        [Range(100,500)]
        public int Level { get; set; }

        public SpeakerModel Speaker { get; set; }
    }
}
