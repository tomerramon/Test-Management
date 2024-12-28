using System;
using System.CodeDom;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telhai.CS.Final.Client.Models;

namespace Telhai.CS.Final.Client.Views
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        private Exam _exam;
        private Participation _participation;
        private string[] _answers;
        private DispatcherTimer timer { get; set; }
        private TimeSpan remainingTime { get; set; }

        public ExamWindow(Exam exam, Participation participation)
        {
            InitializeComponent();
            this._exam = exam;
            this._participation = participation;
            var rnd = new Random();
            if (_exam.IsRandom)
            {
                _exam.Questions = new ObservableCollection<Question>(_exam.Questions.OrderBy(e => rnd.Next())); // orginaize the question in a Random arrangement 
            }
            foreach (var q in _exam.Questions)
            {
                if (q.IsRandom)
                {
                    q.Answers = new ObservableCollection<Answer>(q.Answers.OrderBy(e => rnd.Next()));// orginaize the answers in a Random arrangement 
                }
            }
            this._answers = new string[_exam.Questions.Count]; //create empty array for the users answers selected.
            listBoxQuestions.SelectedIndex = 0;
            this.txtTotalQuestion.Text = _exam.Questions.Count.ToString();
            this.txtNumAnswers.Text = "0";
            this.DataContext = _exam;
            this.Loaded += Load;
        }

        /// <summary>
        ///  when the exam is loaded this function start to run and starts the exam timer
        /// </summary>
        private void Load(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            remainingTime = TimeSpan.FromHours(_exam.TotalHours) + TimeSpan.FromMinutes(_exam.TotalMinutes); //calculate the total hours and minutes the teacher set to the test.
            timeLabel.Content = remainingTime.ToString(@"hh\:mm\:ss"); //format to show the timer in right way.
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick!;
            timer.Start();
        }

        /// <summary>
        ///     this function responsible for every clock tick.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1)); //get the remaining time left for the test.
            if (remainingTime < TimeSpan.Zero) //checks if the exams time is over and if it is the the exam is automaticly submited and the exam window is closed.
            {
                remainingTime = TimeSpan.Zero;
                timer.Stop();
                MessageBox.Show("Time is over we sorry, window is will be closed automatically","Exam time is over",MessageBoxButton.OK,MessageBoxImage.Warning);
                SubmitExam(); //auto submit the test.
                this.Close();
            }
            timeLabel.Content = remainingTime.ToString(@"hh\:mm\:ss");
        }


        /// <summary>
        ///     When ever the user change question.
        ///     the function checks if the user already selected an answer.
        ///     if he did selected, it search which the matcing answer and mark it to be selected in the answers listbox
        /// </summary>
        private void listBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxQuestions.SelectedItem is Question q)
            {
                this.listboxAnswers.ItemsSource = q.Answers; //change answer in the list according to the selected question.
                this.txtQuestion.Text = q.Description; //change the question text in the textbox.
                if (!string.IsNullOrEmpty(this._answers[listBoxQuestions.SelectedIndex])) //chaeck if the user already selected answer for that tquestion.
                {
                    for (int i = 0; i < q.Answers.Count; i++)
                    {
                        if (q.Answers[i].Text == this._answers[listBoxQuestions.SelectedIndex]) //checks every answer if it mtch the answers array.
                        {
                            this.listboxAnswers.SelectedIndex = i; //if the user selected already answer so it will be marked.
                            return;
                        }
                    }
                }
            }
        }


        /// <summary>
        ///     when the user press the next button the function checks that it is not the last question in the test.
        /// </summary>
        private void btnNextQ_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxQuestions.SelectedIndex == _exam.Questions.Count - 1) //if the current question is the last question show message indicate that to the user.
            {
                string msg = $"This is the last question of the test.\nIf you are done press the SUBMIT button.";
                MessageBox.Show(msg, "Last Question", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int nextIndex = listBoxQuestions.SelectedIndex + 1;
            listBoxQuestions.SelectedIndex += 1; //if there is nextt question change the selected question to bw the next in line.
        }


        /// <summary>
        ///     
        /// </summary>
        private void btnResetQ_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this._answers[this.listBoxQuestions.SelectedIndex])) //check that the user selected answer for that question.
            {
                this.txtNumAnswers.Text = (int.Parse(txtNumAnswers.Text) - 1).ToString(); //Subtracts one from the counter for the total questions answered.
                listboxAnswers.UnselectAll(); //unmark in the answer listbox  the selection.
                this._answers[this.listBoxQuestions.SelectedIndex] = string.Empty; //set in the user answers list that no answer selected for that question.
                ListBoxItem? item = listBoxQuestions.ItemContainerGenerator.ContainerFromItem(listBoxQuestions.SelectedItem) as ListBoxItem;
                if (item != null)
                {
                    item.Background = new SolidColorBrush(Colors.WhiteSmoke);// changes that question background original to indicate that the question has no answer yet.

                }
            }
        }


        /// <summary>
        ///     when the user press the previous button the function checks that it is not the first question in the test.
        /// </summary>
        private void btnPrevQ_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxQuestions.SelectedIndex == 0) //if the current question is the first question, show message indicate that to the user.
            {
                string msg = $"This is the First question of the test.\nNo Previous question to show.";
                MessageBox.Show(msg, "First Question", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int prevIndex = listBoxQuestions.SelectedIndex - 1;
            listBoxQuestions.SelectedIndex -= 1;//if there is prev question change the selected question to be the prev in line. 
        }
        

        /// <summary>
        ///     when the user press the end test button.
        ///     it checks if all question got answered and if not a message is shown to the user say that no all questions answered.
        ///     else it just ask the user to confirm that he ended the exam.
        ///     at the ent the submit exam function is activated to send the answwrs to the serveer.
        /// </summary>
        private void btnEndTest_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            if (int.Parse(txtNumAnswers.Text) != _exam.Questions.Count) //checks if all question have an answer.
            {
                msg = $"Not all questions were answered.\nIf you want to continue without a full answer, click OK.\nIf you want to go back and answer the rest of the questions click Cancel.";
                var result = MessageBox.Show(msg, "Submit Exam", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK) // if the user still wwant to ccontinue to submit/
                {
                    SubmitExam();
                    return;
                }
            }
            else //verify with the user that he is sure he want to finish and submit the exam.
            {
                msg = $"Are you sure you want to finish the exam ?";
                var result = MessageBox.Show(msg, "Submit Exam", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    SubmitExam();
                    return;
                }
            }
        }


        /// <summary>
        /// This function is responsible for all test submission operations.
        /// This includes calculating the student's grade and building a participation object that will be saved in the exam and in the participation table in the DB.
        /// </summary>
        public async void SubmitExam()
        {
            int numQ = _exam.Questions.Count;
            double qScore = 100.0 / numQ; //calculate the score for each question.
            for (int i = 0; i < numQ; i++)
            {
                if (_answers[i] == _exam.Questions[i].CurrectAnswer) //check if the student selected the ccurrect answer.
                {
                    _participation.Grade += qScore; // add the question score to the total grade.
                }
                else
                //if the student selected wrong answer the an error object is built and add to the error list for that participation.
                {
                    _participation.Errors.Add(new Error(_exam.Questions[i].Description, _exam.Questions[i].CurrectAnswer, _answers[i])); 
                }
            }

            var exm = HttpExamRepository.Instance.Exams.FirstOrDefault(e => e.Id == _exam.Id); //find the exam in the DB.
            if (exm != null) //check that the exam exists.
            {
                await HttpExamRepository.Instance.AddParticipationAsync(this._exam.Id, _participation); //add to the DB participation table the new participation created.
            }
            this._exam.Participations.Add(_participation); //add to the exams participation list the new participation created.

            this.Close();

        }


        /// <summary>
        ///     When ever the user change answer.
        ///     the function checks if the user already selected an answer.
        ///     if he did not selecte answer, then the total question that got answered counter get +1
        ///     and the question backgroung in the question listbox will be change to indicate that the question has an answer seleccted.
        ///     and the answer is selectted is also saved in the answers list for memorization.  
        /// </summary>
        private void listboxAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.listboxAnswers.SelectedValue is Answer answer)//checks if an answer is selected.
            {
                if (string.IsNullOrEmpty(this._answers[this.listBoxQuestions.SelectedIndex])) //check if the user already selected an answer for this question.
                {
                    this.txtNumAnswers.Text = (int.Parse(txtNumAnswers.Text) + 1).ToString(); //increase the total question answered by one.
                    ListBoxItem? item = listBoxQuestions.ItemContainerGenerator.ContainerFromItem(listBoxQuestions.SelectedItem) as ListBoxItem;
                    if (item != null)
                    {
                        item.Background = new SolidColorBrush(Color.FromRgb(110, 189, 250)); //change the questions background color.
                    }

                }
                this._answers[this.listBoxQuestions.SelectedIndex] = answer.Text; //mark in the answers list the selected answer.
            }
        }
    }
}
