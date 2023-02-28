using QuitSmoking.Data;
using Microsoft.EntityFrameworkCore;

namespace QuitSmoking.Pages
{
    public partial class Index
    {
        public bool ShowCreate { get; set; }
        private UserDataContext? _context;
        public User? NewUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await ShowUsers();
        }

        public void ShowCreateForm()
        {
            NewUser = new User();
            ShowCreate = true;
        }

        public async Task CreateNewUser()
        {
            _context ??= await UserDataContextFactory.CreateDbContextAsync();

            if (NewUser is not null)
            {
                _context?.Users.Add(NewUser);
                _context?.SaveChangesAsync();
            }

            ShowCreate = false;
        }

        public List<User>? OurUsers { get; set; }

        public async Task ShowUsers()
        {
            _context ??= await UserDataContextFactory.CreateDbContextAsync();

            if (_context is not null)
            {
                OurUsers = await _context.Users.ToListAsync();
            }

            if (_context is not null) await _context.DisposeAsync();
        }
    }

}
