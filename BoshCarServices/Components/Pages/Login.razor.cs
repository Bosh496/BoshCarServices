using BoshCarServices.Components.Shared;
using BoshCarServices.Data.Entities;

namespace BoshCarServices.Components.Pages
{
    public partial class Login
    {
        LoginMaster loginModel = new();

        public async void LoginUser()
        {
            LoaderService.Show();
            await Task.Delay(800);
            var user = _context.LoginMasters
                        .FirstOrDefault(x =>
                            x.Username == loginModel.Username &&
                            x.Password == loginModel.Password &&
                            x.IsActive);

            if (user != null)
            {
               
                nav.NavigateTo("/dashboard");
                LoaderService.Hide();
            }
        }

        void Clear()
        {
            loginModel = new LoginMaster();
        }
    }
}
