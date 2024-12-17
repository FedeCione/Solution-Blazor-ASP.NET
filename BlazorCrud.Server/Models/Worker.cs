using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Worker
{
    public int IdWorker { get; set; }

    public string FullName { get; set; } = null!;

    public int IdDepartment { get; set; }

    public int Salary { get; set; }

    public DateOnly ContractDate { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;
}
