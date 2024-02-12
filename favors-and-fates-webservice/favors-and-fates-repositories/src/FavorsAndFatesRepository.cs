using Newtonsoft.Json;
using favors_and_fates_types;

namespace favors_and_fates_repositories;

public class FavorsAndFatesRepository : IFavorsAndFatesRepository
{
  private readonly FavorRequested _defaultFavorRequested = new FavorRequested
  {
    Id = "0", 
    Name = "Opening the Doorway",
    TokensRequested =
    [
      new TokenRequested
      {
        Name = "Drop a Coin into the Well"
      },
      new TokenRequested
      {
        Name = "Say the Words"
      }
    ],
    Fate = new Fate
    {
      Id = "0",
      Name = "The Doorway Opens",
      Value = "Nothing Will Ever Be The Same"
    }
  };
  private readonly string _assetsPath;

  private string FavorRequests
  {
    get
    {
      return Path.Combine(_assetsPath, "FavorRequests");
    }
  }

  private string FavorResponses
  {
    get
    {
      return Path.Combine(_assetsPath, "FavorResponses");
    }
  }

  public FavorsAndFatesRepository(string assetsPath)
  {
    _assetsPath = assetsPath;
  }

  public void Initialize()
  {
    CheckAndCreateDirectory(_assetsPath);
    CheckAndCreateDirectory(FavorRequests);
    CheckAndCreateDirectory(FavorResponses);

    var defaultFavorRequestedFileName = Path.Combine(FavorRequests, $"{_defaultFavorRequested.Id}.json");
    if(!File.Exists(defaultFavorRequestedFileName))
    {
      File.WriteAllText(defaultFavorRequestedFileName, JsonConvert.SerializeObject(_defaultFavorRequested));
    }
    return;
  }

  public async Task<FavorRequested> GetLatestFavorRequest()
  {
    return await Task.Run(() =>
    {
      var fileName = Directory.GetFiles(FavorRequests).OrderDescending().First();
      return DeserializeFavorsAndFatesFile(fileName);
    });
  }

  public async Task<Fate> GetFateForFavorResponse(FavorResponse favorResponse)
  {
    return await Task.Run(async () =>
    {
      var favorRequest = await GetLatestFavorRequest();

      if(favorRequest.Id != favorResponse.Id)
      {
        throw new InvalidOperationException($"Id for latest FavorRequest {favorRequest.Id} does not match Id for FavorResponse {favorResponse.Id}.");
      }

      var favorResponseDirectory = Path.Combine(FavorResponses, favorResponse.Id);
      CheckAndCreateDirectory(favorResponseDirectory);

      foreach(var offeredToken in favorResponse.TokensOffered)
      {
        await File.WriteAllBytesAsync(Path.Combine(favorResponseDirectory, Path.GetFileName(offeredToken.FileName)), offeredToken.Value);
      }

      return favorRequest.Fate;
    });
  }

  private FavorRequested DeserializeFavorsAndFatesFile(string fileName)
  {
    var fileBlob = File.ReadAllText(fileName);
    var favor = JsonConvert.DeserializeObject<FavorRequested>(fileBlob);

    if(favor == null)
    {
      throw new InvalidOperationException($"Could not deserialize FavorsAndFatesFile {fileName}.");
    }

    return favor;
  }

  private void CheckAndCreateDirectory(string path)
  {
    if(!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
  }
}
