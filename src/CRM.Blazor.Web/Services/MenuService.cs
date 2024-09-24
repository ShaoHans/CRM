namespace CRM.Blazor.Web.Services;

public class MenuService
{
    Menu[] allMenus =
    [
        new Menu
        {
            Name = "Overview",
            Path = "/",
            Icon = "&#xe88a"
        },
        new Menu
        {
            Name = "Dashboard",
            Path = "/dashboard",
            Updated = true,
            Title = "Sample Dashboard | Free UI Components by Radzen",
            Description = "Rich dashboard created with the Radzen Blazor Components library.",
            Icon = "&#xe871"
        },
        new Menu
        {
            Name = "Get Started",
            Path = "/get-started",
            Title = "Get Started | Free UI Components by Radzen",
            Description = "How to get started with the Radzen Blazor Components library.",
            Icon = "&#xe1c4"
        },
        new Menu
        {
            Name = "Support",
            Path = "/support",
            Title = "Support | Free UI Components by Radzen",
            Description = "How to get support for the Radzen Blazor Components library.",
            Icon = "&#xe0c6"
        },
        new Menu
        {
            Name = "Accessibility",
            Path = "/accessibility",
            Title = "Blazor Accessibility | Free UI Components by Radzen",
            Description =
                "The accessible Radzen Blazor Components library covers highest levels of web accessibility guidelines and recommendations, making you Blazor app compliant with WAI-ARIA, WCAG 2.2, section 508, and keyboard compatibility standards.",
            Icon = "&#xe92c",
            Tags = new[]
            {
                "keyboard",
                "accessibility",
                "standard",
                "508",
                "wai-aria",
                "wcag",
                "shortcut"
            }
        },
        new Menu
        {
            Name = "Forms",
            Icon = "&#xf1c1",
            Children = new[]
            {
                new Menu
                {
                    Name = "ColorPicker",
                    Description =
                        "Demonstration and configuration of the Radzen Blazor ColorPicker component. HSV Picker. RGBA Picker.",
                    Path = "colorpicker",
                    Icon = "&#xe40a",
                    Tags = new[] { "form", "edit" }
                },
                new Menu
                {
                    Name = "DatePicker",
                    Path = "datepicker",
                    Updated = true,
                    Description =
                        "Demonstration and configuration of the Radzen Blazor Datepicker component with calendar mode.",
                    Icon = "&#xe916",
                    Tags = new[] { "calendar", "form", "edit" }
                },
                new Menu
                {
                    Name = "DropDown",
                    Icon = "&#xe172",
                    Children = new[]
                    {
                        new Menu
                        {
                            Name = "Single selection",
                            Path = "dropdown",
                            Title = "Blazor DropDown Component | Free UI Components by Radzen",
                            Description =
                                "Demonstration and configuration of the Radzen Blazor DropDown component.",
                            Tags = new[] { "select", "picker", "form", "edit", "dropdown" },
                        },
                        new Menu
                        {
                            Name = "Multiple selection",
                            Path = "dropdown-multiple",
                            Title =
                                "Blazor DropDown Component - Multiple Selection | Free UI Components by Radzen",
                            Description =
                                "This example demonstrates multiple selection support in Radzen Blazor DropDown component.",
                            Tags = new[]
                            {
                                "select",
                                "picker",
                                "form",
                                "edit",
                                "multiple",
                                "dropdown"
                            },
                        },
                    }
                },
                new Menu
                {
                    Name = "DropDownDataGrid",
                    Path = "dropdown-datagrid",
                    Description =
                        "Blazor DropDown component with columns and multiple selection support.",
                    Icon = "&#xe99c",
                    Tags = new[] { "select", "picker", "form", "edit" }
                },
                new Menu
                {
                    Name = "Fieldset",
                    Path = "fieldset",
                    Description =
                        "Demonstration and configuration of the Radzen Blazor Fieldset component.",
                    Icon = "&#xe728",
                    Tags = new[] { "form", "container" }
                },
                new Menu
                {
                    Name = "FileInput",
                    Path = "fileinput",
                    Description = "Blazor File input component with preview support.",
                    Icon = "&#xe226",
                    Tags = new[] { "upload", "form", "edit" }
                },
                new Menu
                {
                    Name = "FormField",
                    Path = "form-field",
                    Description =
                        "Radzen Blazor FormField component features a floating label effect. When the user focuses on an empty input field, the label floats above, providing a visual cue as to which field is being filled out.",
                    Icon = "&#xe578",
                    Tags = new[]
                    {
                        "form",
                        "label",
                        "floating",
                        "float",
                        "edit",
                        "outline",
                        "input",
                        "helper",
                        "valid"
                    }
                },
                new Menu
                {
                    Name = "Upload",
                    Description =
                        "Demonstration and configuration of the Radzen Blazor Upload component.",
                    Path = "example-upload",
                    Icon = "&#xf09b",
                    Tags = new[] { "upload", "file" }
                },
            },
        },
        new Menu
        {
            Name = "身份认证管理",
            Icon = "&#xe92c",
            Children =
            [
                new Menu
                {
                    Name = "用户",
                    Path = "/user/list",
                    Icon = "list"
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
