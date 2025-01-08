using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PersonnelSystem.Api.Records;
using PersonnelSystem.Application.Services;
using PersonnelSystem.Core.Model;
using PersonnelSystem.Data.Repositories;

namespace PersonnelSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactController : ControllerBase
    {
        private readonly IDateService _service;
        private readonly IEmployeeService _employeeService;
        private readonly IFindService _findService;
        private readonly ILogger<ReactController> _logger;

        public ReactController(IDateService service, IEmployeeService employeeService, IFindService findService, ILogger<ReactController> logger)
        {
            _service = service;
            _employeeService = employeeService;
            _findService = findService;
            _logger = logger;
        }

        [HttpPost("employee/add")]
        public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployee employee)
        {
            var (id, error) = await _employeeService.CreateEmployee(employee.Name, employee.SubdivisionId);

            if (error != string.Empty)
            {
                return BadRequest(error);
            }

            return Ok(id);
        }

        [HttpGet("subdivision/bydate")]
        public async Task<IActionResult> GetSubdivisionByDate([FromQuery]DateTime date)
        {
            return Ok((await _service.GetSubdivisionByTime(date))
                .Select(s => new SubdivisionRecord(s.Id, s.Name, s.CreatedAt))
                .ToList());
        }

        [HttpGet("subdivision/all")]
        public async Task<IActionResult> GetAllSubdivisions()
        {
            return Ok((await _findService.FindAllSubdivision())
                .Select(s => new SubdivisionRecord(s.Id, s.Name, s.CreatedAt))
                .ToList());
        }

        [HttpGet("employee/bydate")]
        public async Task<IActionResult> GetEmployeesBySubdivisionAndDate([FromQuery]int subdivisionId, [FromQuery]DateTime start, [FromQuery]DateTime end)
        {
            return Ok((await _service.GetEmployeesByTime(subdivisionId, start, end))
                .Select(s => new EmployeeRecord(s.Id, s.Name, s.SubdivisionId))
                .ToList());

        }

        [HttpGet("checkme")]
        public IActionResult CheckAuthorize()
        {
            return Ok();
        }
    }
}
