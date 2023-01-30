using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public static int angka = 1;
        public static async Task<TEntity> Insert<TEntity>
        (this IApplicationDbContext context, DbSet<TEntity> tableContext, TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
        {
            Console.WriteLine(angka);
            tableContext.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public static IOrderedQueryable<TSource> OrderBySwitch<TSource, TKey>
                (this IQueryable<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector,
                 bool isAsc)
        {
            return isAsc ?
            source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }
    }
}