using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Interfaces
{
    public interface IEmployeeService
    {
        public  List<EmployeeResponse> GetAllEmployeeRecord();
        public EmployeeResponse GetEmployeeRecordsById(int id);
        public EmployeeResponse CreateEmployeeRecord(EmployeeResponse employeeResponse);
        public EmployeeResponse UpdateEmployeeRecord(EmployeeResponse employeeResponse , int id);
        public bool DeleteEmployeeRecord(int id);
    }
}
