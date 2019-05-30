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
    public class TraineesController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TraineesController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/Trainees
        [HttpGet]
        public async Task<IActionResult> GetTrainee()
        {            
            var trainees = await _context.Trainee.ToListAsync();

            IEnumerable<TraineeShort> traineeShort = trainees
                .Select(t => new TraineeShort { TraineeId = t.TraineeId, TraineeName = t.TraineeName });

            if (traineeShort == null)
            {
                return NotFound();
            }

            return Ok(traineeShort);
        }
        

        // GET: api/Trainees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainee = await _context.Trainee.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            TraineeShort traineeShort = new TraineeShort { TraineeId = trainee.TraineeId, TraineeName = trainee.TraineeName };    

            return Ok(traineeShort);
        }

        // PUT: api/Trainees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainee([FromRoute] int id, [FromBody] TraineeShort trainee)
        {
            int result;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainee.TraineeId)
            {
                return BadRequest();
            }

            Trainee traineeEdit = await _context.Trainee.FindAsync(id);

            traineeEdit.TraineeName = trainee.TraineeName;
            _context.Entry(traineeEdit).State = EntityState.Modified;

            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(result);            
        }

        // POST: api/Trainees
        [HttpPost]
        public async Task<IActionResult> PostTrainee([FromBody] TraineeShort trainee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trainee traineeToAdd = new Trainee(trainee.TraineeId, trainee.TraineeName);            

            _context.Trainee.Add(traineeToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainee", new { id = traineeToAdd.TraineeId },
                new TraineeShort { TraineeId = traineeToAdd.TraineeId, TraineeName = traineeToAdd.TraineeName });

        }

        // DELETE: api/Trainees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainee = await _context.Trainee.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            _context.Trainee.Remove(trainee);
            await _context.SaveChangesAsync();

            return Ok(trainee);
        }

        private bool TraineeExists(int id)
        {
            return _context.Trainee.Any(e => e.TraineeId == id);
        }
    }
}