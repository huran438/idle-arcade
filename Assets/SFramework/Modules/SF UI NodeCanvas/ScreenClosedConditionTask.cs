using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.UI.Runtime.NodeCanvas
{
    [Category("SFramework/UI")]
    [Name("Screen Closed")]
    [Serializable]
    public class ScreenClosedConditionTask : SFConditionTask
    {
        public BBParameter<string> _screen;

        private ISFUIService _uiListener;

        protected override void Init(ISFContainer injectionContainer)
        {
            _uiListener = injectionContainer.Resolve<ISFUIService>();
        }

        protected override bool OnCheck()
        {
            return _uiListener.GetScreenState(_screen.value) == SFScreenState.Closed;
        }

        protected override string info => $"<color=green>Screen </color><color=yellow>{_screen}</color> Closed";
    }
}