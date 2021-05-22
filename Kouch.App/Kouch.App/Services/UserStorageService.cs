using Kouch.App.Entities;

namespace Kouch.App.Models
{

    public class UserStorageService
    {
        public delegate void UserChangeEvent(User u);
        public event UserChangeEvent OnUserChahged;

        public User User
        {
            get => user; set
            {
                user = value;
                OnUserChahged?.Invoke(user);
            }
        }
        private static UserStorageService _instance;

        private User user;

        public static UserStorageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserStorageService();
                }

                return _instance;
            }
        }
        private UserStorageService()
        {

        }
    }
}
