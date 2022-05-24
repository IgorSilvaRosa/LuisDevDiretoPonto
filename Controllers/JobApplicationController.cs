namespace DevJobs.API.Controllers
{
 using Microsoft.AspNetCore.Mvc;
 using DevJobs.API.Models;
 using DevJobs.API.Persistence;
 using DevJobs.API.Entities;
    using DevJobs.API.Persistence.Repositories;

    [Route("api/job-vancancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if(jobVacancy == null)
            return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
                );

            _repository.AddApplication(application);

            return NoContent();
        }
    }
}