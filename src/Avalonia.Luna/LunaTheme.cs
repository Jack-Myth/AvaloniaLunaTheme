using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Media;

namespace Avalonia.Luna;

// 将配色、控件样式与框架窗口装饰一起载入；应用无需自己处理标题栏鼠标事件。
public sealed class LunaTheme : Styles
{
    private readonly ResourceDictionary _palette = new();
    private LunaVariant _variant;

    public LunaTheme() : this(LunaVariant.Blue) { }

    public LunaTheme(LunaVariant variant)
    {
        AvaloniaXamlLoader.Load(this);
        Resources.MergedDictionaries.Add(_palette);
        ApplyVariant(variant);
    }

    /// <summary>在 UI 线程切换配色；现有窗口和弹出层通过动态资源同步更新。</summary>
    public LunaVariant Variant
    {
        get => _variant;
        set
        {
            Dispatcher.UIThread.VerifyAccess();
            if (!Enum.IsDefined(value)) throw new ArgumentOutOfRangeException(nameof(value));
            if (_variant != value) ApplyVariant(value);
        }
    }

    private void ApplyVariant(LunaVariant variant)
    {
        var values = LunaPalette.Create(variant);
        if (_palette.Count == 0)
            _palette.SetItems(values);
        else
        {
            // Popup 可能缓存资源对象。保留画刷身份并修改颜色，让现有弹出层也收到绘制更新。
            foreach (var (key, value) in values)
            {
                if (_palette[key] is SolidColorBrush brush && value is SolidColorBrush next)
                    brush.Color = next.Color;
                else if (_palette[key] is LinearGradientBrush gradient && value is LinearGradientBrush nextGradient)
                {
                    gradient.StartPoint = nextGradient.StartPoint;
                    gradient.EndPoint = nextGradient.EndPoint;
                    for (int i = 0; i < gradient.GradientStops.Count; i++)
                    {
                        gradient.GradientStops[i].Color = nextGradient.GradientStops[i].Color;
                        gradient.GradientStops[i].Offset = nextGradient.GradientStops[i].Offset;
                    }
                }
            }
        }
        _variant = variant;
    }
}
