using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Packless.Dtos;
using Packless.Models;

namespace Packless.Database
{
    public class AuthRepository : IAuthRepository
    {
        private readonly PacklessDbContext _context;

        public AuthRepository(PacklessDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        /// <summary>
        /// Passwort Hash Verfizieren
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Neuen Benutzer anlegen
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ChangePassword(UserForRegisterDto user, string password)
        {
            bool exists = await UserExists(user.Username);
            if (!exists)
                return false;

            var foundUser = await _context.Users.Where(u => u.Username == user.Username).FirstAsync();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            foundUser.PasswordHash = passwordHash;
            foundUser.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Erzeugt Passwort Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Prüft ob einzelner Benutzer existiert
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        /// <summary>
        /// Alle Benutzernamen ermitteln
        /// </summary>
        /// <returns></returns>
        public async Task<List<String>> GetUsersAsync()
        {
            return await _context.Users.Select(u => u.Username).ToListAsync();
        }

        /// <summary>
        /// Benutzer löschen
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string username)
        {
            var exists = await UserExists(username);
            if (exists)
            {
                if (_context.Users.Count() == 1)
                    return false;

                var user = await _context.Users.FirstAsync(x => x.Username == username);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
