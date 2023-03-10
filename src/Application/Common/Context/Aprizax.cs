using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Common.Context
{
    public record UseAprizax
    {
        [FromHeader(Name = "Authorization")]
        public string Token { get; init; } = "Bearer ";
    }

    public record TokenInfo
    {
        public int? Owner_Id;
        public bool Is_Valid = false;
    }

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
        public static string? GetToken(this UseAprizax aprizax)
        {
            var requestToken = aprizax.Token.Remove(0, "Bearer ".Length);
            if (String.IsNullOrEmpty(requestToken)) return null;
            return requestToken;
        }
        public static int? GetOwnerId(this UseAprizax aprizax)
        {
            var requestToken = aprizax.Token.Remove(0, "Bearer ".Length);

            if (aprizax.GetToken() is null)
                throw new NotFoundException("Token Tidak Ada Harap Login Kembali", HttpStatusCode.BadRequest);

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = secretKey,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken
                (requestToken, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var user_Id = jwtToken.Claims.First(x => x.Type == "id").Value;
                return int.Parse(user_Id);
            }
            catch
            {
                return null;
            }
        }
        public static TokenInfo GetTokenInfo(this UseAprizax aprizax)
        {
            var owner_Id = aprizax.GetOwnerId();
            if (owner_Id is null) return new TokenInfo();
            return new TokenInfo()
            {
                Owner_Id = owner_Id,
                Is_Valid = true
            };
        }

        public static IOrderedQueryable<TSource> OrderBySwitch<TSource, TKey>
                (this IQueryable<TSource> source,
                 Expression<Func<TSource, TKey>> keySelector,
                 bool isAsc)
        {
            return isAsc ?
            source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static async Task<string?> GetCurrentQuantity(this DbSet<Cart> carts, int? userId)
        {
            if (userId is null)
                throw new NullReferenceException("Pastikan User Id telah terisi");

            var quantityCount = await carts
                .Where(x => x.User_Id.Equals(userId))
                .AsNoTracking()
                .SumAsync(y => y.Quantity);

            if (quantityCount.Equals(0)) return null;
            return Convert.ToString(quantityCount);
        }
        public static string? GetFormattedDate(this DateTime date)
        {
            var format = new CultureInfo("id-ID");
            return date.ToString("dddd, dd MMMM yyyy", format);
        }

        public static string? GetFormattedDateGraph(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static int GetDataComparison(int dataComparison1, int dataComparison2, int dataMaks)
        {
            var data1 = dataComparison1 * dataComparison1 / dataMaks;
            var data2 = dataComparison2 * dataComparison2 / dataMaks;
            var entity = data1 - data2;
            return entity - 1;
        }
    }
}