using System;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.Scenes.Runtime.NodeCanvas
{
    [Category("SFramework/Scenes")]
    [Name("Reload Active Scene")]
    [Serializable]
    public class ReloadSceneActionTask : SFActionTask
    {
        private ISFScenesService _scenesService;

        protected override void Init(ISFContainer injectionContainer)
        {
            _scenesService = injectionContainer.Resolve<ISFScenesService>();
        }

        protected override string info => $"<color=green>Reload</color> <color=yellow>Active</color> Scene";

        protected override void OnExecute()
        {
            _scenesService.GetActiveScene(out SFScene sfScene);
            
            _scenesService.ReloadScene(sfScene, () => { }, instance =>
            {
                EndAction(true);
            });
        }
    }
}