﻿using CIPlatform.Entities.Data;
using CIPlatform.Entities.Models;
using CIPlatform.Entities.ViewModel;
using CIPlatform.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly CiPlatformContext _DbContext;

        public UserRepository(CiPlatformContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<User> UserList()
        {
            List<User> objUserList = _DbContext.Users.ToList();
            return objUserList;
        }

    

        public Boolean IsEmailAvailable(string email)
        {
            return _DbContext.Users.Any(u => u.Email == email);
        }

        public User IsPasswordAvailable(string password, string email)
        {
            return _DbContext.Users.Where(u => u.Password == password && u.Email == email).FirstOrDefault();
        }



        public long GetUserID(string Email)
        {
            User user = _DbContext.Users.Where(u => u.Email == Email).FirstOrDefault();
            if (user == null)
            {

                return -1;
            }
            else
            {

                return user.UserId;
            }
        }

       
        public bool ResetPassword(long userId, string OldPassword, string NewPassword)
        {
            try
            {
                User user = _DbContext.Users.Find(userId);
                string pass = (user.Password);
                if (pass == OldPassword)
                {
                    user.Password = (NewPassword);
                    _DbContext.Users.Update(user);
                    _DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Boolean ChangePassword(long UserId, Reset_Password model)
        {
            User user = _DbContext.Users.FirstOrDefault(x => x.UserId == model.UserId);
            user.Password = model.Password;
            user.UpdatedAt = DateTime.Now;
            _DbContext.Users.Update(user);
            _DbContext.SaveChanges();
            return true;

        }
        public User GetUser(int userID)
        {
            try
            {

                User user = _DbContext.Users.Where(u => u.UserId == userID).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                else
                {

                    return null;

                }


            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool Register(User objUser)
        {
            if(_DbContext.Users.Any(u => u.Email != objUser.Email))
            {
                _DbContext.Users.Add(objUser);
                _DbContext.SaveChanges();
                return true;
            }
            return false;

            
                
            
        }

        public void Login(User objLogin)
        {
            if (_DbContext.Users.Any(u => u.Email == objLogin.Email && u.Password == objLogin.Password))
            {

            }

        }

        public List<User> GetUserlist(long UserId)
        {
            var getuser = _DbContext.Users.Include(x => x.City).Include(x => x.Country).Where(x => x.UserId == UserId).ToList();
            return getuser;
        }

        public List<Country> GetCountryList()
        {
            var getCountry = _DbContext.Countries.ToList();
            return getCountry;
        }

        public List<City> GetCities()
        {
            var getcity = _DbContext.Cities.ToList();
            return getcity;
        }

        public List<City> CityList(long CountryID)
        {
            List<City> objCityList = _DbContext.Cities.Where(i => i.CountryId == CountryID).ToList();
            return objCityList;
        }

    }
}
