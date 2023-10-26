﻿using TaskTracker.Entities.Models;

namespace TaskTracker.Contract
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(Guid projectId, bool trackChanges);
    }
}