using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
	public interface IWorkerService
	{
		Task<List<WorkerDTO>> List();
		Task<WorkerDTO> Search(int id);
		Task<int> Create(WorkerDTO worker);
		Task<int> Update(WorkerDTO worker);
		Task<bool> Delete(int id);
	}
}
