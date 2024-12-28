using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telhai.CS.Final.Client.Models
{
    public class Error
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; } = 0;

        [JsonPropertyName("Question_Description")]
        public string Question_Description { get; set; }
        [JsonPropertyName("Currect_Answer")]
        public string Currect_Answer { get; set; }
        [JsonPropertyName("Selected_Asnwer")]
        public string Selected_Asnwer { get; set; }


        public Error()
        {
            this.Question_Description = string.Empty;
            this.Currect_Answer = string.Empty;
            this.Selected_Asnwer = string.Empty;
        }

        public Error(Error error)
        {
            this.Id = error.Id;
            this.Question_Description = error.Question_Description;
            this.Currect_Answer = error.Currect_Answer;
            this.Selected_Asnwer = error.Selected_Asnwer;
        }

        public Error(string desc, string currect_ans, string selected)
        {
            this.Question_Description = desc;
            this.Currect_Answer = currect_ans;
            this.Selected_Asnwer = selected;
        }



    }
}
