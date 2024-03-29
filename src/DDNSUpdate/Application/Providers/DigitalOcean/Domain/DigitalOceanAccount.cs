﻿using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Domain;

public record DigitalOceanAccount
{
    public IList<DigitalOceanDomain> Domains { get; set; } = new List<DigitalOceanDomain>();

    public string Token { get; set; } = default!;
}