using System;
using System.Collections.Generic;

namespace LoginApp.Models;

public partial class UserInfo
{
    public string Id { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Birth { get; set; }

    public int Gender { get; set; }
}
