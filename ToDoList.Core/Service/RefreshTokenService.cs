using ToDoList.Core.DBContext;
using ToDoList.Core.Models;

namespace ToDoList.Core.Service
{
    public class RefreshTokenService(AppDbContext appDbContext, TokenService tokenService)
    {
        public void Upsert(RefreshToken refreshToken)
        {
            if (!appDbContext.Users.Any(e => e.Id == refreshToken.UserId))
            {
                throw new ArgumentException("This user does not exist.");
            }

            var isTokenUsed = appDbContext.RefreshTokens.Any(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (isTokenUsed)
            {
                return;
            }

            appDbContext.RefreshTokens.Add(refreshToken);
            appDbContext.SaveChanges();
        }

        public void Update(RefreshToken refreshToken, out RefreshToken updatedToken)
        {
            var item = appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (item == null)
            {
                throw new ArgumentException("This refresh token does not exist.");
            }

            updatedToken = tokenService.GenerateRefreshToken(refreshToken.Id);

            item.Token = updatedToken.Token;
            item.Expires = updatedToken.Expires;
            item.Create = updatedToken.Create;

            appDbContext.RefreshTokens.Update(item);
            appDbContext.SaveChanges();
        }

        public void Remove(RefreshToken? refreshToken)
        {
            var item = appDbContext.RefreshTokens
                                        .FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id)
                                        ?? throw new InvalidOperationException("Interaction element not found.");

            appDbContext.RefreshTokens.Remove(item);
            appDbContext.SaveChanges();
        }

        public void Remove(string token, Guid userId)
        {
            var item = appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

            if (item != null)
            {
                appDbContext.RefreshTokens.Remove(item);
            }
        }

        public bool IsValidRefreshToken(RefreshToken refreshToken)
            => appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id) != null;

        public RefreshToken? GetRefreshToken(string token, Guid userId)
        {
            var refreshToken = appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

            if (refreshToken == null || refreshToken.Expired)
            {
                Remove(refreshToken);
                return null;
            }

            //Remove RefreshToken Expires by UserId
            RemoveExpiredTokens(refreshToken.UserId);

            return refreshToken;
        }

        public bool IsExistRefreshToken(RefreshToken refreshToken)
            => appDbContext.RefreshTokens.Any(e => e.Token == refreshToken.Token && e.UserId == refreshToken.UserId);

        private void RemoveExpiredTokens(Guid userId)
        {
            var item = appDbContext.RefreshTokens.Where(e => e.UserId == userId && DateTime.Now > e.Expires).ToList();

            if (item.Count == 0)
            {
                return;
            }

            appDbContext.RefreshTokens.RemoveRange(item);
            appDbContext.SaveChanges();
        }
    }
}
