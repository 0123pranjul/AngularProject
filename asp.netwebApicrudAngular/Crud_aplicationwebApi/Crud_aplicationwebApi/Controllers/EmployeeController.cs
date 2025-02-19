using Crud_aplicationwebApi.Models;
using Crud_aplicationwebApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_aplicationwebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmpLoyeeRepository emp;

        public EmployeeController(EmpLoyeeRepository empLoyeeRepository)
        {
            this.emp = empLoyeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> EmployeeList()
        {
            var allemp = await emp.GetAllEmployees();
            return Ok(allemp);
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee( Employee vm)
        {
            await emp.SaveEmployee(vm);
            return Ok(vm);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] Employee vm)
        {
            await emp.updateEmployee(id, vm);
            return Ok(vm);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult>deleteEmployee(int id)
        {
            await emp.DeleteEmployee(id);
            return Ok();
        }
    }
}
