namespace EZPos.Web.Ui.Services.State;

public sealed class ThemeState
{
    public bool IsDarkMode { get; private set; } = true;

    public event Action? Changed;

    public void SetDarkMode(bool value)
    {
        if (IsDarkMode == value)
        {
            return;
        }

        IsDarkMode = value;
        Changed?.Invoke();
    }
}
