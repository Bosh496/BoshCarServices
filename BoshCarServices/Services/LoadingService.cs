namespace BoshCarServices.Services
{
    public class LoadingService
    {
        public bool IsLoading { get; private set; }

        public event Action OnChange;

        public void Show()
        {
            IsLoading = true;
            Notify();
        }

        public void Hide()
        {
            IsLoading = false;
            Notify();
        }

        private void Notify() => OnChange?.Invoke();
    }
}
