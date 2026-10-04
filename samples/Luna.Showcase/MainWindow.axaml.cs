using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Luna;

namespace Luna.Showcase;
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
        InitializeNavigation();
        InitializePalette();
        var name = this.FindControl<TextBox>("NameInput")!;
        var level = this.FindControl<Slider>("Level")!;
        var result = this.FindControl<TextBlock>("Result")!;
        // 示例只演示真实交互，不为主题库引入额外业务框架。
        this.FindControl<Button>("ApplyButton")!.Click += (_, _) => result.Text = $"已应用：{name.Text}，进度 {level.Value:0}%。";
        this.FindControl<Button>("ResetButton")!.Click += (_, _) =>
        {
            name.Text = "Luna 使用者";
            level.Value = 62;
            this.FindControl<ComboBox>("ColorChoice")!.SelectedIndex = 0;
            this.FindControl<CheckBox>("Sounds")!.IsChecked = true;
            result.Text = "已重置主要设置。";
        };
        this.FindControl<ListBox>("DemoList")!.ItemsSource = Enumerable.Range(1, 40).Select(i => $"示例项目 {i:00} — Luna 桌面控件").ToArray();
    }

    private void InitializePalette()
    {
        var selector = this.FindControl<ComboBox>("ThemeSelector")!;
        void Apply()
        {
            if (selector.SelectedIndex is < 0 or > 2) return;
            var variant = (LunaVariant)selector.SelectedIndex;
            ((App)Avalonia.Application.Current!).Luna.Variant = variant;
            var english = variant switch { LunaVariant.Blue => "Blue", LunaVariant.Silver => "Silver", _ => "Olive Green" };
            var chinese = variant switch { LunaVariant.Blue => "蓝色", LunaVariant.Silver => "银色", _ => "橄榄绿" };
            Title = $"Luna · Windows XP {chinese}主题";
            this.FindControl<TextBlock>("ThemeHeadline")!.Text = $"熟悉的{chinese}，新的 Avalonia。";
            this.FindControl<TextBlock>("ThemeSubtitle")!.Text = $"Luna {english} / 交互式控件展示";
            this.FindControl<TextBlock>("ThemeFooter")!.Text = $"Luna {english}  ·  Avalonia 12  ·  原生桌面交互";
        }
        selector.SelectionChanged += (_, _) => Apply();
        Apply();
    }

    // 所有修改只作用于示例节点，不访问真实文件。
    private void InitializeNavigation()
    {
        var tree = this.FindControl<TreeView>("DesktopTree")!;
        var input = this.FindControl<TextBox>("RenameInput")!;
        var status = this.FindControl<TextBlock>("DesktopStatus")!;
        var rename = this.FindControl<Button>("RenameButton")!;
        var root = this.FindControl<TreeViewItem>("WorkspaceNode")!;
        var context = tree.ContextMenu!;
        MenuItem Item(string name) => this.FindControl<MenuItem>(name)!;
        void ShowSelection()
        {
            if (tree.SelectedItem is TreeViewItem selected)
            {
                input.Text = selected.Header?.ToString();
                status.Text = $"已选择：{selected.Header}";
                rename.IsEnabled = selected.IsEnabled;
            }
        }
        void BeginRename() { input.Focus(); input.SelectAll(); }
        void SetExpanded(TreeViewItem item, bool expanded)
        {
            if (item.Items.Count > 0) item.IsExpanded = expanded;
            foreach (var child in item.Items.OfType<TreeViewItem>()) SetExpanded(child, expanded);
        }
        rename.Click += (_, _) =>
        {
            if (tree.SelectedItem is TreeViewItem selected && !string.IsNullOrWhiteSpace(input.Text))
            {
                selected.Header = input.Text.Trim();
                status.Text = $"已重命名：{selected.Header}";
            }
        };
        Item("MenuRename").Click += (_, _) => BeginRename();
        Item("ContextRename").Click += (_, _) => BeginRename();
        Item("MenuExpand").Click += (_, _) => SetExpanded(root, true);
        Item("MenuCollapse").Click += (_, _) => SetExpanded(root, false);
        Item("ContextExpand").Click += (_, _) => { if (tree.SelectedItem is TreeViewItem node) node.IsExpanded = true; };
        Item("ContextCollapse").Click += (_, _) => { if (tree.SelectedItem is TreeViewItem node) node.IsExpanded = false; };
        void Explain() => status.Text = "选择项目后可重命名。方向键浏览目录；右键打开菜单。";
        Item("MenuHelp").Click += (_, _) => Explain();
        this.FindControl<Button>("TipButton")!.Click += (_, _) => Explain();
        Item("MenuClose").Click += (_, _) => Close();
        tree.PropertyChanged += (_, change) => { if (change.Property == TreeView.SelectedItemProperty) ShowSelection(); };
        tree.PointerPressed += (_, e) =>
        {
            if (!e.GetCurrentPoint(tree).Properties.IsRightButtonPressed) return;
            var node = (e.Source as Avalonia.Visual)?.GetSelfAndVisualAncestors().OfType<TreeViewItem>().FirstOrDefault();
            if (node?.IsEnabled == true) tree.SelectedItem = node;
        };
        context.Opened += (_, _) =>
        {
            bool branch = tree.SelectedItem is TreeViewItem node && node.Items.Count > 0;
            Item("ContextExpand").IsEnabled = branch;
            Item("ContextCollapse").IsEnabled = branch;
        };
        Opened += (_, _) => { tree.SelectedItem = this.FindControl<TreeViewItem>("PlanNode"); ShowSelection(); };
    }
}
