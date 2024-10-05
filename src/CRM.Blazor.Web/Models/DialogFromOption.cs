namespace CRM.Blazor.Web.Models;

public class DialogFromOption<TCreateOrUpdateInput> where TCreateOrUpdateInput : class, new()
{
    public TCreateOrUpdateInput Model { get; set; } = new();

    public string OkSubmitText { get; set; } = default!;

    public string CancelButtonText { get; set; } = default!;

    public Func<TCreateOrUpdateInput, Task> OnSubmit { get; set; } = default!;

    public Action OnCancel { get; set; } = default!;
}
