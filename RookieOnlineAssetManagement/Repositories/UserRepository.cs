
using AutoMapper;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Interface;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UserRepository(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserModel>>(users);
        }
        public async Task<byte> PostAsync(UserDTO model, User user)
        {
            string[] splitlastname = model.LastName.Split(' ');
            string sum = "";
            foreach (string slice in splitlastname)
                if (slice.Length > 0)
                {
                    sum += slice[0].ToString().ToLower();
                }
            model.UserName = model.FirstName.ToLower() + sum;
            var duplicatename = await _context.Users.FirstOrDefaultAsync(p => p.UserName == model.UserName);


            int count = 0;
            string newname = model.UserName;
            while (duplicatename != null)
            {
                count++;
                newname = (model.UserName + count.ToString());
                duplicatename = await _context.Users.FirstOrDefaultAsync(p => p.UserName == newname);

            }
            model.UserName = newname;
            DateTime DOB = DateTime.Parse(model.DateofBirth.ToString());
            DateTime today = DateTime.Today;
            if (today <= DOB.AddYears(18))
            {
                return 1;
            }
            DateTime JD = DateTime.Parse(model.JoinedDay.ToString());
            if (JD.Year < DOB.Year || JD.Year == DOB.Year && JD.Month > DOB.Month || JD.Year == DOB.Year && JD.Month == DOB.Month && JD.Date > DOB.Date)
            {
                return 2;
            }
            if (model.Gender is not Enum.Gender.Female and not Enum.Gender.Male)
            {
                return 3;
            }

            if (model.Type is not Enum.UserType.Admin and not Enum.UserType.Staff)
            {
                return 4;
            }
            if(JD.DayOfWeek == DayOfWeek.Saturday || JD.DayOfWeek == DayOfWeek.Sunday )
            {
                return 5;
            }    
            
            //var users = user;
            //model.Location = users.Location;

            var password = new PasswordHasher<UserDTO>();
            var hashed = password.HashPassword(model, $"{model.UserName}@{model.DateofBirth.ToString("ddMMyyyy")}");
            model.PasswordHash = hashed;

            int total = await _context.Users.CountAsync();
            if (total >= 0)
            {
                total++;
                model.StaffCode = "SD" + total.ToString().PadLeft(4, '0');
            }
          
            _context.Users.Add(_mapper.Map<User>(model));
            _context.SaveChanges();
            return 0;
        }

    }
}
