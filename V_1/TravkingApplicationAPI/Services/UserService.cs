using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class UserService
    {
        public IUser User;
        public UserService(IUser User) {
    this.User = User;
}

public async Task<string> LoginUser(Login user){

     try
     {
        
         return await User.LoginUser(user);
     }
     catch(Exception ex) {
         throw;
     }
 }

 public async Task<string> AddUser(AddUser user)
        {
            try{
                return await User.AddUser(user);


            }
            catch(Exception ex){
                throw;
            }
            }
    }
}