using PodcastApi.DTOs.Guests;
using PodcastApi.Models;

namespace PodcastApi.Interfaces;

public interface IGuestService
{
    Task<IEnumerable<GuestDto>> GetAllGuestsAsync(CancellationToken cancellationToken = default);
    Task<GuestDto?> GetGuestByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GuestDto> CreateGuestAsync(CreateGuestRequest request, CancellationToken cancellationToken = default);
    Task UpdateGuestAsync(int id, UpdateGuestRequest request, CancellationToken cancellationToken = default);
    Task DeleteGuestAsync(int id, CancellationToken cancellationToken = default);
}