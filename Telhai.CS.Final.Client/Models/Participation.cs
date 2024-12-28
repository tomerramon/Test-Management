using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Telhai.CS.Final.Client.Models
{
    public class Participation
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
     
        [JsonPropertyName("Student_Id")]
        public int Student_Id { get; set; }
        
        [JsonPropertyName("Student_Name")]
        public string Student_Name { get; set; }
        
        [JsonPropertyName("Exam_Id")]
        public int Exam_Id { get; set; }
        
        [JsonPropertyName("Grade")]
        public double Grade { get; set; }
        
        [JsonPropertyName("Errors")]
        public List<Error> Errors { get; set; }


        public Participation()
        {
            this.Id = 0;
            this.Student_Id = 0;
            this.Student_Name = string.Empty;
            this.Exam_Id = 0;
            this.Grade = 0;
            this.Errors = new List<Error>();
        }

        public Participation(int id, int student_Id, string student_Name, int exam_Id, double grade, List<Error> errors)
        {
            Id = id;
            Student_Id = student_Id;
            Student_Name = student_Name;
            Exam_Id = exam_Id;
            Grade = grade;
            Errors = errors;
        }

        public Participation(Participation participation)
        {
            this.Id = participation.Id;
            this.Student_Id = participation.Student_Id;
            this.Student_Name = participation.Student_Name;
            this.Exam_Id = participation.Exam_Id;
            this.Grade = participation.Grade;
            this.Errors = participation.Errors;
        }

        public Participation(int student_Id, string strudent_Name, int exam_Id)
        {
            this.Id = 0;
            this.Student_Id = student_Id;
            this.Student_Name = strudent_Name;
            this.Exam_Id = exam_Id;
            this.Grade = 0;
            this.Errors = new List<Error>();
        }
    }
}
