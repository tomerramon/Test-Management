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
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public ObservableCollection<Exam> LocalExams { get; set; } = new ObservableCollection<Exam>(HttpExamRepository.Instance.LoadExams());
        public TeacherWindow()
        {
            InitializeComponent();
            this.DataContext = HttpExamRepository.Instance;
            this.listBoxLocalExams.ItemsSource = LocalExams;
        }


        /// <summary>
        ///     open the built test window and create new local exam.
        /// </summary>
        private void btnAddtest_Click(object sender, RoutedEventArgs e)
        {
            BuildExamWindow buildExamWindow = new BuildExamWindow(HttpExamRepository.Instance);  
            buildExamWindow.ShowDialog(); //open the build exam window
            var ex = buildExamWindow.Exam; //read the new exam built in the "buildExamWindow".
            if (ex.Id != 0 | ex.Name != string.Empty) //check if the new exam really created.
            {
                this.LocalExams.Add(ex); //assign the new exam built to the local exam list to be shown in the local exams list box.
                HttpExamRepository.Instance.SaveExam(ex); //save the new exam to the local exams folder.
            }
        }


        /// <summary>
        ///     opens the build exam window with the selected exam fron the list box.
        ///     the build exam window updates the given exam and save it back to the DB.
        /// </summary>
        private void btnUpdateTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBoxExams.SelectedItem is Exam ex)
            {
                BuildExamWindow buildExamWindow = new BuildExamWindow(HttpExamRepository.Instance, ex);
                buildExamWindow.ShowDialog();
            }
        }
        /// <summary>
        ///  this manege the search for exam in the exams list.
        ///  every time user enter something to the search field this function activates and show only the exams containing the entered sequence.
        /// </summary>
        private void FilterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var exams = HttpExamRepository.Instance.Exams; //gets the exams list 
            if (this.FilterText.Text != string.Empty) //checks the the input in not empty.
            {
                this.listBoxExams.ItemsSource = exams.Where(e => e.Name.Contains(this.FilterText.Text, StringComparison.OrdinalIgnoreCase)); //get all the exams that contains the sequence entered in there names.
            }
            else
            {
                this.listBoxExams.ItemsSource = exams;//if the search bar is empty all the exams will be shown.
            }
        }


        /// <summary>
        ///     this function take read the selected exem from the local exam list box,
        ///     then the exam sent to the server to be uploaded to the DB using post request.
        /// </summary>
        private async void btnUploadDb_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBoxLocalExams.SelectedItem is Exam ex) //read the exam selected from the list box.
            {
                var res = await HttpExamRepository.Instance.AddExamAsync(ex); //send it top the server to be uploaded.
                if (res != null) //checks that the upload succeeded
                {
                    HttpExamRepository.Instance.DeleteLocalExam(ex); //delete the local exam file from the local folder.
                    this.LocalExams.Remove(ex); //remove the exam fron the local exam listbox.
                }
            }
        }

        /// <summary>
        ///     activate the load exam funtion in the repository.
        ///     then if an exam realy found and got back fron the repository the function loads it to the local exams listbox.
        /// </summary>
        private void btnLoadTest_Click(object sender, RoutedEventArgs e)
        {
            var exam = HttpExamRepository.Instance.LoadExam(); //activate the load function in the repository.
            if (exam != null)
            {
                this.LocalExams.Add(exam); //insert the loaded exam to the local exams listbox.
                return;
            }
            return;
        }

        /// <summary>
        ///     this function show the user a statistic wiindow for an exam selected fron the database.
        ///     if no particiaption in the selected exam the function show the user a message with that details.
        /// </summary>
        private void btnStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBoxExams.SelectedItem is Exam ex)
            {
                if (ex.Participations.Count > 0) //check there are partisipation that tool that exam.
                {
                    StatsticWindow statsticWindow = new StatsticWindow(ex); //show the statistic window for the selected exam.
                    statsticWindow.Show();
                }
                else  //if not participation the show the user message expalining that.
                {
                    string msg = "The exam statstic is empty.\n\nNo one took the Exam yet.";
                    MessageBox.Show(msg, "No Participations.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }


        /// <summary>
        ///     this function delete an exam selected from the DB exam listbox by the user,
        ///     the function calls the delete function in the repository.
        /// </summary>
        private void btnRemoveTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBoxExams.SelectedItem is Exam ex)
            {
                HttpExamRepository.Instance.RemoveExamAsync(ex.Id); //call the delete function in the repository with the exam id the user choose to delete.
            }
        }
    }
}
