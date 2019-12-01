using PollAPICorrect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
