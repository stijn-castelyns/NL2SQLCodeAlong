using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
