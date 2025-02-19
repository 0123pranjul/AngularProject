using Crud_aplicationwebApi.Data;
using Crud_aplicationwebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_aplicationwebApi.Repository
{
    public class EmpLoyeeRepository
    {
        private readonly AppDbContext dbContext;

        public EmpLoyeeRepository( AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }
        public async Task SaveEmployee(Employee emp)
        {
            await dbContext.Employees.AddAsync(emp);
            await dbContext.SaveChangesAsync();
        }
      
        public async Task updateEmployee(int id, Employee obj)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }
            employee.Name = obj.Name;
            employee.Email = obj.Email;
            employee.Mobile = obj.Mobile;
            employee.Age = obj.Age;
            employee.Salary = obj.Salary;
            employee.Status = obj.Status;

            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteEmployee(int id)
        {
            var employees = await dbContext.Employees.FindAsync(id);
            if(employees==null)
            {
                throw new Exception("Employee Not Found");
            }
            dbContext.Employees.Remove(employees);
            await dbContext.SaveChangesAsync();
        }

    }
}
