using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.UI.Runtime.NodeCanvas
{
    [Category("SFramework/UI")]
    [Name("Show Screen")]
    [Serializable]
    public class ShowScreenConditionTask : SFConditionTask
    {
        public BBParameter<string> _screen;

        private ISFUIService _uiListener;

        protected override void Init(ISFContainer injectionContainer)
        {
            _uiListener = injectionContainer.Resolve<ISFUIService>();
        }
        
        protected override bool OnCheck()
        {
            return _uiListener.GetScreenState(_screen.value) == SFScreenState.Showing;
        }

        protected override string info => $"<color=green>Show </color><color=yellow>{_screen}</color> Screen";
    }
}