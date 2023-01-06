using _Client_.Scripts.Data;
using SFramework.Core.Runtime;
using TMPro;
using UnityEngine;

namespace _Client_.Scripts.Views.Debug
{
    public class VersionLabelWidget : SFView
    {
        [SFInject]
        private readonly BuildData _buildData;
        
        [SerializeField]
        private TextMeshProUGUI _labelText;

        protected override void Init()
        {
            _labelText.SetText(_buildData.Version);
        }
    }
}