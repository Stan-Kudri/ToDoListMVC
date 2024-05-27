using ToDoList.Core.DBContext;
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

            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (item != null)
            {
                Update(refreshToken);
                return;
            }

            _appDbContext.RefreshTokens.Add(refreshToken);
            _appDbContext.SaveChanges();
        }

        public void Update(RefreshToken refreshToken)
        {
            var user = _appDbContext.Users.FirstOrDefault(e => e.Id == refreshToken.UserId);

            if (user == null)
            {
                throw new ArgumentException("This user does not exist.");
            }

            var item = _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id);

            if (item == null)
            {
                throw new ArgumentException("This user does not exist.");
            }

            item.Token = refreshToken.Token;
            item.Expires = refreshToken.Expires;
            item.Create = refreshToken.Create;

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

        public RefreshToken? GetRefreshToken(string token, Guid userId)
        {
            var refreshToken = _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == token && e.UserId == userId);

            if (refreshToken == null || refreshToken.Expired)
            {
                Remove(refreshToken);
                return null;
            }

            //Remove RefreshToken Expires by UserId
            RemoveRangeTokensExpired(refreshToken);

            return refreshToken;
        }

        public bool IsUserIdExist(RefreshToken refreshToken)
            => _appDbContext.RefreshTokens.FirstOrDefault(e => e.UserId == refreshToken.UserId && e.Id == refreshToken.Id) != null;

        public bool IsExistRefreshToken(RefreshToken refreshToken)
            => IsExistRefreshToken(refreshToken.Token, refreshToken.UserId);

        public bool IsExistRefreshToken(string refreshToken, Guid userId)
            => _appDbContext.RefreshTokens.FirstOrDefault(e => e.Token == refreshToken && e.UserId == userId) != null;

        private void RemoveRangeTokensExpired(RefreshToken refreshToken)
        {
            var item = _appDbContext.RefreshTokens.Where(e => e.UserId == refreshToken.UserId && DateTime.Now > e.Expires).ToList();

            if (item.Count == 0)
            {
                return;
            }

            _appDbContext.RefreshTokens.RemoveRange(item);
        }
    }
}
