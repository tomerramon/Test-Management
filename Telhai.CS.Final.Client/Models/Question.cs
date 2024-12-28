using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telhai.CS.Final.Client.Models
{
    public class Question : INotifyPropertyChanged
    {

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

        private string _description;
        [JsonPropertyName("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private ObservableCollection<Answer> _answers;
        [JsonPropertyName("Answers")]
        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set
            {
                _answers = value;
                OnPropertyChanged("Answers");
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

        private string _currectAnswer;
        [JsonPropertyName("CurrectAnswer")]
        public string CurrectAnswer
        {
            get { return _currectAnswer; }
            set
            {
                _currectAnswer = value;
                OnPropertyChanged("CurrectAnswer");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public Question()
        {
            Id = 0;
            Str_Id = Guid.NewGuid().ToString();
            Description = string.Empty;
            Answers = new ObservableCollection<Answer>();
            CurrectAnswer = string.Empty;
            IsRandom = false;
        }
        public Question(string description, ObservableCollection<Answer> answers, string currectAnswer, bool isRandom)
        {
            Id = 0;
            Str_Id = Guid.NewGuid().ToString();
            Description = description;
            Answers = new ObservableCollection<Answer>(answers);
            CurrectAnswer = currectAnswer;
            IsRandom = isRandom;
        }


        //return an answer with a giben id.
        public string GetAnswer(int i)
        {
            return Answers[i].Text;
        }

        //add new answer to the question.
        public void AddAaswer(Answer answer)
        {
            Answers.Append(answer);
        }

        public void UpdateAnswer(int insex, Answer answer)
        {
            Answers[insex] = answer;
        }



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
