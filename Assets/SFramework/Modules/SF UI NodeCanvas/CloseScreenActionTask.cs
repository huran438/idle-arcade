using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.Core.Runtime;
using SFramework.NodeCanvas.Runtime;

namespace SFramework.UI.Runtime.NodeCanvas
{
    [Category("SFramework/UI")]
    [Name("Close Screen")]
    [Serializable]
    public class CloseScreenActionTask : SFActionTask
    {
        public BBParameter<string> _screen;

        private ISFUIService _uiController;

        protected override void Init(ISFContainer injectionContainer)
        {
            _uiController = injectionContainer.Resolve<ISFUIService>();
        }

        protected override string info => $"<color=red>Close</color> <color=yellow>{_screen}</color> Screen";

        protected override void OnExecute()
        {
            _uiController.CloseScreen(_screen.value);
            EndAction(true);
        }
    }
}