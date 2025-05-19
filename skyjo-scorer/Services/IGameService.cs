using Models.Dto;

namespace Services;

public interface IGameService
{
    Task Add(AddGameRequest addGameRequest);
    Task Delete(int number);
    Task<List<GameDto>> GetAll(int? year);
}
