using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class DemoProduct1
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double UnitPrice { get; set; }

    public int? CategoryId { get; set; }
}
