﻿using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace RBA.Domain.Entities;

[JsonObject(MemberSerialization.OptIn), Table(Name = "asf.role_action", DisableSyncStructure = true)]
public partial class RoleAction : IEntity
{

  [JsonProperty, Column(Name = "action_id", IsPrimary = true)]
  public int Action_Id { get; set; }

  [JsonProperty, Column(Name = "role_id", IsPrimary = true)]
  public int Role_Id { get; set; }

  [JsonProperty, Column(Name = "created_date", DbType = "timestamptz", InsertValueSql = "CURRENT_TIMESTAMP")]
  public DateTime? Created_Date { get; set; }

  [JsonProperty, Column(Name = "is_active")]
  public bool? Is_Active { get; set; }

  [JsonProperty, Column(Name = "last_edit_date", DbType = "timestamptz", InsertValueSql = "CURRENT_TIMESTAMP")]
  public DateTime? Last_Edit_Date { get; set; }

}
