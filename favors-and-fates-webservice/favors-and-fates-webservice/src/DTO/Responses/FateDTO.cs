using favors_and_fates_types;

namespace favors_and_fates_webservice.DTO;

public class FateDTO
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;

  public static FateDTO FromFate(Fate obj)
  {
    return new FateDTO
    {
      Id = obj.Id,
      Name = obj.Name,
      Value = obj.Value
    };
  }
}
