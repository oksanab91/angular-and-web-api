using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIs.AspNetCore.ExistingDb.Models;

namespace WebAPIs.AspNetCore.ExistingDb.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeTestsController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TraineeTestsController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/TraineeTests
        [HttpGet]
        public async Task<IActionResult> GetTraineeTest()
        {                        
            if (!ModelState.IsValid)
            {
                return null;
            }

            IEnumerable<TraineeTestSubject> traineeTest = null;

            var taskRetrieve = Task.Run(() => { traineeTest = RetrieveTraineeTestsLinq(); });
            await taskRetrieve;


            if (traineeTest == null)
            {
                return NotFound();
            }

            return Ok(traineeTest.ToList());
        }

        // GET: api/TraineeTests/5
        [HttpGet("{traineeId}")]
        public async Task<IActionResult> GetTraineeTest([FromRoute] int traineeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            IList<TraineeTestSubject> traineeTest = await RetrieveTraineeTests(traineeId);
            
            if (traineeTest == null)
            {
                return NotFound();
            }

            return Ok(traineeTest);
        }

        // PUT: api/TraineeTests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraineeTest([FromRoute] int id, [FromBody] TraineeTest traineeTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traineeTest.TraineeTestId)
            {
                return BadRequest();
            }

            _context.Entry(traineeTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeTestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TraineeTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraineeTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traineeTest = await _context.TraineeTest.FindAsync(id);
            if (traineeTest == null)
            {
                return NotFound();
            }

            _context.TraineeTest.Remove(traineeTest);
            await _context.SaveChangesAsync();

            return Ok(traineeTest);
        }

        private async Task<IList<TraineeTestSubject>> RetrieveTraineeTestsLambda(int traineeId)
        {
            var traineeTest = new List<TraineeTestSubject>();

            var traineeTestsWithSubjects = await _context.TraineeTest
                    .Where(tr => tr.TraineeId == traineeId)
                    .Include((TraineeTest t) => t.TestNav.TestSubjectNav)
                    .ThenInclude(s => s.SubjectNav)
                    .ToListAsync();

            try
            {
                traineeTest = traineeTestsWithSubjects
                .Select(test => 
                    new { test.TraineeTestId, test.TestStatus, test.TestNav.Description, test.TestNav.TestSubjectNav })
                .SelectMany(testWithSubject => testWithSubject.TestSubjectNav
                    .Select(subject => new TraineeTestSubject
                    {
                        TraineeTestId = testWithSubject.TraineeTestId,
                        TestDescription = testWithSubject.Description,
                        TestStatus = testWithSubject.TestStatus,
                        SubjectName = subject.SubjectNav.Name,
                    }))
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }            

            return traineeTest;
        }

        private async Task<IList<TraineeTestSubject>> RetrieveTraineeTests(int traineeId)
        {
            IList<TraineeTestSubject> traineeTest = new List<TraineeTestSubject>();

            try
            {
                var traineeTestsWithSubjects = await _context.TraineeTest
                    .Where(tr => tr.TraineeId == traineeId)
                    .Include((TraineeTest t) => t.TestNav.TestSubjectNav)
                    .ThenInclude(s => s.SubjectNav)
                    .ToListAsync();

                foreach (TraineeTest test in traineeTestsWithSubjects)
                {
                    foreach (TestSubject subject in test.TestNav.TestSubjectNav)
                    {
                        var traineeTestSubject = new TraineeTestSubject
                        {
                            TraineeTestId = test.TraineeTestId,
                            TestDescription = test.TestNav.Description,
                            TestStatus = test.TestStatus,
                            SubjectName = subject.SubjectNav.Name
                        };

                        traineeTest.Add(traineeTestSubject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }

            return traineeTest;
        }

        private IEnumerable<TraineeTestSubject> RetrieveTraineeTestsLinq()
        {
            var traineeTest = from trTest in _context.TraineeTest
                              join test in _context.Test on trTest.TestId equals test.TestId into table1
                              from test in table1
                              join testSubject in _context.TestSubject on test.TestId equals testSubject.TestId into table2
                              from testSubject in table2
                              join subject in _context.Subject on testSubject.SubjectCode equals subject.SubjectCode into table3
                              from subject in table3

                              select new TraineeTestSubject
                              {
                                  TraineeTestId = trTest.TraineeTestId,
                                  TestDescription = test.Description,
                                  TestStatus = trTest.TestStatus,
                                  SubjectName = subject.Name
                              };

            return traineeTest;
        }

        private bool TraineeTestExists(int id)
        {
            return _context.TraineeTest.Any(e => e.TraineeTestId == id);
        }
    }
}