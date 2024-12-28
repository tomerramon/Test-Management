using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
    /// Interaction logic for EnterExamWindow.xaml
    /// </summary>
    public partial class EnterExamWindow : Window
    {
        private Exam _exam;
        public EnterExamWindow(Exam exam)
        {
            _exam = exam;
            InitializeComponent();
            this.txtTitle.Content = exam.Name;
        }


        /// <summary>
        ///  start exan button, valudate the time and the students details and if everything is right the student redirect to the exams window
        /// </summary>
        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            bool isTimeOk = ValidateTime(); //vallidate that the students entering the exam on the right time.
            bool isFeildsOk = IsValidFields(); //validate that the students really enter his details to the right fields.
            if (isTimeOk & isFeildsOk)
            {
                string studentName = $"{this.txtFirstN.Text} {this.txtLastN.Text}"; 
                Participation participation = new Participation(int.Parse(this.txtID.Text), studentName, _exam.Id); // create new participation object with the students name, id and the exams id.
                ExamWindow examWindow = new ExamWindow(_exam,participation); //open the exam window to start his test.
                examWindow.ShowDialog();
                this.Close();
            }

        }

        /// <summary>
        ///     checks the starting exam time and the time the student press the start test.
        ///     if the student tries to enter to soon message box is pop up and the window is closed.
        ///     if he tries after the staring time, the test window will open.
        /// </summary>
        private bool ValidateTime()
        {

            DateTime date = (DateTime)_exam.Date!;
            DateTime examDateStart = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second); //crate date object for the starting time.
            DateTime examDateEnd = examDateStart.AddMinutes(_exam.TotalMinutes).AddHours(_exam.TotalHours); //crate date object for the ending time.
            DateTime now = DateTime.Now; //gets the current time.

            if (DateTime.Compare(now, examDateStart) < 0) // student entered the exam too Early, show message .
            {
                string msg = $" Exan not started yet\n Wait until {_exam.Date.ToString()} and try again.";
                MessageBox.Show(msg, "Exan not started yet", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (DateTime.Compare(examDateEnd, now) < 0) // student enter the exam after the time is over.
            {
                string msg = $" You are to late...\n The Exam Time is OVER....\nExam started at : {_exam.Date.ToString()}";
                MessageBox.Show(msg, "Exam ended", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        ///  this function valitade the user input,
        ///  it create a message with all the wrong input entered and at the end show every invalid input in a messageBox.
        /// </summary>
        private bool IsValidFields()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(this.txtID.Text)) //checks the students Id is not empty.
            {
                msg += "Id number must be entered.\n"; // add the invalid message.
            }
            if (string.IsNullOrEmpty(this.txtFirstN.Text)) //checks the students first name is not empty
            {
                msg += "Student first name must be entered.\n";// add the invalid message.
            }
            if (string.IsNullOrEmpty(this.txtLastN.Text)) //checks the students last name is not empty
            {
                msg += "Student last name must be entered.\n";// add the invalid message.
            }
            if(!string.IsNullOrEmpty(msg)) // if the message is not empty => there was some invalid input. so is show the message to the user.
            {
                MessageBox.Show(msg, "Invalid students information", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        /// <summary>
        ///     this function activates when the user leave the id input box.
        ///     and te function validate that the input is a possitive number.
        /// </summary>
        private void txtID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                return;
            }
            if (this.txtID.Text.Count() != 9 ) //check the id doesnt have more the 9 characters.
            {
                string msg = "Invalid ID number length.";
                MessageBox.Show(msg, "Wrong Id Number", MessageBoxButton.OK, MessageBoxImage.Error);
                this.txtID.Clear();
                return;
            }
            int id_num;
            bool isNum = int.TryParse(this.txtID.Text, out id_num); // try to parse the id input to a number.
            if (!isNum | id_num <= 0) // if the input isnt number or a negative number a message indicated on that is show to the user.
            {
                string msg = "Id number must contain only positive numbers.";
                MessageBox.Show(msg, "Wrong Id Number", MessageBoxButton.OK, MessageBoxImage.Error);
                this.txtID.Clear();
                return;
            }
        }
    }
}




