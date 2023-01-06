using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.UI.Runtime.NodeCanvas
{
    [Category("SFramework/UI")]
    [Name("Button Clicked")]
    [Serializable]
    public class ButtonClickedScreenConditionTask : SFConditionTask
    {
        public BBParameter<string> Button;

        private ISFUIService _uiListener;

        protected override void Init(ISFContainer injectionContainer)
        {
            _uiListener = injectionContainer.Resolve<ISFUIService>();
            _uiListener.OnButtonClick += OnButtonClick;
        }

        private void OnButtonClick(string button)
        {
            if (Button.value != button) return;
            YieldReturn(true);
        }

        protected override bool OnCheck()
        {
            return false;
        }

        protected override string info => $"<color=green>Clicked </color><color=yellow>{Button}</color> Button";
    }
}