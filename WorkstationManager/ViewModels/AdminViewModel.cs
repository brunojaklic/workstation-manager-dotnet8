using WorkstationManager.Models;

namespace WorkstationManager.ViewModels
{
    internal class AdminViewModel
    {
        private User user;

        public AdminViewModel(User user)
        {
            this.user = user;
        }
    }
}