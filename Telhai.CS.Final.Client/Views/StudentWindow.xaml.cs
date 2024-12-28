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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;
using Telhai.CS.Final.Client.Models;

namespace Telhai.CS.Final.Client.Views
{

    public partial class StudentWindow : Window
    {
        HttpExamRepository repo;
        public StudentWindow(HttpExamRepository repo)
        {
            InitializeComponent();
            this.repo = repo;
            this.DataContext = repo; // binding to the exams list to show all the exams in the DB.
            lblText.Content = "Search for a test by name, select the right test in the list\n          and press 'start test' button to start the test.";
        }

        /// <summary>
        ///     when pressing the start exam button the window to enter the exam loads only if the user really selected an exam from the list.
        /// </summary>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBoxExams.SelectedItem is Exam ex) //checks that the use choose an exam from the list.
            {
                EnterExamWindow enterExamWindow = new EnterExamWindow(ex);//open the window to enter the exam.
                enterExamWindow.ShowDialog();
            }
        }


        /// <summary>
        ///  this manege the search for exam in the exams list.
        ///  every time user enter something to the search field this function activates and show only the exams that their names contains the entered sequence.
        /// </summary>
        private void FilterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var exams =  HttpExamRepository.Instance.Exams; //gets the exams list 
            if (this.FilterText.Text != string.Empty) //checks the the input in not empty.
            {
                this.listBoxExams.ItemsSource = exams.Where(e => e.Name.Contains(this.FilterText.Text, StringComparison.OrdinalIgnoreCase)); //get all the exams that contains the sequence entered in there names.
            }
            else
            {
                this.listBoxExams.ItemsSource = exams; //if the search bar is empty all the exams will be shown.
            }
        }

    }
}
