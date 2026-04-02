using BoshCarServices.Data.Entities;

namespace BoshCarServices.Components.Pages
{
    public partial class Login
    {
        LoginMaster loginModel = new();

        public void LoginUser()
        {
            var user = _context.LoginMasters
                        .FirstOrDefault(x =>
                            x.Username == loginModel.Username &&
                            x.Password == loginModel.Password &&
                            x.IsActive);

            if (user != null)
            {
                nav.NavigateTo("/dashboard");
            }
        }

        void Clear()
        {
            loginModel = new LoginMaster();
        }
    }
}
