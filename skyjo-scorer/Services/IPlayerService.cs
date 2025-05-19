using Models.Domain;
using Models.Dto;

namespace Services;

public interface IPlayerService
{
    Task<Player> Add(AddPlayerScore addPlayerScore);
    Task<List<PlayerDto>> GetAllWithStats(int? year);
    Task<PlayerDto> Get(string name);
    //Task Update(PlayerDto playerDto);
    Task Delete(string name);
}
