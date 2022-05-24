namespace DevJobs.API.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using DevJobs.API.Entities;
    using Microsoft.EntityFrameworkCore;
    using DevJobs.API.Persistence.Repositories;
    using Serilog;

    [Route("api/job_vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();
            return Ok(jobVacancies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        [HttpPost]
        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>       
        /// <param name="model">Dados da vaga.</param>
        /// <returns>Objeto recem criado.</returns>        
        public IActionResult Post(AddJobVacancyInputModel model){  
            Log.Information("Post Vacancy");
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _repository.Add(jobVacancy);

            return CreatedAtAction("GetById", new { id = jobVacancy.Id }, jobVacancy);
        }

        [HttpPut("")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model){
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
           _repository.Update(jobVacancy);

            return NoContent();
        }
    }
}