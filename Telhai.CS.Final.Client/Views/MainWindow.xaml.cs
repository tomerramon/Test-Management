using System;
using System.Windows;
using Telhai.CS.Final.Client.Models;

namespace Telhai.CS.Final.Client.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        ///     this the logic when user press the login button
        ///     open the teacher/student window dependent on the user choise.
        /// </summary>
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            await HttpExamRepository.Instance.GetAllExamsAsync();
            if (studentBtn.IsChecked == true)
            {
                studentBtn.Focus();
                StudentWindow s = new StudentWindow(HttpExamRepository.Instance); //open the students window 
                s.ShowDialog();
            }
            else if (TeacherBtn.IsChecked == true)
            {
                TeacherBtn.Focus();
                TeacherWindow t = new TeacherWindow(); //open the teachers window.
                t.ShowDialog();
            }
        }

        /// <summary>
        ///     activates when the windown in loaded and gets all the exams from the DB.
        /// </summary>
        private async void Window_Activated(object sender, EventArgs e)
        {
            await HttpExamRepository.Instance.GetAllExamsAsync();
        }
    }
}
