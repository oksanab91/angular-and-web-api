using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIs.AspNetCore.ExistingDb.Models;

namespace WebAPIs.AspNetCore.ExistingDb.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeTestCreateController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TraineeTestCreateController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/TraineeTestCreate
        [HttpGet("{traineeId}")]
        public async Task<IActionResult> Get(int traineeId)
        {
            IList<TraineeTest> traineeTestsBeforeUpdate = await _context.TraineeTest
                .Where(t => t.TraineeId == traineeId)
                .ToListAsync();

            // Tests without duplicates
            HashSet<int> testIdHash = _context.TraineeTest
                .Where(t => t.TraineeId == traineeId)
                .Select(t => t.TestId).ToHashSet();
            IEnumerable<TestShort> testsAll = await _context.Test
                .Select(t => new TestShort { TestId = t.TestId, Name = t.Name, Description = t.Description })
                .ToListAsync();
            var tests = testsAll.Where(el => !testIdHash.Contains(el.TestId));

            // Subjects
            IEnumerable<SubjectShort> subjects = await _context.Subject
                .Select(s => new SubjectShort { SubjectCode = s.SubjectCode, Name = s.Name })
                .ToListAsync();

            // Statuses
            var statuses = new string[] { "Pass", "Failed" };

            TraineeTestCreateFill traineeTestCreate = new TraineeTestCreateFill(tests, subjects, statuses);

            if (traineeTestCreate == null)
            {
                return NotFound();
            }

            return Ok(traineeTestCreate);
        }

       // POST: api/TraineeTestCreate
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TraineeTestCreate traineeTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traineeTestToAdd = new TraineeTest
            {
                TraineeId = traineeTest.TraineeId,
                TestId = traineeTest.TestId,
                TestStatus = traineeTest.TestStatus                
            };

            _context.TraineeTest.Add(traineeTestToAdd);
            
            if (!TestSubjectExists(traineeTest.TestId, traineeTest.SubjectCode))
            {
                var testSubjectToAdd = new TestSubject
                {
                    TestId = traineeTest.TestId,
                    SubjectCode = traineeTest.SubjectCode
                };
                _context.TestSubject.Add(testSubjectToAdd);
            }
            
            var result = await _context.SaveChangesAsync(true);

            return Ok(result);
        }

        // PUT: api/TraineeTestCreate/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        private bool TestSubjectExists(int testId, string subjectCode)
        {
            return _context.TestSubject.Any(e => e.TestId == testId && e.SubjectCode == subjectCode);
        }        
    }
}
