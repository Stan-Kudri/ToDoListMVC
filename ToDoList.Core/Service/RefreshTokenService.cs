﻿using ToDoList.Core.DBContext;
using ToDoList.Core.Extension;
using ToDoList.Core.Models;

namespace ToDoList.Core.Service
{
    public class RefreshTokenService
    {
        private readonly AppDbContext _appDbContext;

        public RefreshTokenService(AppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public void Add(RefreshToken refreshToken)
        {
            var user = _appDbContext.Users.FirstOrDefault(e => e.Id == refreshToken.UserId);

            if (user == null)
            {
                throw new ArgumentException("This user does not exist.");
            }

            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId);

            if (item != null)
            {
                throw new ArgumentException("User with the same name already exists.");
            }

            _appDbContext.RefreshTokens.Add(refreshToken);
            _appDbContext.SaveChanges();
        }

        public void Uppdata(RefreshToken refreshToken)
        {
            var user = _appDbContext.Users.FirstOrDefault(e => e.Id == refreshToken.UserId);

            if (user == null)
            {
                throw new ArgumentException("This user does not exist.");
            }

            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId);

            if (item == null)
            {
                throw new ArgumentException("This user does not exist.");
            }

            item.Token = refreshToken.Token;
            _appDbContext.RefreshTokens.Update(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == id) ?? throw new InvalidOperationException("Interaction element not found.");
            _appDbContext.RefreshTokens.Remove(item);
            _appDbContext.SaveChanges();
        }

        public RefreshToken? GetRefreshToken(string token, Guid userId)
        {
            var refreshToken = _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

            if (refreshToken == null || !refreshToken.IsExpiredRefreshToken())
            {
                return null;
            }

            return refreshToken;

        }

        public bool IsUserIdExist(Guid userId)
            => _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == userId) != null;

        public bool IsExistRefreshToken(string refreshToken, Guid userId)
            => _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == refreshToken && e.UserId == userId) == null;
    }
}