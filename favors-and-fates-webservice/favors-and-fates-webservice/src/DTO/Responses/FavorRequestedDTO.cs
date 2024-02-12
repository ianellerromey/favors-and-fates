using favors_and_fates_types;

namespace favors_and_fates_webservice.DTO;

public class FavorRequestedDTO
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public IEnumerable<TokenRequestedDTO> TokensRequested { get; set; } = Array.Empty<TokenRequestedDTO>();

  public static FavorRequestedDTO FromFavorRequested(FavorRequested obj)
  {
    return new FavorRequestedDTO
    {
      Id = obj.Id,
      Name = obj.Name,
      TokensRequested = obj.TokensRequested.Select(x => TokenRequestedDTO.FromTokenRequested(x))
    };
  }
}
