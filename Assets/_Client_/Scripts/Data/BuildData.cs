using UnityEngine;

namespace _Client_.Scripts.Data
{
    [CreateAssetMenu(menuName="SFramework/Build Data")] 
    public class BuildData : ScriptableObject
    {
        [HideInInspector]
        public string Build_Machine_Number = "NONE";

        [HideInInspector]
        public string Build_Machine_Branch = "NONE";

        [HideInInspector]
        public string Commit_Hash = "NONE";

        [HideInInspector]
        public int BUILD_NUMBER = 0;

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(Commit_Hash) || Commit_Hash == "NONE")
                {
                    return
                        $"#{Build_Machine_Number}_{Build_Machine_Branch}_{Commit_Hash}_{Application.version}_{BUILD_NUMBER}";
                }

                return
                    $"#{Build_Machine_Number}_{Build_Machine_Branch}_{Commit_Hash.Substring(0, 7)}_{Application.version}_{BUILD_NUMBER}";
            }
        }
    }
}