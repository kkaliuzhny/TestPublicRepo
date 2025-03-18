using Lab5TestTask.Data;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions.Where(dt => dt.DeviceType == Enums.DeviceType.Desktop)
                                        .OrderBy(s => s.StartedAtUTC)
                                        .FirstOrDefaultAsync();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        return await _dbContext.Sessions.Include(s => s.User)
                                        .Where(s => s.User.Status == Enums.UserStatus.Active && s.EndedAtUTC.Year < 2025)
                                        .ToListAsync();
    }
}
