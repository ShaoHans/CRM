namespace Radzen;

public static class NotificationServiceExtension
{
    public static NotificationService Error(
        this NotificationService service,
        string detail,
        string summary = "",
        double duration = 3000.0,
        Action<NotificationMessage>? click = null,
        bool closeOnClick = false,
        object? payload = null,
        Action<NotificationMessage>? close = null
    )
    {
        service.Notify(
            NotificationSeverity.Error,
            summary,
            detail,
            duration,
            click,
            closeOnClick,
            payload,
            close
        );
        return service;
    }

    public static NotificationService Success(
        this NotificationService service,
        string detail,
        string summary = "",
        double duration = 3000.0,
        Action<NotificationMessage>? click = null,
        bool closeOnClick = false,
        object? payload = null,
        Action<NotificationMessage>? close = null
    )
    {
        service.Notify(
            NotificationSeverity.Success,
            summary,
            detail,
            duration,
            click,
            closeOnClick,
            payload,
            close
        );
        return service;
    }
}
