using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private MainEnterPoint mainEnterPoint;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private LevelCamera levelCamera;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Level level;
    [SerializeField] private MainUI mainUI;
    [SerializeField] private PlayerZone playerZone;
    [SerializeField] private PickupbleObjectFactory pickupbleObjectFactory;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInNewPrefab<Player>(playerPrefab, Lifetime.Singleton);
        RegisterServices(builder);
        builder.RegisterComponent<PickupbleObjectFactory>(pickupbleObjectFactory);
        RegisterEnemyFactory(builder);
        builder.RegisterComponent<MainEnterPoint>(mainEnterPoint);
        InitMainEnterPoint();
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.RegisterComponent<Joystick>(joystick);
        builder.RegisterComponent<LevelCamera>(levelCamera);
        builder.RegisterComponent<Level>(level);
        builder.RegisterComponent<MainUI>(mainUI);
        builder.RegisterComponent<PlayerZone>(playerZone);

    }

    private void RegisterEnemyFactory(IContainerBuilder builder)
    {
        builder.RegisterFactory<Enemy>(container =>
        {
            var player = container.Resolve<Player>();
            return () =>
            {
                var pickupbleFactory = container.Resolve<PickupbleObjectFactory>();
                var enemy = Instantiate(enemyPrefab);
                enemy.Construct(player, pickupbleObjectFactory);
                return enemy;
            };
        }, Lifetime.Scoped);
    }


    private async void InitMainEnterPoint()
    {
        await Cysharp.Threading.Tasks.UniTask.Delay(1);
        mainEnterPoint.Init();
    }
}
