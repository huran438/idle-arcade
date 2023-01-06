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
    public class LoadSceneConditionTask : SFConditionTask
    {
        public BBParameter<SFScene> Scene;

        protected override void Init(ISFContainer injectionContainer)
        {
            injectionContainer.Resolve<ISFScenesService>().OnSceneLoad += scene =>
            {
                if (Scene.value.IsNone || Scene.value == scene)
                {
                    YieldReturn(true);
                }
            };
        }

        protected override string info =>
            $"Load {Scene} Scene";

        protected override bool OnCheck()
        {
            return false;
        }
    }
}