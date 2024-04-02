﻿using System;
using System.Collections.Generic;

namespace Confab.Modules.Users.Core.DTO;

internal class AccountDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public DateTime CreateAt { get; set; }
}