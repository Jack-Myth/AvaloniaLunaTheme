using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia.Luna;

// 原创调色：保留 Blue 原值；Silver 为紫灰，Olive 为暖橄榄绿。选择高亮仍为 XP 蓝。
internal static class LunaPalette
{
    private static readonly string[,] Colors =
    {
        // 资源名                       Blue        Silver      Olive Green
        { "LunaSurface",               "#ECE9D8", "#E4E4E9", "#ECECD9" },
        { "LunaPaper",                 "#FCFCF8", "#F8F8FB", "#FCFCF5" },
        { "LunaInsetSurface",          "#F7F5EB", "#F1F1F6", "#F5F5E6" },
        { "LunaProgressTrack",         "#FAFAF5", "#FAFAFC", "#FAFAF2" },
        { "LunaHeading",               "#003C74", "#42465B", "#4F6032" },
        { "LunaMutedText",             "#6A695D", "#6C6D7A", "#70775A" },
        { "LunaFrame",                 "#0854DA", "#85889D", "#758544" },
        { "LunaInactiveFrame",         "#6888BB", "#AAAABB", "#A6AC8B" },
        { "LunaButtonBorder",          "#003C74", "#606477", "#647746" },
        { "LunaInputBorder",           "#7F9DB9", "#9297A7", "#8D986C" },
        { "LunaToggleBorder",          "#527696", "#777B8F", "#778557" },
        { "LunaThumbBorder",           "#567CA8", "#85899E", "#7B8D53" },
        { "LunaArrowInk",              "#244B8D", "#44485F", "#4F5E31" },
        { "LunaArrowBorder",           "#91B0DB", "#A2A6BE", "#A0AE76" },
        { "LunaScrollBorder",          "#8DA8CD", "#A0A4BB", "#A0AB7A" },
        { "LunaScrollHotBorder",       "#5A84C1", "#747DAD", "#778D46" },
        { "LunaGripDark",              "#88A6CF", "#A0A3BB", "#A1AF7D" },
        { "LunaGripLight",             "#EEF5FF", "#F6F6FF", "#F6F8E8" },
        { "LunaInputPressed",          "#9AB8E4", "#B5B8D0", "#B0C18A" },
        { "LunaScrollPressed",         "#ADC5E8", "#BFC2D6", "#BDCDA0" },
        { "LunaHotBorder",             "#E6A126", "#D6A14C", "#CDA137" },
        { "LunaHotBevel",              "#FFE5AF", "#FFE8BB", "#F3E4AF" },
        { "LunaFocus",                 "#245EDB", "#6378B5", "#6F8843" },
        { "LunaFocusBevel",            "#A3C5FA", "#C5CCE9", "#D4DFAD" },
        { "LunaButtonPressed",         "#DDDCCF", "#D3D3DE", "#D6DCC1" },
        { "LunaTogglePressed",         "#D9D6C6", "#D2D2DC", "#D4D8BA" },
        { "LunaPressedBevel",          "#A49F90", "#999AA9", "#9CA67D" },
        { "LunaDisabledText",          "#77756A", "#767681", "#787B65" },
        { "LunaDisabledBorder",        "#AAA89B", "#AAAAB5", "#ABAE98" },
        { "LunaDisabledArrow",         "#E0DECF", "#DBDBE3", "#DFE2CF" },
        { "LunaGrooveBorder",          "#9B998B", "#A0A0AD", "#A1A58D" },
        { "LunaTick",                  "#757266", "#747582", "#777E60" },
        { "LunaPopupBorder",           "#716F64", "#777787", "#747C58" },
        { "LunaDivider",               "#C8C5B5", "#C6C6D0", "#C7CCAF" },
        { "LunaSeparator",             "#ACA899", "#ABABB8", "#A9AF94" },
        { "LunaGroupBorder",           "#C7C3B3", "#C5C5D1", "#C8CEAD" },
        { "LunaTreeExpanderBorder",     "#7A796D", "#7E7F8E", "#7E8667" },
        { "LunaTreeHover",             "#E2EDFA", "#E7EAF5", "#EAF0DC" },
        { "LunaScrollTrack",           "#F3F1E8", "#EEEEF4", "#F0F2E4" },
        { "LunaScrollFocus",           "#DDEAFF", "#E4E7F5", "#E5EDCF" },
        { "LunaTrackPressed",          "#30A7B7D2", "#30AAAFC8", "#30ACBB89" },
        { "LunaCaptionBorder",         "#E1EEFF", "#F5F5FF", "#E5ECCC" },
        { "LunaCaptionPressed",        "#174490", "#7C8099", "#657437" },
        { "LunaTitleText",             "#FFFFFF", "#252636", "#FFFFFF" },
        { "LunaCaptionGlyph",          "#FFFFFF", "#3E4051", "#FFFFFF" },
        { "LunaInactiveTitleText",     "#FFFFFF", "#666677", "#F3F5E7" },
        { "LunaSelection",             "#316AC5", "#316AC5", "#316AC5" },
        { "LunaToolTipSurface",        "#FFFFE1", "#FFFFE1", "#FFFFE1" },
        { "LunaTreeExpanderInk",       "#333333", "#333333", "#333333" },
        { "LunaPlaceholder",           "#888888", "#888894", "#858973" },
        { "LunaValidationError",       "#B52C21", "#B52C21", "#B52C21" },
        { "LunaClosePressed",          "#A52D1C", "#A52D1C", "#A52D1C" },
        { "LunaCaptionFocus",          "#FFFF80", "#8F9ABD", "#FFFF80" },
        { "LunaCheckMark",             "#269324", "#4E7337", "#577B2B" },
    };

    public static ResourceDictionary Create(LunaVariant variant)
    {
        if (!Enum.IsDefined(variant)) throw new ArgumentOutOfRangeException(nameof(variant));
        var column = (int)variant + 1;
        var result = new ResourceDictionary();
        for (int row = 0; row < Colors.GetLength(0); row++)
            result[Colors[row, 0]] = new SolidColorBrush(Color.Parse(Colors[row, column]));

        string Pick(string blue, string silver, string olive) => variant switch
        { LunaVariant.Blue => blue, LunaVariant.Silver => silver, _ => olive };
        LinearGradientBrush Gradient(string colors, string offsets, bool horizontal = false, bool diagonal = false)
        {
            var brush = new LinearGradientBrush { StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative), EndPoint = new RelativePoint(horizontal || diagonal ? 1 : 0, horizontal ? 0 : 1, RelativeUnit.Relative) };
            var stops = colors.Split(',');
            var positions = offsets.Split(',');
            for (int i = 0; i < stops.Length; i++)
                brush.GradientStops.Add(new GradientStop(Color.Parse(stops[i]), double.Parse(positions[i], System.Globalization.CultureInfo.InvariantCulture)));
            return brush;
        }

        result["LunaTitle"] = Gradient(Pick("#6DA3FA,#1269ED,#0055E5,#287AF1,#0038A8", "#E7E7EE,#FAFAFE,#B8BACD,#D7D9E7,#7C7F9B", "#CBD5A9,#B8C78F,#91A15A,#ADBC80,#6E7B3F"), "0,0.15,0.5,0.88,1");
        result["LunaInactiveTitle"] = Gradient(Pick("#A5BFE6,#7A96C8,#8DA8D3,#6888BB", "#E5E5EA,#C9C9D5,#DDDDE5,#AAAABB", "#D6DBC3,#BBC2A1,#CBD1B8,#A6AC8B"), "0,0.2,0.88,1");
        result["LunaCaptionFill"] = Gradient(Pick("#A6C6FF,#4386F1,#1254CE", "#FFFFFF,#D7D8E5,#9295AE", "#E6EDCE,#B2C17D,#7D8D45"), "0,0.3,1", diagonal: true);
        result["LunaCloseFill"] = Gradient("#FFB49A,#E86643,#B92B16", "0,0.3,1", diagonal: true);
        result["LunaButtonFill"] = Gradient(Pick("#FFFFFF,#F5F4EA,#DEDCCB", "#FFFFFF,#F4F3F8,#DCDCE5", "#FFFFFF,#F5F5E8,#D8DBC1"), "0,0.5,1");
        result["LunaProgressFill"] = Gradient(Pick("#B9EF9A,#45B52F,#219A18", "#DAEFCE,#91BD71,#6AA048", "#C8E49C,#86A346,#587F31"), "0,0.45,1");
        result["LunaInputFill"] = Gradient(Pick("#F1F7FF,#CBDEF9,#A9C5EF", "#F9F9FD,#DFE0ED,#B9BACE", "#F7FAEA,#DCE3B8,#B7C688"), "0,0.5,1");
        var hot = Pick("#FFFFFF,#DDEEFF,#BED6FF", "#FFFFFF,#ECECF7,#C9CCE1", "#FFFFFF,#EDF2D5,#CDD9A4");
        result["LunaInputHotFill"] = Gradient(hot, "0,0.5,1");
        result["LunaVerticalScrollHot"] = Gradient(hot, "0,0.5,1", horizontal: true);
        var scroll = Pick("#EEF4FF,#C8D9F2,#BCD0EE,#D7E5FA", "#F9F9FD,#E1E2EE,#CED1E1,#EDEEF7", "#F5F7E6,#D8DFB6,#C0CCA0,#E4E9CD");
        result["LunaVerticalScrollFill"] = Gradient(scroll, "0,0.25,0.7,1", horizontal: true);
        result["LunaHorizontalScrollFill"] = Gradient(scroll, "0,0.25,0.7,1");
        // 选择仍是蓝色，未覆盖的 Fluent 控件也保持系统选择习惯。
        result["SystemAccentColor"] = Color.Parse("#245EDB");
        result["SystemControlHighlightAccentBrush"] = new SolidColorBrush(Color.Parse("#245EDB"));
        return result;
    }
}
