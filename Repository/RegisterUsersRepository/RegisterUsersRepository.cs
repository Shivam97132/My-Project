using AirlineWebAPI.Controllers;
using AirlineWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections;
using AilrineWebAPI.Repository.RegisterUsersRepository;
using System.Security.Cryptography.X509Certificates;

namespace AirlineWebAPI.Repository.RegisterUsersRepository
{
    public class RegisterUsersRepository : IRegisterUsersRepository
    {
        private readonly AirlineDbContext _context;
        private readonly ILogger<RegisterUsersRepository> _logger;

        public RegisterUsersRepository(AirlineDbContext context, ILogger<RegisterUsersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<IEnumerable<RegisterUser>>> Getusers()
        {
            _logger.LogInformation("Getting all the users successfully.");
            return await _context.users.ToListAsync();
        }

        public async Task<ActionResult<RegisterUser>> GetRegisterUser(int id)
        {
            var registerUser = await _context.users.FindAsync(id);
            if (registerUser == null)
            {
                throw new NullReferenceException("Sorry, no user found with this id " + id);
            }
            else
            {
                return registerUser;
            }
        }

        public async Task<RegisterUser> GetRegisterUserByPwd(string email, string password)
        {
            //var user = await _context.users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            //if (user == null)
            //{
            //    throw new NullReferenceException();
            //}
            //else
            //{
            //    return user;
            //}
            var user = await _context.users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<bool> CheckEmailExists(string email)
        {
            // Use LINQ to query the database and check if an email exists
            bool emailExists = await _context.users.AnyAsync(u => u.Email == email);

            return emailExists;
        }

        public async Task<ActionResult<RegisterUser>> PostRegisterUser(RegisterUser registerUser)
        {
           
            _context.users.Add(registerUser);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User created successfully.");

            return registerUser;
        }

        public async Task<ActionResult<RegisterUser>> DeleteRegisterUser(int id)
        {
            var registerUser = await _context.users.FindAsync(id);

            if (registerUser == null)
            {
                throw new NullReferenceException("Sorry, no user found with this id " + id);
            }
            else
            {
                _context.users.Remove(registerUser);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User deleted successfully.");

                return registerUser;
            }
        }

        public bool RegisterUserExists(int id)
        {
            return _context.users.Any(e => e.UserId == id);
        }
    }
}