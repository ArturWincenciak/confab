﻿using System;
using System.Collections.Generic;

namespace Confab.Modules.Conferences.Core.Entities;

internal class Host
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Conference> Conferences { get; set; }
}