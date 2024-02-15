using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class ProductWithCatAndSupp
{
    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;
}
