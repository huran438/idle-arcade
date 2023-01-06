using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.Scenes.Runtime.NodeCanvas
{
    [Category("SFramework/Scenes")]
    [Name("Load Scene")]
    [Serializable]
    public class LoadSceneActionTask : SFActionTask
    {
        public BBParameter<SFScene> _scene;

        public bool SetActive;

        private ISFScenesService _scenesService;

        protected override void Init(ISFContainer injectionContainer)
        {
            _scenesService = injectionContainer.Resolve<ISFScenesService>();
        }

        protected override string info => $"<color=green>Load</color> <color=yellow>{_scene}</color> Scene";

        protected override void OnExecute()
        {
            _scenesService.LoadScene(_scene.value, SetActive, instance => { EndAction(true); });
        }
    }
}