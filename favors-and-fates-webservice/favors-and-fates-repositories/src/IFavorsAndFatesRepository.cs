using System.Runtime.CompilerServices;
using favors_and_fates_types;

namespace favors_and_fates_repositories;

public interface IFavorsAndFatesRepository
{
  void Initialize();
  Task<FavorRequested> GetLatestFavorRequest();
  Task<Fate> GetFateForFavorResponse(FavorResponse favor);
}
