using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;

        public WorkerController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var responseApi = new ResponseAPI<List<WorkerDTO>>();
            var listWorkerDTO = new List<WorkerDTO>();

            try
            {
                foreach (var item in await _dbContext.Workers.Include(d => d.IdDepartmentNavigation).ToListAsync())
                {
                    listWorkerDTO.Add(new WorkerDTO
                    {
                        IdWorker = item.IdWorker,
                        FullName = item.FullName,
                        IdDepartment = item.IdDepartment,
                        Salary = item.Salary,
                        ContractDate = item.ContractDate,
                        Department = new DepartmentDTO
                        {
                            IdDepartment = item.IdDepartmentNavigation.IdDepartment,
                            Name = item.IdDepartmentNavigation.Name,
                        }
                    });

                    responseApi.IsCorrect = true;
                    responseApi.Value = listWorkerDTO;
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Search/{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var responseApi = new ResponseAPI<WorkerDTO>();
            var WorkerDTO = new WorkerDTO();

            try
            {
                var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(x => x.IdWorker == id);

                if (dbWorker != null)
                {
                    WorkerDTO.IdWorker = dbWorker.IdWorker;
                    WorkerDTO.FullName = dbWorker.FullName;
                    WorkerDTO.IdDepartment = dbWorker.IdDepartment;
                    WorkerDTO.Salary = dbWorker.Salary;
                    WorkerDTO.ContractDate = dbWorker.ContractDate;

                    responseApi.IsCorrect = true;
                    responseApi.Value = WorkerDTO;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "No encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(WorkerDTO worker)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbWorker = new Worker
                {
                    FullName = worker.FullName,
                    IdDepartment = worker.IdDepartment,
                    Salary = worker.Salary,
                    ContractDate = worker.ContractDate,
                };

                _dbContext.Workers.Add(dbWorker);
                await _dbContext.SaveChangesAsync();

                if (dbWorker.IdWorker != 0)
                {
                    responseApi.IsCorrect = true;
                    responseApi.Value = dbWorker.IdWorker;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(WorkerDTO worker, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(x => x.IdWorker == id);

                if (dbWorker != null)
                {
                    dbWorker.FullName = worker.FullName;
                    dbWorker.IdDepartment = worker.IdDepartment;
                    dbWorker.Salary = worker.Salary;
                    dbWorker.ContractDate = worker.ContractDate;

                    _dbContext.Workers.Update(dbWorker);
                    await _dbContext.SaveChangesAsync();

                    responseApi.IsCorrect = true;
                    responseApi.Value = dbWorker.IdWorker;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "Empleado no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.IsCorrect = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbWorker = await _dbContext.Workers.FirstOrDefaultAsync(x => x.IdWorker == id);

                if (dbWorker != null)
                {
                    _dbContext.Workers.Remove(dbWorker);
                    await _dbContext.SaveChangesAsync();

                    responseApi.IsCorrect = true;
                }
                else
                {
                    responseApi.IsCorrect = false;
                    responseApi.Message = "Empleado no encontrado";
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
