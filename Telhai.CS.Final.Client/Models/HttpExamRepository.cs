using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace Telhai.CS.Final.Client.Models
{
    public class HttpExamRepository : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Exam> _exams;
        public ObservableCollection<Exam> Exams
        {
            get { return _exams; }
            set
            {
                _exams = value;
                OnPropertyChanged("Exams");
            }
        }

        HttpClient clientApi;
        static private HttpExamRepository? _instance = null;

        private HttpExamRepository()
        {
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7070"); //set the connection url.
        }

        // Get Factory Of HttpExamRepository  as singelton
        public static HttpExamRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpExamRepository();
                }
                return _instance;
            }
        }

        /// <summary>
        ///     the function get all the exams saved in the DB/
        /// </summary>
        public async Task<List<Exam>> GetAllExamsAsync()
        {
            HttpResponseMessage response = await clientApi.GetAsync("api/Exams");// send to the server side GET request to get all the exams from the DB
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string? dataString = await response.Content.ReadAsStringAsync();
                var exams = JsonSerializer.Deserialize<ObservableCollection<Exam>>(dataString); //create new Exams list from the exams json the server sent.

                if (exams != null)
                {
                    Exams = exams;
                    return exams.ToList();
                }
            }
            return new List<Exam>();
        }

        /// <summary>
        ///     the function sends request to the server side with an exam id the return the Exam objext with the spasific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Exam> GetExamAsync(int id)
        {
            HttpResponseMessage response = await clientApi.GetAsync($"api/Exams/{id}");
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string? dataString = await response.Content.ReadAsStringAsync();
                var exam = JsonSerializer.Deserialize<Exam>(dataString);

                if (exam != null)
                {
                    return exam;
                }
            }
            return new Exam();
        }

        /// <summary>
        ///     the function gets an exam object and send it as a Json to the server side.
        ///     then the server send post request and post it to the DB.
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        public async Task<Exam?> AddExamAsync(Exam exam)
        {
            try
            {
                var response = await clientApi.PostAsJsonAsync("api/Exams", exam); //serialize the exam object to Json the send it to the server to be posted.
                response.EnsureSuccessStatusCode();
                var res = GetAllExamsAsync(); //returns all the exam with the new exam isserted.

                return await response.Content.ReadFromJsonAsync<Exam>(); //read the content from the response got back from the server side.
            }
            catch (Exception ex)//if something went wrong this catch the exeption thrown and show a messege with the error.
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Failed to add new exam.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// the function gets a exam object the send it to be updated in the DB by Put request.
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        public async Task<Exam?> UpdateExamAsync(Exam exam)
        {
            try
            { 
                string jsonStudentString = JsonSerializer.Serialize<Exam>(exam);//serialize the Exam to Json obj.
                var content = new StringContent(jsonStudentString, Encoding.UTF8, "application/json"); //create string fron the json created.
                var response = await clientApi.PutAsync($"api/Exams/{exam.Id}", content);//sent the serialized exam to the server to a PUT request.
                response.EnsureSuccessStatusCode();
                OnPropertyChanged("Exams");//dedclare that the exams list was changed.
                return await response.Content.ReadFromJsonAsync<Exam>();
            }

            catch (Exception e)//if something went wrong this catch the exeption thrown and show a messege with the error.
            {
                string msg = e.Message;
                MessageBox.Show(msg, "Failed to update the exam.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// this functio nget exam id and send it to the server to be deleted from the DB
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<Exam?> RemoveExamAsync(int examId)
        {
            try
            {
                HttpResponseMessage response = await clientApi.DeleteAsync($"api/Exams/{examId}"); //send delete request with the exams id to the server/
                response.EnsureSuccessStatusCode();
                OnPropertyChanged("Exams");//dedclare that the exams list was changed.
                var res = GetAllExamsAsync();

                return null;
            }
            catch (Exception ex)//if something went wrong this catch the exeption thrown and show a messege with the error.
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Failed to add new question.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;

            }
        }
        
        /// <summary>
        /// this function gets exams id and a question object and send it to the server to post the question in the exam with the given id.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<Question?> AddQuestionAsync(int examId, Question question)
        {
            try
            {
                string jsonQuestionString = JsonSerializer.Serialize<Question>(question);//serialize the question to Json obj.
                var content = new StringContent(jsonQuestionString, Encoding.UTF8, "application/json");//create string from the json created.
                var response = await clientApi.PostAsync($"api/Exams/{examId}/Questions", content);//sent the serialized question to the server to a post request.

                response.EnsureSuccessStatusCode();
                OnPropertyChanged("Exams");//dedclare that the exams list was changed.
                var res = GetAllExamsAsync();

                return await response.Content.ReadFromJsonAsync<Question>();
            }
            catch (Exception ex)//if something went wrong this catch the exeption thrown and show a messege with the error.
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Failed to add new question.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        /// this function gets exams object and a question object and send it to the server to delete the question from the exam .
        public async Task<Exam?> RemoveQuestionAsync(Exam exam, Question question)
        {
            try
            {
                HttpResponseMessage response = await clientApi.DeleteAsync($"api/Exams/{exam.Id}/Questions/{question.Id}"); //send to the server the exam id and questions id to delete the question fromn the exam
                response.EnsureSuccessStatusCode();
                OnPropertyChanged("Exams");//dedclare that the exams list was changed.
                var res = GetAllExamsAsync();

                return null;
            }
            catch (Exception ex)//if something went wrong this catch the exeption thrown and show a messege with the error.
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Failed to add new question.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        //save exam to a local folder untill the teacher uploads it to the db.
        public void SaveExam(Exam exam)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonStudentsString = JsonSerializer.Serialize<Exam>(exam, options);

            if (!Directory.Exists("Local_Exams"))//create directory in it we will save the file (only if not exists)
            {
                Directory.CreateDirectory("Local_Exams");
            }
            File.WriteAllText($"Local_Exams/{exam.Name}.json", jsonStudentsString); //witing the exam to a file.
        }


        /// <summary>
        ///     the function gets an exam object.
        ///     then it checks if the is aa file with the exams name in the local exam folder.
        ///     if there is one it delete it.
        /// </summary>
        public void DeleteLocalExam(Exam exam)
        {
            string examPath = $"{Directory.GetCurrentDirectory()}\\Local_exams\\{exam.Name}.json";
            if (File.Exists(examPath))
            {
                File.Delete(examPath);
            }
        }

        /// <summary>
        ///     this function read all the exams saved to the local directory the from eacch file read it creates an Exam object.
        ///     then it assign each exam created to the exams list to be shown in the teachers window.
        /// </summary>
        public List<Exam> LoadExams()
        {
            if (!Directory.Exists("Local_Exams"))//create directory in it we will save the file (only if not exists)
            {
                Directory.CreateDirectory("Local_Exams");
            }
            List<Exam> exams = new List<Exam>();
            string path = $"{Directory.GetCurrentDirectory()}\\Local_exams\\";
            string[] files = Directory.GetFiles(path, "*.json", SearchOption.AllDirectories);
            if (files.Count() != 0)
            {
                foreach (string file in files)
                {
                    string examText = File.ReadAllText(file);///read the file as a json
                    var exam = JsonSerializer.Deserialize<Exam>(examText); //desialize the file in to an exam file.
                    exams.Add(exam); // add objexct to exams list
                }
            }
            return exams;
        }

        /// <summary>
        /// this function lets the user cchoose a single exam file saved in a local directory be opening the dialog window and let him choose a file.
        /// the the function desiralize the file in to exam object and assign it to the local exams list to show in the tteachers window.
        /// </summary>
        public Exam? LoadExam()
        {
            string jsonPath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                jsonPath = openFileDialog.FileName; //get the file path.

                if (jsonPath != string.Empty)
                {
                    string examText = File.ReadAllText(jsonPath); ///read the file as a json
                    var exam = JsonSerializer.Deserialize<Exam>(examText); //desialize the file in to an exam.
                    SaveExam(exam);
                    return exam;
                }
                else
                {
                    string msg = "Failed to load the selected exam,\nPlease try again.";
                    MessageBox.Show(msg, "Failed to load exam.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        ///     this function get an exam id and Partisipation object and send it to the server.
        ///     then the server assign the partisipation object to the exam with the gibed id in the db.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="participation"></param>
        /// <returns></returns>
        public async Task<Participation?> AddParticipationAsync(int examId, Participation participation)
        {
            try
            {
                string jsonParticipationString = JsonSerializer.Serialize<Participation>(participation);//serialize the partisipation to Json obj.
                var content = new StringContent(jsonParticipationString, Encoding.UTF8, "application/json");//create string from the json created.
                var response = await clientApi.PostAsync($"api/Exams/{examId}/Participations", content);//sent the serialized partisipation to the server to a post request.

                response.EnsureSuccessStatusCode();
                OnPropertyChanged("Exams"); //declare that the Exams list was changed.
                var res = GetAllExamsAsync();

                return await response.Content.ReadFromJsonAsync<Participation>();
            }
            catch (Exception ex) //show a message to the user is something went wrong while posting the partisipation.
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Failed to add new participation.", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
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



