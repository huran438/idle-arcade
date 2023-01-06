using System;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.Scenes.Runtime.NodeCanvas
{
    [Category("SFramework/Scenes")]
    [Name("Unload Active Scene")]
    [Serializable]
    public class UnloadActiveSceneActionTask : SFActionTask
    {
        private ISFScenesService _scenesService;

        protected override void Init(ISFContainer injectionContainer)
        {
            _scenesService = injectionContainer.Resolve<ISFScenesService>();
        }

        protected override string info => $"<color=red>Unload</color> <color=yellow>ACTIVE</color> Scene";

        protected override void OnExecute()
        {
            if (_scenesService.GetActiveScene(out SFScene activeSFScene))
            {
                _scenesService.UnloadScene(activeSFScene, () => { EndAction(true); });
                return;
            }

            EndAction(true);
        }
    }
}