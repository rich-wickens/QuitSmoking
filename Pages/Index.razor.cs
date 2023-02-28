using QuitSmoking.Data;
using Microsoft.EntityFrameworkCore;

namespace QuitSmoking.Pages
{
    public partial class Index
    {
        public bool ShowCreate { get; set; }
        public bool EditRecord { get; set; }
        public int EditingId { get; set; }

        private UserDataContext? _context;
        public User? UserToUpdate { get; set; }
        public User? NewUser { get; set; }
        public List<User>? OurUsers { get; set; }

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
            await ShowUsers();
        }

        public async Task ShowEditForm(User ourUser)
        {
            _context ??= await UserDataContextFactory.CreateDbContextAsync();

            if(_context is not null)
            {
                UserToUpdate = _context.Users.FirstOrDefault(x => x.Id == ourUser.Id);
                EditingId = ourUser.Id;
                EditRecord = true;  
            }
        }

        public async Task ShowUsers()
        {
            _context ??= await UserDataContextFactory.CreateDbContextAsync();

            if (_context is not null)
            {
                OurUsers = await _context.Users.ToListAsync();
            }
        }

        public async Task UpdateUser()
        {
            _context ??= await UserDataContextFactory.CreateDbContextAsync();
            if (_context is not null)
            {
                if (UserToUpdate is not null) _context.Users.Update(UserToUpdate);
                await _context.SaveChangesAsync();
            }
            EditRecord = false;
        }
    }

}
