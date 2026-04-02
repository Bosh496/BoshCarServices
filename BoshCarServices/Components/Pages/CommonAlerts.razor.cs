using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BoshCarServices.Components.Pages
{
    public partial class CommonAlerts : ComponentBase
    {
        #region Parameters
        [Parameter] public bool IsLoading { get; set; }
        #endregion
        #region Interface Injection
        [Inject] ISnackbar Snackbar { get; set; }
        #endregion
        #region Methods
        public void ShowSnackbar(string message, Severity severity, string position, int visibledurationMiliSec = 3000)
        {
            try
            {
                Snackbar.Configuration.PositionClass = position;
                Snackbar.Add(message, severity, config =>
                {
                    config.ShowCloseIcon = true;
                    config.VisibleStateDuration = visibledurationMiliSec;
                    config.HideTransitionDuration = 500;
                    config.ShowTransitionDuration = 800;
                    config.SnackbarVariant = Variant.Filled;
                });
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
