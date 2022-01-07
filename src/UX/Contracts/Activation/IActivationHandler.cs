using System.Threading.Tasks;

namespace Seemon.Authenticator.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}
