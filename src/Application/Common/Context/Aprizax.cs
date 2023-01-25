using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Context
{
    public static class Aprizax
    {
        public static async Task<TEntity> Insert<TEntity>
        (IApplicationDbContext context, DbSet<TEntity> tableContext, TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
        {
            tableContext.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}