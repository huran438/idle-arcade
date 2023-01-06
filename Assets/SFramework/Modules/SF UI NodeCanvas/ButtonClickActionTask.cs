using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.UI.Runtime.NodeCanvas
{
    [Category("SFramework/UI")]
    [Name("Button Click")]
    [Serializable]
    public class ButtonClickActionTask : SFActionTask
    {
        public BBParameter<string> Button;

        private ISFUIService _uiService;

        protected override void Init(ISFContainer container)
        {
            _uiService = container.Resolve<ISFUIService>();
        }

        protected override string info => $"<color=green>Click</color> <color=yellow>{Button}</color> Button";

        protected override void OnExecute()
        {
            _uiService.ButtonClickCallback(Button.value);
            EndAction(true);
        }
    }
}