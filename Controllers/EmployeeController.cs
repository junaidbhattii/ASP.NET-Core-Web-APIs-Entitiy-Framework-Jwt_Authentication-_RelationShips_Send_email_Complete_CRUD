using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthentication_Relations_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public List<EmployeeResponse> GetAllEmployee()
        {
            var employees = _employeeService.GetAllEmployeeRecord();
            return employees;
        }

        [HttpGet("{id}")]
        public EmployeeResponse GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeRecordsById(id);
            return employee;
        }

        [HttpPost]
        public EmployeeResponse CreateEmpRecord([FromBody] EmployeeResponse employeeResponse)
        {
            var employee = _employeeService.CreateEmployeeRecord(employeeResponse);
            return employee;
        }

        [HttpPut("{id}")]
        public EmployeeResponse UpdateEmpRecord(int id, [FromBody] EmployeeResponse employeeResponse)
        {
            var employee = _employeeService.UpdateEmployeeRecord(employeeResponse, id);
            return employee;
        }

        [HttpDelete("{id}")]
        public bool DeleteEmpRecord(int id)
        {
            var employee = _employeeService.DeleteEmployeeRecord(id);
            return employee;
        }
    }
}
