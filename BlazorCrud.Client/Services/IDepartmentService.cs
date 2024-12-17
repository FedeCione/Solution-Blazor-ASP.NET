using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
	public interface IDepartmentService
	{
		Task<List<DepartmentDTO>> List();
	}
}
