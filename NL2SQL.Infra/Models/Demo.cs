using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class Demo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Product { get; set; }

    public double? UnitPrice { get; set; }

    public int? Amount { get; set; }
}
