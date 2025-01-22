using System.Collections.Frozen;
using CineControl.Common.Clients.SeanceService.IClients;
using CineControl.Common.JWTProvider;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Extensions;
using CineControl.OperatorPanel.Models.DTOs.Movie;
using CineControl.OperatorPanel.Service.IService;

namespace CineControl.OperatorPanel.Service;

public class MovieService(
    IMoviesClient moviesClient,
    IHttpContextAccessor httpContextAccessor,
    ITenantProvider tenantProvider,
    IJWTProvider jwtProvider
) : IMovieService
{
    private readonly IMoviesClient _moviesClient = moviesClient;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITenantProvider _tenantProvider = tenantProvider;
    private readonly IJWTProvider _jwtProvider = jwtProvider;

    public async Task<ResultT<IEnumerable<GetMovieResponse>>> GetMoviesAsync()
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovies = await _moviesClient.GetMoviesAsync(TenantId);

        if (!GetMovies.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return GetMovies.Value.ToResponse();
    }

    public async Task<ResultT<GetMovieResponse>> GetMovieByIdAsync(int id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _moviesClient.GetMoviesByIdAsync(id,TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return GetMovie.Value.ToResponse();
    }


    public async Task<Result> AddMovieAsync(AddMovieRequest request)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _moviesClient.AddMovieAsync(request.ToRequest(),TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }



    public async Task<Result> UpdateMovieAsync(int id, UpdateMovieRequest request)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovie = await _moviesClient.UpdateMovieAsync(id,request.ToRequest(),TenantId);

        if (!GetMovie.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }

    public async Task<Result> DeleteMovieAsync(int id)
    {
        _jwtProvider.SetToken(_httpContextAccessor.GetTokenValue());
        var TenantId = _jwtProvider.GetTenantId();

        _tenantProvider.SetTenant(Guid.Parse(TenantId));

        var GetMovies = await _moviesClient.DeleteMovieAsync(id,TenantId);

        if (!GetMovies.IsSuccess)
        {
            return CinemaServiceErrors.Failure();
        }
        return Result.Success();
    }
}
