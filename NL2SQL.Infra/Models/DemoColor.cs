using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class DemoColor
{
    public int ColorId { get; set; }

    public string ColorName { get; set; } = null!;
}
