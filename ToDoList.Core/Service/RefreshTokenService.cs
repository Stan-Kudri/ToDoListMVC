using ToDoList.Core.DBContext;
using ToDoList.Core.Models;

namespace ToDoList.Core.Service
{
    public class RefreshTokenService
    {
        private readonly AppDbContext _appDbContext;
        private readonly TokenService _tokenService;

        public RefreshTokenService(AppDbContext appDbContext, TokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public void Upsert(RefreshToken refreshToken)
        {
            if (!_appDbContext.Users.Any(e => e.Id == refreshToken.UserId))
            {
                throw new ArgumentException("This user does not exist.");
            }

            var isTokenUsed = _appDbContext.RefreshTokens.Any(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (isTokenUsed)
            {
                return;
            }

            _appDbContext.RefreshTokens.Add(refreshToken);
            _appDbContext.SaveChanges();
        }

        public void Update(RefreshToken refreshToken, out RefreshToken updatedToken)
        {
            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (item == null)
            {
                throw new ArgumentException("This refresh token does not exist.");
            }

            updatedToken = _tokenService.GenerateRefreshToken(refreshToken.Id);

            item.Token = updatedToken.Token;
            item.Expires = updatedToken.Expires;
            item.Create = updatedToken.Create;

            _appDbContext.RefreshTokens.Update(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(RefreshToken? refreshToken)
        {
            var item = _appDbContext.RefreshTokens
                                        .FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id)
                                        ?? throw new InvalidOperationException("Interaction element not found.");

            _appDbContext.RefreshTokens.Remove(item);
            _appDbContext.SaveChanges();
        }

        public void Remove(string token, Guid userId)
        {
            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

            if (item != null)
            {
                _appDbContext.RefreshTokens.Remove(item);
            }
        }

        public bool IsValidRefreshToken(RefreshToken refreshToken)
            => _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id) != null;

        public RefreshToken? GetRefreshToken(string token, Guid userId)
        {
            var refreshToken = _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

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
            => _appDbContext.RefreshTokens.Any(e => e.Token == refreshToken.Token && e.UserId == refreshToken.UserId);

        private void RemoveExpiredTokens(Guid userId)
        {
            var item = _appDbContext.RefreshTokens.Where(e => e.UserId == userId && DateTime.Now > e.Expires).ToList();

            if (item.Count == 0)
            {
                return;
            }

            _appDbContext.RefreshTokens.RemoveRange(item);
            _appDbContext.SaveChanges();
        }
    }
}
