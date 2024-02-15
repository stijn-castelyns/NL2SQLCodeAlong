using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class DemoProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
