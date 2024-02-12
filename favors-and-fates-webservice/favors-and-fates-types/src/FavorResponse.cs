namespace favors_and_fates_types;

public class FavorResponse
{
  public string Id { get; set; } = string.Empty;
  public IEnumerable<TokenOffered> TokensOffered { get; set; } = Array.Empty<TokenOffered>();
}
