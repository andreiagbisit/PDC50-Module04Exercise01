using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module02Exercise01.Services
{
    public interface IMyService
    {
        string LoginMessage();
        string OfflineMessage();
    }

    public class MyService : IMyService 
    {
        public string LoginMessage()
        {
            return "Welcome to the Employee View App";
        }
        public string OfflineMessage()
        {
            return "Your device seems to be offline. Please check your internet connection.";
        }
    }
}
