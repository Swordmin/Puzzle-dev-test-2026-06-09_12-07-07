
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Level _level;
    [SerializeField] private DragManager _dragManager;
    [SerializeField] private DragingObjectContainer _dragingObjectContainer;
    [SerializeField] private LevelSwitcher _levelSwitcher;

    public override void InstallBindings()
    {
        Container.Bind<Level>().FromInstance(_level);
        Container.Bind<DragManager>().FromInstance(_dragManager);
        Container.Bind<DragingObjectContainer>().FromInstance(_dragingObjectContainer);
        Container.Bind<LevelSwitcher>().FromInstance(_levelSwitcher);
    }
}
