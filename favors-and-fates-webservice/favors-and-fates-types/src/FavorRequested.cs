namespace favors_and_fates_types;

public class FavorRequested
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public IEnumerable<TokenRequested> TokensRequested { get; set; } = Array.Empty<TokenRequested>();
  public Fate Fate { get; set; } = new Fate();
}
