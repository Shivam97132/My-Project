﻿using AirlineWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AilrineWebAPI.Repository.RegisterUsersRepository
{
    public interface IRegisterUsersRepository
    {
        Task<ActionResult<IEnumerable<RegisterUser>>> Getusers();
        Task<ActionResult<RegisterUser>> GetRegisterUser(int id);
        Task<RegisterUser> GetRegisterUserByPwd(string email, string password);
        Task<ActionResult<RegisterUser>> PostRegisterUser(RegisterUser registerUser);
        Task<ActionResult<RegisterUser>> DeleteRegisterUser(int id);
        Task<bool> CheckEmailExists(string email);
        bool RegisterUserExists(int id);
    }
}