using Microsoft.AspNetCore.Mvc;
using favors_and_fates_services;
using favors_and_fates_webservice.DTO;
using favors_and_fates_webservice.DTO.Requests;
using Newtonsoft.Json;

namespace favors_and_fates_webservice;

[ApiController]
[Route("[controller]")]
public class FavorsAndFatesController : ControllerBase
{
  private IFavorsAndFatesService _favorService;

  public FavorsAndFatesController(IFavorsAndFatesService favorService)
  {
    _favorService = favorService;
  }

  [HttpGet]
  public async Task<ActionResult<FavorRequestedDTO>> GetLatestFavorRequest()
  {
    var favorRequested = await _favorService.GetLatestFavorRequest();
    return FavorRequestedDTO.FromFavorRequested(favorRequested);
  }

  [HttpPost]
  public async Task<ActionResult<FateDTO>> GetFateForFavorResponse([FromForm] IFormCollection formCollection)
  {
    var favorReponseJSON = formCollection["favorResponse"];
    var favorResponse = JsonConvert.DeserializeObject<FavorResponseDTO>(favorReponseJSON);
    var fate = await _favorService.GetFateForFavorResponse(FavorResponseDTO.ToFavorResponse(favorResponse, formCollection.Files));
    return FateDTO.FromFate(fate);
  }
}