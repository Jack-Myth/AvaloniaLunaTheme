# Avalonia Luna Theme

原创 Windows XP Luna 风格主题，支持 Blue、Silver、Olive Green，默认 Blue。基于 Avalonia 12.1.3 / .NET 10。源码包含主题库 `src/Avalonia.Luna` 与展示应用 `samples/Luna.Showcase`。

```powershell
dotnet run --project samples/Luna.Showcase
```

展示窗口右上角可以实时切换配色；“常用控件”页的配色选择同步更新。

接入已有 Avalonia 12.1.3 桌面应用时，引用 `src/Avalonia.Luna/Avalonia.Luna.csproj`，应用保留 `Avalonia.Desktop`，在 `Application.Initialize()` 中按下列顺序加载主题。普通 `Window` 会自动获得 Luna 装饰，无需复制 Showcase 的 XAML 或初始化代码。目前未发布公共 NuGet 包。

```csharp
using Avalonia.Luna;
RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;
Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
var luna = new LunaTheme(); // 也可 new LunaTheme(LunaVariant.Silver)
Styles.Add(luna);
luna.Variant = LunaVariant.OliveGreen; // 在 UI 线程调用
```

三种配色共用 XAML 模板；`LunaPalette.cs` 集中定义画刷与渐变，切换时原位更新共享画刷，让已经打开的菜单、右键菜单、组合框及提示跟随配色。Silver 使用紫灰色金属渐变和深色标题字，Olive Green 使用橄榄绿和暖米色；菜单与树的选择高亮保留 XP 蓝色。

普通 `Window` 使用框架 `WindowDecorationsTheme`，保留拖动、缩放、双击及最小化／最大化／关闭行为。顶部 8 DIP 透明圆角，最大化恢复方角；三种配色都有激活／未激活状态。覆盖按钮、文本框、复选框、单选框、组合框、标签页、进度条、横竖滑块与滚动条，以及菜单／子菜单、右键菜单、ToolTip、GroupBox 和树控件。输入使用 28 DIP 最小高度、Tahoma／微软雅黑回退；有悬停、按下、禁用和键盘焦点状态。

“菜单与导航”页支持选择、重命名、展开／折叠和共享说明开关；只修改内存节点，不访问文件。控件保留框架 Popup、Track、键盘导航与弹出定位。滚动条独立占位，握柄凸纹固定尺寸并居中。

当前针对 Windows 10 浅色主题，未覆盖控件仍使用 Fluent；树连接线／文件图标、标题栏图标与系统窗口换肤尚未实现。配色为参考 XP 外观的原创近似，不提取 Windows 位图、不复制 XP.css / Classic.Avalonia 代码。许可证见 LICENSE 和 THIRD-PARTY-NOTICES。

程序化交互检查包括三种配色的控件状态、菜单生命周期、打开的弹出层换色、重复切换、恢复 Blue、圆角与最大化，以及 125%／150%／200% 应用内离屏渲染。未改系统显示设置；真实鼠标操作和跨显示器 DPI 切换仍需人工确认。项目需要 .NET 10 SDK；依赖由 NuGet 还原。
