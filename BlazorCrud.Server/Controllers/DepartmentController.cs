using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;

        public DepartmentController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var responseApi = new ResponseAPI<List<DepartmentDTO>>();
            var listDepartmentDTO = new List<DepartmentDTO>();

            try
            {
                foreach (var item in await _dbContext.Departments.ToListAsync())
                {
                    listDepartmentDTO.Add(new DepartmentDTO
                    {
                        IdDepartment = item.IdDepartment,
                        Name = item.Name
                    });

                    responseApi.IsCorrect = true;
                    responseApi.Value = listDepartmentDTO;
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
