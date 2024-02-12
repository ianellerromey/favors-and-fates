using favors_and_fates_types;

namespace favors_and_fates_webservice.DTO.Requests;

public class FavorResponseDTO
{
  public string Id { get; set; } = string.Empty;
  public IEnumerable<string> TokensOffered { get; set; } = Array.Empty<string>();

  public static FavorResponse ToFavorResponse(FavorResponseDTO dto, IFormFileCollection files)
  {
    var tokensOffered = new List<TokenOffered>();

    for(int i = 0, j = dto.TokensOffered.Count(); i < j; ++i)
    {
      var file = files.ElementAt(i);

      var bytes = Array.Empty<byte>();
      using (var stream = new MemoryStream())
      {
          file.CopyTo(stream);

          bytes = stream.ToArray();
      }

      tokensOffered.Add(new TokenOffered
      {
        Name = dto.TokensOffered.ElementAt(i),
        FileName = file.FileName,
        Value = bytes
      });
    }

    return new FavorResponse
    {
      Id = dto.Id,
      TokensOffered = tokensOffered
    };
  }
}
