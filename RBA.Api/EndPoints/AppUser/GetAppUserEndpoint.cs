﻿using Microsoft.AspNetCore.Authorization;

using FastEndpoints;

using RBA.Api.Contracts;
using RBA.Repository;

namespace RBA.Api.EndPoints.AppUser;

[HttpGet("appusers/{id}"), AllowAnonymous]
public class GetAppUserEndpoint(IAppUserRepository repository) : Endpoint<EntityRequestById, Domain.Entities.AppUser>
{

  private readonly IAppUserRepository _repository = repository;

  public override async Task HandleAsync(EntityRequestById req, CancellationToken ct)
  {

    var res = await _repository.GetAsync(req.id);

    if (res is null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    await SendOkAsync(res, ct);

  }

}
