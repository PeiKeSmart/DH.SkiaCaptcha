using System.Reflection;

using SkiaSharp;

namespace DH.SkiaCaptcha;

/// <summary>默认字体族集合，从嵌入资源加载字体文件，避免依赖系统字体导致验证码文字空白</summary>
public class DefaultFontFamilys
{
    /// <summary>单例实例</summary>
    public static DefaultFontFamilys Instance { get; } = new();

    private static List<SKTypeface> _fontFamilies = [];
    private static readonly Dictionary<String, String> FamilyNameMapper = new()
    {
        { "actionj", "Action Jackson" },
        { "epilog", "Epilog" },
        { "fresnel", "Fresnel" },
        { "headache", "Tom's Headache" },
        { "lexo", "Lexographer" },
        { "prefix", "Prefix" },
        { "progbot", "PROG.BOT" },
        { "ransom", "Ransom" },
        { "robot", "Robot Teacher" },
        { "scandal", "Potassium Scandal" },
        { "kaiti", "FZKai-Z03" }
    };

    static DefaultFontFamilys()
    {
        if (_fontFamilies.Count == 0)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();

            if (names?.Length > 0 == true)
            {
                foreach (var name in names)
                {
                    _fontFamilies.Add(SKTypeface.FromStream(assembly.GetManifestResourceStream(name)));
                }
            }
            else
            {
                throw new Exception($"绘制验证码字体文件加载失败");
            }
        }
    }

    /// <summary>
    /// 获取字体
    /// </summary>
    /// <param name="name">字体名称</param>
    /// <returns>字体</returns>
    public SKTypeface GetFontFamily(String name)
    {
        var realName = "Epilog";
        var normalizeName = name?.ToLowerInvariant();
        if (!String.IsNullOrWhiteSpace(normalizeName) && FamilyNameMapper.ContainsKey(normalizeName))
        {
            // 默认字体
            realName = FamilyNameMapper[normalizeName];
        }
        // 改用StartsWith, 某些环境下： Prefix取到的值为Prefix Endangered, Ransom取到的值为Ransom CutUpLetters
        return _fontFamilies.First(f => f.FamilyName.StartsWith(realName));
    }

    /// <summary>ACTIONJ 字体</summary>
    public SKTypeface Actionj => GetFontFamily("Actionj");

    /// <summary>Epilog 字体</summary>
    public SKTypeface Epilog => GetFontFamily("Epilog");

    /// <summary>Fresnel 字体</summary>
    public SKTypeface Fresnel => GetFontFamily("Fresnel");

    /// <summary>Headache 字体</summary>
    public SKTypeface Headache => GetFontFamily("Headache");

    /// <summary>Lexo 字体</summary>
    public SKTypeface Lexo => GetFontFamily("Lexo");

    /// <summary>Prefix 字体</summary>
    public SKTypeface Prefix => GetFontFamily("Prefix");

    /// <summary>Progbot 字体</summary>
    public SKTypeface Progbot => GetFontFamily("Progbot");

    /// <summary>Ransom 字体</summary>
    public SKTypeface Ransom => GetFontFamily("Ransom");

    /// <summary>Robot 字体</summary>
    public SKTypeface Robot => GetFontFamily("Robot");

    /// <summary>Scandal 字体</summary>
    public SKTypeface Scandal => GetFontFamily("Scandal");

    /// <summary>楷体</summary>
    public SKTypeface Kaiti => GetFontFamily("Kaiti");
}
