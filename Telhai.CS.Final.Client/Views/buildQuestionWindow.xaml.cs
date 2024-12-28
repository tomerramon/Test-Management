using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telhai.CS.Final.Client.Models;

namespace Telhai.CS.Final.Client.Views
{
    /// <summary>
    /// Interaction logic for buildQuestionWindow.xaml
    /// </summary>
    public partial class buildQuestionWindow : Window
    {
        private Exam Exam;
        private Question _question;
        public ObservableCollection<Answer> Answers { get; set; }

        public buildQuestionWindow(Exam exam)
        {
            this.Exam = exam;
            _question = new Question();
            Answers = _question.Answers;
            InitializeComponent();
            this.DataContext = this;
        }

        public buildQuestionWindow(Exam exam, Question question)
        {
            this.Exam = exam;
            this._question = question;
            InitializeComponent();
            this.txtQuesDesciption.Text = question.Description;
            Answers = _question.Answers;
            this.DataContext = this;

        }

        /// <summary>
        ///     add answer button.
        ///     it add the new answer to the answer list box, only if the textbox is not empty.
        /// </summary>
        private void btnAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtAnswer.Text != "")
            {
                this.Answers.Add(new Answer(this.txtAnswer.Text, false));
                this.txtAnswer.Clear();
                this.txtAnswer.Focus();
            }
        }

        /// <summary>
        ///     cancel button,
        ///     this button clise the window witthout saving the work.
        /// </summary>
        private void btnCancelQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        /// <summary>
        /// checks that the user entered a question and at list 2 answers.
        /// </summary>
        private bool ValitadeInfo()
        {
            if (string.IsNullOrEmpty(this.txtQuesDesciption.Text))// cchecks that the user entered a quetion text.
            {
                return false;
            }
            else if (this.lstAnswers.Items.Count < 2) //ccheck that the user entered at least 2 answers.
            {
                return false;
            }
            return true;
        }


        /// <summary>
        ///     added new question obect to the exam and to the question table in the DB
        /// </summary>
        private async void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (!ValitadeInfo())//valitad the fields to create new question.
            {
                MessageBox.Show("Failed to add new question.\nPlease check you entered question description and at least 2 answers.");
                return;
            }
            if (lstAnswers.SelectedIndex < 0) //validate that the user seleccted the currect answer for the new question.
            {
                MessageBox.Show("No correct answer was selected.\nPlease select the correct answer from the list and try again");
                return;
            }
            if (lstAnswers.SelectedItem is Answer currect)
            {
                foreach (var ans in this.Answers)
                    //loop over all the answers inserted and mark the selected answer to be the currect answer.
                {
                    if (ans.Text == currect.Text)
                    {
                        ans.IsCorrect = true;
                        break;
                    }
                }
                var exm = HttpExamRepository.Instance.Exams.FirstOrDefault(e => e.Id == Exam.Id); //get the right exam object from the DB.
                Question question = new Question(txtQuesDesciption.Text, this.Answers, currect.Text ,this.btnRandom.IsChecked.Value); //built new question objecct from the information inserted in the fields and with answers.
                if (exm != null)
                {
                    await HttpExamRepository.Instance.AddQuestionAsync(this.Exam.Id, question); //adds new question object to the question table in the DB
                }
                this.Exam.Questions.Add(question); //added the new question to the local exam object to be shown in the prev window.

                this.Close();
            }
        }
    }
}
