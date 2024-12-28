using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Telhai.CS.Final.Server.Models;
using Telhai.CS.Final.Server.ModelsDB;


namespace Telhai.CS.Final.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ExamDbContext _context;

        public ExamsController(ExamDbContext context)
        {
            _context = context;
        }


        /// <summary>
        ///     return a list of all the exams in the db.
        ///     it also incluse for every exam all the questtion, answers, partisipations and errors in every exam.
        /// </summary>
        /// <returns></returns>
        // GET: api/Exams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            return await _context.Exams.Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .Include(e => e.Participations)
                .ThenInclude(p => p.Errors)
                .ToListAsync();

        }

        /// <summary>
        ///     return a single exams with a given id.
        ///     it also incluse  all the questtion, answers, partisipations and errors in the exam.       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.Exams.Include(e => e.Questions) //get an exam by id and include all the subobject exam has.
                .ThenInclude(q => q.Answers)
                .Include(e => e.Participations)
                .ThenInclude(p => p.Errors)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null) //if no exam with that id it returns an "NOT FOUND (404)" response
            {
                return NotFound();
            }
            return exam;
        }


        /// <summary>
        ///     PUT request.
        ///     this function get an exam and id and updated in the DB exam with the id sent with the exam object sent.
        /// </summary>
        /// <returns></returns>
        // PUT: api/Exams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam exam)
        {
            if (id != exam.Id) //validate that the id match the exam sent
            {
                return BadRequest();
            }

            _context.Entry(exam).State = EntityState.Modified; //mark the exam has modified

            try
            {
                await _context.SaveChangesAsync(); //saves the updated exan in the DB.
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!ExamExists(id)) //if the exam not exists in the db it returns an "NOT FOUND (404)" response
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("PutExam", new { id = exam.Id }, exam);
        }


        /// <summary>
        ///     this function get an exam object and post it to the db
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        // POST: api/Exams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exams.Add(exam); //adds the exam to the exams list.
            try
            {
                await _context.SaveChangesAsync(); //saves the exams list in the DB after the new exam has added.
            }
            catch (DbUpdateException)
            {
                if (ExamExists(exam.Id)) //if the exam already exists in the db it returns an "CONFLICT" response
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetExam", new { id = exam.Id }, exam);
        }



        /// <summary>
        ///     this function get an exams id.
        ///     then look if the exam exists in the DB and if it is then it delete him.
        ///     the function uses delete request.
        /// </summary>
        /// <returns></returns>
        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id); //find if exam with the given id is exists in the DB.
            if (exam == null)
            {
                return NotFound(); //if the exam not exists in the db it returns an "NotFound" response
            }

            _context.Exams.Remove(exam); //remove the exam found from the exams list.
            await _context.SaveChangesAsync(); //save the exams list after the exam has removed back to the DB.
            return NoContent();
        }

        // check if exam with given id is exists in the exams list.
        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }



        /// <summary>
        ///     this function gets an exam id and question object.
        ///     find if there is exam with that id in the exams list, if there is one then the given question is added to the exams questions list.
        ///     if no such exam then "not found" response is returned.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{examId}/Questions")]
        public async Task<ActionResult<Question>> PostQuestion(int examId, Question question)
        {
            var exam = await _context.Exams.FindAsync(examId); //find in the exams list an exam with given id.

            if (exam != null)
            {
                exam.Questions.Add(question); // add the new question sent to the exam that was found.
                _context.Questions.Add(question); //add the question to the question table in the Db.
                await _context.SaveChangesAsync(); //save the changes in the DB.
            }
            else
            {
                return NotFound();//if the exam not exists in the db it returns an "NotFound" response
            }
            return CreatedAtAction("PostQuestion", new { id = question.Id }, question);
        }



        /// <summary>
        ///     this function gets an exam id and question id.
        ///     if no such exan the delete request is denied and bad request returned.
        ///     the function look for a question with that id in the question table in the DB and id there is one it
        ///     gets removed and the updated table is saved back to the DB.
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        // DELETE: api/Exams/5
        [HttpDelete("{examId}/Questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int examId, int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);//find in the question list an question with given id.
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question); //remove the qurstion found from the questions table.
            await _context.SaveChangesAsync(); // save the changes back to the DB.
            return NoContent();
        }


        /// <summary>
        ///     this function gets an exam id and participation object.
        ///     find if there is exam with that id in the exams list, if there is one then the given participation is added to the exams participation list.
        ///     if no such exam then "not found" response is returned.
        /// </summary>
        [HttpPost("{examId}/Participations")]
        public async Task<ActionResult<Participation>> PostParticipation(int examId, Participation participation)
        {
            var exam = await _context.Exams.FindAsync(examId); //find in the exams list an exam with given id.

            if (exam != null)
            {
                exam.Participations.Add(participation); // add the new participation sent to the exam that was found.
                _context.Participations.Add(participation); //add the participation to the participation table in the Db.
                await _context.SaveChangesAsync(); //save the changes in the DB.
            }
            else
            {
                return NotFound();
            }
            return CreatedAtAction("PostParticipation", new { id = participation.Id }, participation);
        }
    }
}
