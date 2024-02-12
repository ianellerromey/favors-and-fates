using favors_and_fates_types;

namespace favors_and_fates_services;

public interface IFavorsAndFatesService
{
  Task<FavorRequested> GetLatestFavorRequest();
  Task<Fate> GetFateForFavorResponse(FavorResponse favor);
}
