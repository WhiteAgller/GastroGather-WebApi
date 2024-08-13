using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.AuthServer.ViewModels;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}