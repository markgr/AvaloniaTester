using AvaloniaTester.Services;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AvaloniaTester.Messages
{
    internal class LoginSuccessMessage(AuthenticationResult result) : ValueChangedMessage<AuthenticationResult>(result)
    {

    }
}
