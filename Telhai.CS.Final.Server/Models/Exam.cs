using MessagePack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Telhai.CS.Final.Server.Models
{
    public class Exam : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id = 0;
        [JsonPropertyName("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        private string _str_Id;
        [JsonPropertyName("Str_Id")]
        public string Str_Id
        {
            get { return _str_Id; }
            set
            {
                _str_Id = value;
                OnPropertyChanged("Str_Id");
            }
        }

        private string _name;
        [JsonPropertyName("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private DateTime? _date;
        [JsonPropertyName("Date")]
        public DateTime? Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");

            }
        }

        private string _teacherFName;
        [JsonPropertyName("TeacherFName")]
        public string TeacherFName
        {
            get { return _teacherFName; }
            set
            {
                _teacherFName = value;
                OnPropertyChanged("TeacherFName");
            }
        }

        private string _teacherLName;
        [JsonPropertyName("TeacherLNmae")]
        public string TeacherLNmae
        {
            get { return _teacherLName; }
            set
            {
                _teacherLName = value;
                OnPropertyChanged("TeacherLNmae");
            }
        }

        private int _totalHour;
        [JsonPropertyName("TotalHours")]
        public int TotalHours
        {
            get { return _totalHour; }
            set
            {
                _totalHour = value;
                OnPropertyChanged("TotalHours");
            }
        }

        private int _totalMinutes;
        [JsonPropertyName("TotalMinutes")]
        public int TotalMinutes
        {
            get { return _totalMinutes; }
            set
            {
                _totalMinutes = value;
                OnPropertyChanged("TotalMinutes");
            }
        }

        private bool _isRandom;
        [JsonPropertyName("IsRandom")]
        public bool IsRandom
        {
            get { return _isRandom; }
            set
            {
                _isRandom = value;
                OnPropertyChanged("IsRandom");
            }
        }

        private ObservableCollection<Question> _questions;
        [JsonPropertyName("Questions")]
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }

        private ObservableCollection<Participation> _participations;
        [JsonPropertyName("Participations")]
        public ObservableCollection<Participation> Participations
        {
            get { return _participations; }
            set
            {
                _participations = value;
                OnPropertyChanged("Participations");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Exam()
        {
            Str_Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            Date = DateTime.Now;
            TeacherFName = string.Empty;
            TeacherLNmae = string.Empty;
            TotalHours = 0;
            TotalMinutes = 0;
            IsRandom = false;
            Questions = new ObservableCollection<Question>();
            Participations = new ObservableCollection<Participation>();
        }

        public Exam(Exam exam)
        {
            Str_Id = exam.Str_Id;
            Name = exam.Name;
            Date = exam.Date;
            TeacherFName = exam.TeacherFName;
            TeacherLNmae = exam.TeacherLNmae;
            TotalHours = exam.TotalHours;
            TotalMinutes = exam.TotalMinutes;
            IsRandom = exam.IsRandom;
            Questions = exam.Questions;
            Participations = exam.Participations;
        }

        public Exam(string str_Id, string name, DateTime? date, string teacherFName, string teacherLNmae, int totalHours, int totalMinutes, bool isRandom, ObservableCollection<Question> questions, ObservableCollection<Participation> participations)
        {
            Str_Id = str_Id;
            Name = name;
            Date = date;
            TeacherFName = teacherFName;
            TeacherLNmae = teacherLNmae;
            TotalHours = totalHours;
            TotalMinutes = totalMinutes;
            IsRandom = isRandom;
            Questions = questions;
            Participations = participations;
        }

        public void RemoveQuestion(string id)
        {
            int indexFound = this._questions.ToList().FindIndex(e => e.Str_Id == id);
            if (indexFound >= 0)
            {
                this._questions.RemoveAt(indexFound);
            }
        }

    }
}
