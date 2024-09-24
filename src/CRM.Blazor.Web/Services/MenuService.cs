namespace CRM.Blazor.Web.Services;

public class MenuService
{
    Menu[] allMenus =
    [        
        new Menu
        {
            Name = "身份认证管理",
            Icon = "security_key",
            Children =
            [
                new Menu
                {
                    Name = "用户",
                    Path = "/user/list",
                    Icon = "person"
                },
                 new Menu
                {
                    Name = "角色",
                    Path = "/role/list",
                    Icon = "safety_check"
                },
            ]
        }
    ];

    public IEnumerable<Menu> Menus
    {
        get { return allMenus; }
    }

    public IEnumerable<Menu> Filter(string term)
    {
        if (string.IsNullOrEmpty(term))
            return allMenus;

        bool contains(string value) =>
            value != null && value.Contains(term, StringComparison.OrdinalIgnoreCase);

        bool filter(Menu example) =>
            contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));

        bool deepFilter(Menu example) => filter(example) || example.Children?.Any(filter) == true;

        return Menus
            .Where(category => category.Children?.Any(deepFilter) == true || filter(category))
            .Select(category => new Menu
            {
                Name = category.Name,
                Path = category.Path,
                Icon = category.Icon,
                Expanded = true,
                Children = category
                    .Children?.Where(deepFilter)
                    .Select(example => new Menu
                    {
                        Name = example.Name,
                        Path = example.Path,
                        Icon = example.Icon,
                        Expanded = true,
                        Children = example.Children
                    })
                    .ToArray()
            })
            .ToList();
    }

    public Menu FindCurrent(Uri uri)
    {
        IEnumerable<Menu> Flatten(IEnumerable<Menu> e)
        {
            return e.SelectMany(c => c.Children != null ? Flatten(c.Children) : new[] { c });
        }

        return Flatten(Menus)
            .FirstOrDefault(example =>
                example.Path == uri.AbsolutePath || $"/{example.Path}" == uri.AbsolutePath
            );
    }

    public string TitleFor(Menu example)
    {
        if (example != null && example.Name != "Overview")
        {
            return example.Title
                ?? $"Blazor {example.Name} Component | Free UI Components by Radzen";
        }

        return "Free Blazor Components | 90+ UI controls by Radzen";
    }

    public string DescriptionFor(Menu example)
    {
        return example?.Description
            ?? "The Radzen Blazor component library provides more than 90 UI controls for building rich ASP.NET Core web applications.";
    }
}
