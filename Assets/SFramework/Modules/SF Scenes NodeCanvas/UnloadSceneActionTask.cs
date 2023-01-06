using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.Scenes.Runtime.NodeCanvas
{
    [Category("SFramework/Scenes")]
    [Name("Unload Scene")]
    [Serializable]
    public class UnloadSceneActionTask : SFActionTask
    {
        public BBParameter<SFScene> _scene;

        private ISFScenesService _scenesService;

        protected override void Init(ISFContainer injectionContainer)
        {
            _scenesService = injectionContainer.Resolve<ISFScenesService>();
        }

        protected override string info => $"<color=red>Unload</color> <color=yellow>{_scene}</color> Scene";

        protected override void OnExecute()
        {
            _scenesService.UnloadScene(_scene.value, () =>
            {
                EndAction(true);
            });
        }
    }
}