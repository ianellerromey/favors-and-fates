using favors_and_fates_types;

namespace favors_and_fates_webservice.DTO;

public class TokenRequestedDTO
{
  public string Name { get; set; } = string.Empty;

  public static TokenRequestedDTO FromTokenRequested(TokenRequested obj)
  {
    return new TokenRequestedDTO
    {
      Name = obj.Name
    };
  }
}
