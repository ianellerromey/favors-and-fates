namespace favors_and_fates_types;

public class TokenOffered
{
  public string Name { get; set; } = string.Empty;
  public string FileName { get; set; } = string.Empty;
  public byte[] Value { get; set; } = Array.Empty<byte>();
}
