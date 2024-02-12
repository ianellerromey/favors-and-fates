using favors_and_fates_repositories;
using favors_and_fates_types;

namespace favors_and_fates_services;

public class FavorsAndFatesService : IFavorsAndFatesService
{
  private readonly IFavorsAndFatesRepository _favorRepository;

  public FavorsAndFatesService(IFavorsAndFatesRepository favorRepository)
  {
    _favorRepository = favorRepository;
  }

  public async Task<FavorRequested> GetLatestFavorRequest() => await _favorRepository.GetLatestFavorRequest();

  public async Task<Fate> GetFateForFavorResponse(FavorResponse favor) => await _favorRepository.GetFateForFavorResponse(favor);
}
