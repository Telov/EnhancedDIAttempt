using Zenject;

public class UpdatesControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // IUpdatesController updatesController = new UpdatesController();
        //
        // Container.Bind(
        //     typeof(IUpdatesController),
        //     typeof(ITickable),
        //     typeof(IFixedTickable),
        //     typeof(ILateTickable))
        //     .FromInstance(updatesController);
    }
}