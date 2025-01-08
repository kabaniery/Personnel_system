using Microsoft.AspNetCore.Mvc;
using PersonnelSystem.Api.Records;
using PersonnelSystem.Application.Services;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Repositories;

namespace PersonnelSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReactController : ControllerBase
    {
        private readonly IDateService _service;
        private readonly IEmployeeService _employeeService;

        public ReactController(IDateService service, IEmployeeService employeeService)
        {
            _service = service;
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody]string name, [FromBody]int subdivisionId)
        {
            var (id, error) = await _employeeService.CreateEmployee(name, subdivisionId);

            if (error != string.Empty)
            {
                return BadRequest(error);
            }

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubdivisionByDate([FromQuery]DateTime time)
        {
            return Ok((await _service.GetSubdivisionByTime(time))
                .Select(s => new SubdivisionRecord(s.Id, s.Name, s.CreatedAt))
                .ToList());
        }
    }
}
