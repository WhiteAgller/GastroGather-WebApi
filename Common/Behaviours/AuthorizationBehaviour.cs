﻿using System.Reflection;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }
        }

        // User is authorized / authorization not required
        return next();
    }
}