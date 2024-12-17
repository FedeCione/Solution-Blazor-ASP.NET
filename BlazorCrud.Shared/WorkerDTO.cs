using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
	public class WorkerDTO
	{
		public int IdWorker { get; set; }

		[Required(ErrorMessage = "El campo {0} es requerido.")]
		public string FullName { get; set; } = null!;

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
		public int IdDepartment { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
		public int Salary { get; set; }

		public DateOnly ContractDate { get; set; }

		public DepartmentDTO? Department { get; set; }
	}
}
