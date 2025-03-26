using Microsoft.AspNetCore.Identity.Data;
using SkateEventManager.Enums;

namespace SkateEventManager.Models;

public class ExtendedRegisterRequest
{
    public RegisterRequest RegisterRequest { get; set; }  // Composition

    /// <summary>
    /// The user's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The user's role.
    /// </summary>
    public RoleOfUser Role { get; set; }
    public ExtendedRegisterRequest(RegisterRequest registerRequest)
    {
        RegisterRequest = registerRequest;
    }
}
