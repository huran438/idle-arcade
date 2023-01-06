using System;
using System.Linq;
using SFramework.Core.Editor;
using SFramework.Core.Runtime;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace _Client_.Editor
{
    public class SFrameworkWindow : OdinMenuEditorWindow
    {
        [MenuItem("Window/SFramework")]
        private static void OpenWindow()
        {
            var window = GetWindow<SFrameworkWindow>();
            window.minSize = new Vector2(300f, 300f);
            window.titleContent = new GUIContent("SFramework");
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false)
            {
                Config =
                {
                    DrawSearchToolbar = true
                },
                DefaultMenuStyle = OdinMenuStyle.TreeViewStyle
            };


            var databases = SFEditorExtensions.FindAssets<SFDatabase>();

            for (int i = 0; i < databases.Count; i++)
            {
                tree.Add($"Databases/{databases[i].Title}", databases[i]);
            }

            return tree;
        }

        private Type[] GetInheritedClasses(Type MyType) 
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && MyType.IsAssignableFrom(t))
                .ToArray();
        }
    }
}