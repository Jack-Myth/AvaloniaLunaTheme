using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Luna;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

namespace Luna.Showcase;
public sealed class App : Application
{
    public LunaTheme Luna { get; private set; } = null!;
    public override void Initialize()
    {
        RequestedThemeVariant = ThemeVariant.Light;
        // 保留 Fluent 的输入、弹出层、无障碍和键盘行为，再覆盖 Luna 外观。
        Styles.Add(new FluentTheme());
        Luna = new LunaTheme();
        Styles.Add(Luna);
    }
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow();
        base.OnFrameworkInitializationCompleted();
    }
}
