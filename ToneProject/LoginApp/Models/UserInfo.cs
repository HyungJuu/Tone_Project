using System;
using System.Collections.Generic;

namespace LoginApp.Models;

public partial class UserInfo
{
    public string Id { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateOnly Birth { get; set; }

    public string Gender { get; set; } = null!;
}
