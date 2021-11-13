namespace Identity.API.Models.AuthorizationViewModels;

public record AuthorizeViewModel
{
    public string ApplicationName { get; init; }
    public string Scope { get; init; }
    public string ReturnUrl { get; init; }
}