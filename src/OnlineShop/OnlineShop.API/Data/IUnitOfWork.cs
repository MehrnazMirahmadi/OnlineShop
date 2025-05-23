﻿using OnlineShop.API.Repository;

namespace OnlineShop.API.Data;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(CancellationToken cancellationToken);
    public IUserRepository userRepository { get; init; }
    public ICityRepository cityRepository { get; init; }
}
