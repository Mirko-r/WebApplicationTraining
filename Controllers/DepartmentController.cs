using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private static List<Department> departmentList = new List<Department>{ 
            new Department {DepartmentId = 1, Name = "Development"},
            new Department {DepartmentId = 2, Name = "Testing"}
        };

        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return departmentList;
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            foreach (var department in departmentList)
            {
                if (department.DepartmentId == id)
                {
                    return Ok(department);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            departmentList.Add(department);
            return Ok(departmentList);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Department dp)
        {
            foreach (var department in departmentList)
            {
                if(dp.DepartmentId == department.DepartmentId)
                {
                    department.Name = dp.Name;
                    return Ok(departmentList);
                }
            }
            return null;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            foreach(var department in departmentList)
            {
                if(department.DepartmentId == id)
                {
                    departmentList.Remove(department);
                    return Ok(departmentList);
                }
            }
            return NotFound();
        }
    }
}
