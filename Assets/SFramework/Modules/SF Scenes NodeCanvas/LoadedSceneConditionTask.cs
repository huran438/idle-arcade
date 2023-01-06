using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.Scenes.Runtime.NodeCanvas
{
    [Category("SFramework/Scenes")]
    [Name("Loaded Scene")]
    [Serializable]
    public class LoadedSceneConditionTask : SFConditionTask
    {
        public BBParameter<SFScene> Scene = new BBParameter<SFScene>();

        private ISFScenesService _scenesService;

        protected override void Init(ISFContainer injectionContainer)
        {
            _scenesService = injectionContainer.Resolve<ISFScenesService>();

            if (Scene.value.IsNone)
            {
                _scenesService.OnSceneLoaded += scene => { YieldReturn(true); };
            }
        }

        protected override string info =>
            $"Loaded {Scene} Scene";

        protected override bool OnCheck()
        {
            if (Scene.value.IsNone) return false;
            return _scenesService.IsLoaded(Scene.value);
        }
    }
}