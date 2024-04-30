using System.Threading.Tasks;

namespace TestWasmApp.ViewModels;

using TestWasmApp.Models;

public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    EdgeListener _listener = new EdgeListener();

    public void StartListening()
    {
        _listener.AttemptToConnect();
    }
    
    
#pragma warning restore CA1822 // Mark members as static
}