using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telhai.CS.Final.Client.Models
{
    public class Answer
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Text")]
        public string Text { get; set; }
        [JsonPropertyName("IsCorrect")]
        public bool IsCorrect { get; set; }


        public Answer()
        {
            Id = 0;
            Text = string.Empty;
            IsCorrect = false;
        }

        public Answer(string text, bool isCorrect)
        {
            Id = 0;
            Text = text;
            IsCorrect = isCorrect;
        }

        public Answer(Answer answer)
        {
            Id = answer.Id;
            Text = answer.Text;
            IsCorrect = answer.IsCorrect;
        }
    }
}
