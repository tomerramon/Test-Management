using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telhai.CS.Final.Client.Models;

namespace Telhai.CS.Final.Client.Views
{
    public partial class BuildExamWindow : Window
    {
        public Exam Exam { get; set; }

        HttpExamRepository repo;

        /// <summary>
        ///     conatuctor initalize all the fields empty exam object.
        /// </summary>
        public BuildExamWindow(HttpExamRepository repo)
        {
            InitializeComponent();
            this.repo = repo;
            this.Exam = new Exam();
            this.txtExamId.Text = Exam.Str_Id;
            this.txtExamName.Text = Exam.Name;
            this.dateExam.SelectedDate = Exam.Date;
            this.txtTeacherFName.Text = Exam.TeacherFName;
            this.txtTeacherLName.Text = Exam.TeacherLNmae;
            this.txtHourStart.Text = Exam.Date!.Value.Hour.ToString();
            this.txtHourTotal.Text = Exam.TotalHours.ToString();
            this.txtMinutesStart.Text = Exam.Date.Value.Minute.ToString();
            this.txtMinutesTotal.Text = Exam.TotalMinutes.ToString();
            this.DataContext = Exam;
        }


        /// <summary>
        ///     conatuctor initalize all the fields with a given exam object
        /// </summary>
        public BuildExamWindow(HttpExamRepository repo, Exam exam)
        {
            InitializeComponent();
            this.repo = repo;
            this.Exam = exam;
            this.Exam.Id = exam.Id;
            this.txtExamId.Text = exam.Str_Id;
            this.txtExamName.Text = exam.Name;
            this.dateExam.SelectedDate = exam.Date;
            this.txtTeacherFName.Text = exam.TeacherFName;
            this.txtTeacherLName.Text = exam.TeacherLNmae;
            this.txtHourStart.Text = exam.Date!.Value.Hour.ToString();
            this.txtHourTotal.Text = exam.TotalHours.ToString();
            this.txtMinutesStart.Text = exam.Date.Value.Minute.ToString();
            this.txtMinutesTotal.Text = exam.TotalMinutes.ToString();
            this.btnRandom.IsChecked = exam.IsRandom;
            this.DataContext = Exam;

        }


        /// <summary>
        ///     cancel button, close the window when pressed.
        /// </summary>
        private void btnCancelExam_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        ///     save exam butoon.
        ///     this function activate when user press the save button, this will save the exam to a local folder.
        /// </summary>
        private void btSaveExam_Click(object sender, RoutedEventArgs e)
        {
            if (repo.Exams.FirstOrDefault(e => e.Id == Exam.Id) != null) // checks that the exam is not in the DB yet, if it is a message show to the user
            {
                string msg = "Can not save Exam.\nExam is already exists to database, try updating the existing exam insted.";
                MessageBox.Show(msg, "Exam is in db.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ValidateExamInfo()) //validate the inputs and assign then to the exam object to be saved.
            {
                this.Exam.Name = txtExamName.Text;
                this.Exam.Date = new DateTime(dateExam.SelectedDate!.Value.Year, dateExam.SelectedDate.Value.Month, dateExam.SelectedDate.Value.Day, int.Parse(txtHourStart.Text), int.Parse(txtMinutesStart.Text), 0);
                this.Exam.TeacherFName = txtTeacherFName.Text;
                this.Exam.TeacherLNmae = txtTeacherLName.Text;
                this.Exam.TotalHours = int.Parse(txtHourTotal.Text);
                this.Exam.TotalMinutes = int.Parse(txtMinutesTotal.Text);
                this.Exam.IsRandom = btnRandom.IsChecked.Value;

                repo.SaveExam(Exam); //saves the exam to the local folder.
                this.Close();
            }
            return;
        }

        /// <summary>
        ///     add exam butoon.
        ///     this function activate when user press the addExam button, this will add the exam to the local exam list to be shown in the teacher local exam list.
        /// </summary>
        private void btnAddExam_Click(object sender, RoutedEventArgs e)
        {
            if (repo.Exams.FirstOrDefault(e => e.Id == Exam.Id) != null) // checks that the exam is not in the DB yet, if it is a message show to the user
            {
                string msg = "Can not add Exam.\nExam is already exists to database, try updating the existing exam insted or to create new one.";
                MessageBox.Show(msg, "Exam is already exists.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ValidateExamInfo()) //validate the inputs and assign then to the exam object to be added to the exam list.
            {
                this.Exam.Name = txtExamName.Text;
                this.Exam.Date = new DateTime(dateExam.SelectedDate!.Value.Year, dateExam.SelectedDate.Value.Month, dateExam.SelectedDate.Value.Day, int.Parse(txtHourStart.Text), int.Parse(txtMinutesStart.Text), 0);
                this.Exam.TeacherFName = txtTeacherFName.Text;
                this.Exam.TeacherLNmae = txtTeacherLName.Text;
                this.Exam.TotalHours = int.Parse(txtHourTotal.Text);
                this.Exam.TotalMinutes = int.Parse(txtMinutesTotal.Text);
                this.Exam.IsRandom = btnRandom.IsChecked.Value;
                this.Close();
            }
            return;
        }

        /// <summary>
        ///     update exam butoon.
        ///     this function activate when user press the save button, this will save the exam to a local folder.
        /// </summary>
        private void btnUpdateExam_Click(object sender, RoutedEventArgs e)
        {
            if (repo.Exams.FirstOrDefault(ex => ex.Str_Id == Exam.Str_Id) == null) // checks that the exam is in the DB , if not  it is a message show to the user
            {
                string msg = "Can not update Exam.\nExam is not exists in database yet, try adding it or save to local file insted.";
                MessageBox.Show(msg, "Exam is in db.", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ValidateExamInfo()) //validate the inputs and assign then to the exam object to be updated in the DB.
            {
                this.Exam.Name = txtExamName.Text;
                this.Exam.Date = new DateTime(dateExam.SelectedDate!.Value.Year, dateExam.SelectedDate.Value.Month, dateExam.SelectedDate.Value.Day, int.Parse(txtHourStart.Text), int.Parse(txtMinutesStart.Text), 0);
                this.Exam.TeacherFName = txtTeacherFName.Text;
                this.Exam.TeacherLNmae = txtTeacherLName.Text;
                this.Exam.TotalHours = int.Parse(txtHourTotal.Text);
                this.Exam.TotalMinutes = int.Parse(txtMinutesTotal.Text);
                this.Exam.IsRandom = btnRandom.IsChecked.Value;
            }
            var res = repo.UpdateExamAsync(Exam); // send the updated exam object to the server to be updated.
            if (res != null)
            {
                this.Close();
            }
        }


        /// <summary>
        ///     this function changes the ubnderline of the exam name field to be read if it is empty.
        ///     if not empty change back to blue.
        /// </summary>
        private void txtExamName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtExamName.Text))
            {
                txtExamName.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtExamName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5A6996"));
            }
        }


        /// <summary>
        ///    this function is being activated every time the starting time of the tesst is being changed.
        ///    the function also validate that the values are correct:
        ///         number between 0 - 23.
        /// </summary>
        private void txtHourStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            int convertedHour;
            bool isOk = int.TryParse(txtHourStart.Text, out convertedHour); //try to convert the hour fron string to integer.
            if (!isOk & txtHourStart.Text != string.Empty) //validate that the number is not empty and that is integer
            {
                MessageBox.Show($"Hour: '{txtHourStart.Text}', must be an integer between 00 - 23.");
                txtHourStart.BorderBrush = new SolidColorBrush(Colors.Red);
                txtHourStart.Text = string.Empty;
            }
            else if (convertedHour > 23 | convertedHour < 0) //validate that the number is in the range of 0 -23.
            {
                MessageBox.Show($"Hour: '{txtHourStart.Text}', must be an integer between 00 - 23.");
                txtHourStart.BorderBrush = new SolidColorBrush(Colors.Red);
                txtHourStart.Text = string.Empty;

            }
            if (string.IsNullOrEmpty(txtHourStart.Text)) // if the field is empty set the under line to be red to represent that the field in missing.
            {
                txtHourStart.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtHourStart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5A6996"));
            }
        }


        /// <summary>
        ///    this function is being activated every time the starting time of the test is being changed.
        ///    the function also validate that the minutes value is correct:
        ///         number between 0 -59.
        /// </summary>
        private void txtMinutesStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            int convertedMinute;
            bool isOk = int.TryParse(txtMinutesStart.Text, out convertedMinute);//try to convert the minits from string to integer.
            if (!isOk & txtMinutesStart.Text != string.Empty)//validate that the number is not empty and that is integer
            {
                MessageBox.Show($"Minutes: '{txtMinutesStart.Text}', must be an integer between 0 - 59.");
                txtMinutesStart.BorderBrush = new SolidColorBrush(Colors.Red);
                txtMinutesStart.Text = string.Empty;
            }
            else if (convertedMinute > 59 | convertedMinute < 0) //validate that the number is in the range of 0 - 59
            {
                MessageBox.Show($"Minutes: '{txtMinutesStart.Text}', must be an integer between 0 - 59.");
                txtMinutesStart.BorderBrush = new SolidColorBrush(Colors.Red);
                txtMinutesStart.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtMinutesStart.Text))// if the field is empty set the under line to be red to represent that the field in missing.
            {
                txtMinutesStart.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtMinutesStart.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5A6996"));
            }

        }

        /// <summary>
        ///    this function is being activated every time the starting time of the tesst is being changed.
        ///    the function also validate that the values are correct:
        ///         number between 0 - 23.
        /// </summary>
        private void txtHourTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int convertedHour;
            bool isOk = int.TryParse(txtHourTotal.Text, out convertedHour);//try to convert the hour fron string to integer.
            if (!isOk & txtHourTotal.Text != string.Empty)//validate that the number is not empty and that is integer
            {
                MessageBox.Show($"Hour: '{txtHourTotal.Text}', must be an integer between 00 - 23.");
                txtHourTotal.BorderBrush = new SolidColorBrush(Colors.Red);
                txtHourTotal.Text = string.Empty;
            }
            else if (convertedHour > 23 | convertedHour < 0)//validate that the number is in the range of 0 -23.
            {
                MessageBox.Show($"Hour: '{txtHourTotal.Text}', must be an integer between 00 - 23.");
                txtHourTotal.BorderBrush = new SolidColorBrush(Colors.Red);
                txtHourTotal.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtHourTotal.Text))// if the field is empty set the under line to be red to represent that the field in missing.
            {
                txtHourTotal.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtHourTotal.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5A6996"));
            }
        }

        /// <summary>
        ///    this function is being activated every time the starting time of the test is being changed.
        ///    the function also validate that the minutes value is correct:
        ///         number between 0 -59.
        /// </summary>
        private void txtMinutesTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            int convertedMinute;
            bool isOk = int.TryParse(txtMinutesTotal.Text, out convertedMinute);//try to convert the minits from string to integer.
            if (!isOk & txtMinutesTotal.Text != string.Empty)//validate that the number is not empty and that is integer
            {
                MessageBox.Show($"Minutes: '{txtMinutesTotal.Text}', must be an integer between 0 - 59.");
                txtMinutesTotal.BorderBrush = new SolidColorBrush(Colors.Red);
                txtMinutesTotal.Text = string.Empty;
            }
            else if (convertedMinute > 59 | convertedMinute < 0)//validate that the number is in the range of 0 - 59.
            {
                MessageBox.Show($"Minutes: '{txtMinutesTotal.Text}', must be an integer between 0 - 59.");
                txtMinutesTotal.BorderBrush = new SolidColorBrush(Colors.Red);
                txtMinutesTotal.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtMinutesTotal.Text))// if the field is empty set the under line to be red to represent that the field in missing.
            {
                txtMinutesTotal.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtMinutesTotal.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5A6996"));
            }
        }


        /// <summary>
        ///  checks all the exam field required to create a new exam object.
        /// </summary>
        private bool ValidateExamInfo()
        {
            //checks the the exan name, teachers first name and teacher last name is not empty. if one of then is empty a message shown to the user.
            if (string.IsNullOrEmpty(txtExamName.Text) | string.IsNullOrEmpty(txtTeacherFName.Text) | string.IsNullOrEmpty(txtTeacherLName.Text))
            {
                MessageBox.Show("Please check the tests name or the teachers name.");
                return false;
            }

            //checks the the exams starting hour is not empty and that he is a number. if not a message shown to the user.
            else if (string.IsNullOrEmpty(txtHourStart.Text) | int.Parse(txtHourStart.Text) < 0 | int.Parse(txtHourStart.Text) > 23)
            {
                MessageBox.Show("Please check the starting time of the test");
                return false;
            }

            //checks the the exams starting minutes is not empty and that he is a number. if not a message shown to the user.
            else if (string.IsNullOrEmpty(txtMinutesStart.Text) | int.Parse(txtMinutesStart.Text) < 0 | int.Parse(txtMinutesStart.Text) > 59)
            {
                MessageBox.Show("Please check the starting time of the test");
                return false;
            }

            //checks the the exams total hour is not empty and that he is a number. if not a message shown to the user.
            else if (string.IsNullOrEmpty(txtHourTotal.Text) | int.Parse(txtHourTotal.Text) < 0 | int.Parse(txtHourTotal.Text) > 23)
            {
                MessageBox.Show("Please check the total test time");
                return false;
            }

            //checks the the exams total minutes is not empty and that he is a number. if not a message shown to the user.
            else if (string.IsNullOrEmpty(txtMinutesTotal.Text) | int.Parse(txtMinutesTotal.Text) < 0 | int.Parse(txtMinutesTotal.Text) > 59)
            {
                MessageBox.Show("Please check the total test time");
                return false;
            }
            else
                return true;

        }

        /// <summary>
        ///     Delete the selected question from the exams question list and then uptade the exam in the Repository\DB.
        /// </summary>
        private async void btnDelQ_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxQuestions.SelectedItem is Question q) //work only if the user really selected a question fron the questions list in the window.
            {
                if (HttpExamRepository.Instance.Exams.FirstOrDefault(e => e.Id == Exam.Id) != null) // cheack if there an exam with that id in the DB
                {
                    await HttpExamRepository.Instance.RemoveQuestionAsync(Exam, q); // removes the questions fron the DB.
                    Exam = await HttpExamRepository.Instance.GetExamAsync(Exam.Id); // get from the Db the exam with that id.
                    this.Exam.Questions.Remove(q); // also remove the question fron the local exam found.
                    this.listBoxQuestions.ItemsSource = Exam.Questions; //updated the questions list in the window.
                }
                else
                {
                    this.Exam.Questions.Remove(q); //if the exam not in thr DB (he saved loclly) remove the question from the local exam object.
                }
            }
        }
        

        /// <summary>
        ///  when presing the add question button it opens the window for buildinbg new question.
        /// </summary>
        private void btnAddQ_Click(object sender, RoutedEventArgs e)
        {
            buildQuestionWindow buildQuestionWindow = new buildQuestionWindow(Exam);
            buildQuestionWindow.ShowDialog();

        }
    }
}
