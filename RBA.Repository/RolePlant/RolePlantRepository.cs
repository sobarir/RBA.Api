using Microsoft.Extensions.Logging;
using RBA.Domain.Entities;

namespace RBA.Repository;

public class RolePlantRepository(IFreeSql sql, ILogger<RolePlantRepository> logger) 
  : RepositoryBase<RolePlant>(sql, logger), IRolePlantRepository
{

  public override Task<RolePlant> CreateAsync(RolePlant entity)
  {
    return CreateIdentityAsync(entity);
  }

}
