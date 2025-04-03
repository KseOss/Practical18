using System;
using System.Collections.Generic;

namespace Practical18.Models;

public partial class WorkersInfo
{
    public int WorkerId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string DepartmentName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public decimal SalaryAmount { get; set; }

    public int Experience { get; set; }

    public int WorkerRank { get; set; }

    public string Position { get; set; } = null!;
}
