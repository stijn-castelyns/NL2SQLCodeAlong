using System;
using System.Collections.Generic;

namespace NL2SQL.Infra.Models;

public partial class Client
{
    public int ClientNr { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }
}
