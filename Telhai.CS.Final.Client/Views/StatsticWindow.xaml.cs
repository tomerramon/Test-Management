using System;
using System.Collections.Generic;
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
    public partial class StatsticWindow : Window
    {
        private Exam _exam;
        public StatsticWindow(Exam exam)
        {
            InitializeComponent();
            this._exam = exam;
            this.txtExamName.Text = _exam.Name; //show the exam name in the window top row.
            this.txtExamId.Text = _exam.Str_Id; // show the exam id in the textblock.
            this.txtExamAvg.Text = string.Format("{0:0.##}", GetAvg()); //shoiw the exam avg grage and format it to be 2 numbers after the dot (.xx)
            this.DataContext = _exam; //set the exam sent to the constructor the be the binded to the lists in the window,
        }

        /// <summary>
        ///    when ever the user press on differnt student in the students list 
        ///    this function pressent the studnets ditails in the currect fields.
        /// </summary>
        private void listBoxStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBoxStudents.SelectedItem is Participation p) 
            {
                this.txtStudentName.Text = p.Student_Name; //change the students name in the field.
                this.txtStudentID.Text = p.Student_Id.ToString(); //change the students id in the field.
                this.txtStudentGrade.Text =string.Format("{0:0.##}", p.Grade); //change the students Grade in the field.
                this.listBoxsErrors.ItemsSource = p.Errors; //change the error lst sourse to be the errors of the selected student.
                this.txtCurrectAns.Text = ""; // clear the answer field
                this.txtSelectedAns.Text = "";// clear the answer field
            }
        }

        /// <summary>
        ///     when the user change his selection in the errors list,
        ///     it assign the students selected anser and the currectt answer to the fields to be shown on the screen\window.
        /// </summary>
        private void listBoxsErrors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxsErrors.SelectedItem is Error err)
            {
                this.txtCurrectAns.Text = err.Currect_Answer;
                this.txtSelectedAns.Text = err.Selected_Asnwer;
            }
        }

        /// <summary>
        ///  claculate the AVG grade in that exam.
        ///  every question gets the same sccore.
        ///  it sums all the students grades in that exam and divide that number by the number of students took that exam.
        /// </summary>
        private double GetAvg()
        {
            double avg = 0.0;
            foreach(var user in _exam.Participations)
            {
                avg += user.Grade;
            }
            avg /= _exam.Participations.Count;
            return avg;
        }
    }
}
