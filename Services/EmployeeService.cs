using JwtAuthentication_Relations_Authorization.Context;
using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly JwtAuthentication _JwtAuthentication;
        public EmployeeService(JwtAuthentication jwtAuthentication)
        {
            _JwtAuthentication = jwtAuthentication;
        }
        public EmployeeResponse CreateEmployeeRecord(EmployeeResponse employeeResponse)
        {
            var emp = _JwtAuthentication.Employees.SingleOrDefault(e => e.Email == employeeResponse.Email);
            if(emp == null)
            {
                    var employee = new Employee
                    {
                        Email = employeeResponse.Email,
                        Name = employeeResponse.Name,
                        Department = employeeResponse.Department,
                        Address = employeeResponse.Address,
                        StateCode = employeeResponse.StateCode,
                    };
                _JwtAuthentication.Employees.Add(employee);

                var Entry = _JwtAuthentication.SaveChanges();
                if(Entry > 0)
                {
                    var Response = new EmployeeResponse
                    {
                        Email = employee.Email,
                        Name = employee.Name,
                        Department = employee.Department,
                        Address = employee.Address,
                        StateCode = employee.StateCode,
                    };
                    return Response;
                }
                else
                {
                    throw new Exception("User Not Added Something Went Wrong");
                }
            }
            throw new Exception("User Aleady Exist, Try Another Email To Register");
        }

        public bool DeleteEmployeeRecord(int id)
        {
            var employee = _JwtAuthentication.Employees.SingleOrDefault(e => e.Id == id);
            if(employee == null)
            {
                return false;
                throw new Exception("Request For Invalid Id");
            }
            _JwtAuthentication.Employees.Remove(employee);
            _JwtAuthentication.SaveChanges();
            return true;
        }

        public List<EmployeeResponse> GetAllEmployeeRecord()
        {
            var employees = _JwtAuthentication.Employees.ToList();
            if (employees.Count <= 0)
            {
                throw new Exception("Record Not Found");
            }
            else
            {
                List<EmployeeResponse> employeeList = new List<EmployeeResponse>();

                foreach (Employee employee in employees)
                {
                    EmployeeResponse eachEmp = new EmployeeResponse();
                    eachEmp.Name = employee.Name;
                    eachEmp.Department = employee.Department;
                    employeeList.Add(eachEmp);
                }
                return employeeList;
            }
        }

        public EmployeeResponse GetEmployeeRecordsById(int id)
        {
            var emp = _JwtAuthentication.Employees.FirstOrDefault(emp => emp.Id == id);
            try
            {
                if (emp != null)
                {
                    var response = new EmployeeResponse
                    {
                        Email = emp.Email,
                        Name = emp.Name,
                        Address = emp.Address,
                        Department = emp.Department,
                        StateCode = emp.StateCode,
                    };
                    return response;
                }
                else
                {
                    throw new Exception("User Not Found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User Not Found", ex);
            }
        }

        public EmployeeResponse UpdateEmployeeRecord(EmployeeResponse employeeResponse , int id)
        {
            var emp = _JwtAuthentication.Employees.SingleOrDefault(emp => emp.Id == id);
            try
            {
                if (emp != null)
                {
                    _JwtAuthentication.Employees.Update(emp);
                    var Entry = _JwtAuthentication.SaveChanges();
                    if (Entry > 0)
                    {
                        var Response = new EmployeeResponse
                        {
                            Email = emp.Email,
                            Name = emp.Name,
                            Address = emp.Address,
                            Department = emp.Department,
                            StateCode = emp.StateCode,
                        };
                        return Response;
                    }
                    else
                    {
                        throw new Exception("Record Not Save Something Went Wrong");
                    }
                }
                else
                {
                    throw new Exception("EMployee not Found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Record Not Found", ex);
            }
        }

        
    }
}
