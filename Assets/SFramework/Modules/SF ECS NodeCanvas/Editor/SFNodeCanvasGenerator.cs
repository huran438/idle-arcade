using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SFramework.Core.Runtime;
using SFramework.ECS.Runtime.NodeCanvas;
using SFramework.NodeCanvas.Runtime;
using UnityEditor;
using UnityEngine;

namespace SFramework.ECS.Editor.NodeCanvas
{
    [InitializeOnLoad]
    public static class SFNodeCanvasGenerator
    {
        private static string ADD_TEMPLATE = @"using ParadoxNotion.Design;
using @@COMPONENTNAMESPACE@@;
using SFramework.ECS.Runtime.NodeCanvas;
using UnityEngine.Scripting;

namespace @@NAMESPACE@@
{
    [Preserve]
    [Name(""Add @@NAME@@"")]
    public sealed class ADD_@@COMPONENTNAME@@_NC : AddComponentActionTask<@@COMPONENTNAME@@> {}
}";

        private static string HAS_TEMPLATE = @"using ParadoxNotion.Design;
using @@COMPONENTNAMESPACE@@;
using SFramework.ECS.Runtime.NodeCanvas;
using UnityEngine.Scripting;

namespace @@NAMESPACE@@
{
    [Preserve]
    [Name(""Has @@NAME@@"")]
    public sealed class HAS_@@COMPONENTNAME@@_NC : HasComponentConditionTask<@@COMPONENTNAME@@> {}
}";


        static SFNodeCanvasGenerator()
        {
            Generate();
        }

        public static void Generate(bool force = false)
        {
            var settings = AssetDatabase.LoadAssetAtPath<SFCoreSettings>(SFCoreSettings._assetPath);

            var authoringsToGenerate = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsValueType && t.GetCustomAttribute<NodeCanvasComponentAttribute>() != null)
                .ToList();
            
            var dirPath = Path.GetFullPath(Path.Combine(
                Application.dataPath + Path.DirectorySeparatorChar + settings.GeneratorScriptsPath));

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                AssetDatabase.Refresh();
            }

            foreach (var type in authoringsToGenerate)
            {
                CreateFile(type, force, dirPath, "ADD", ADD_TEMPLATE);
                CreateFile(type, force, dirPath, "HAS", HAS_TEMPLATE);
            }

            AssetDatabase.Refresh();
        }

        private static void CreateFile(Type type, bool force, string path, string prefix, string template)
        {
            var providerClassName = $"{prefix}_{type.Name}_NC";
            var providerFileName = $"{providerClassName}.cs";

            if (!force)
            {
                if (File.Exists(Path.GetFullPath(Path.Combine(path, providerFileName))))
                {
                    return;
                }
            }

            var fileContent = template
                .Replace("@@COMPONENTNAMESPACE@@", type.Namespace)
                .Replace("@@NAMESPACE@@", "SFramework.Generated")
                .Replace("@@COMPONENTNAME@@", type.Name)
                .Replace("@@NAME@@", AddSpacesToSentence(type.Name).Replace("Ref", "Reference"));


            File.WriteAllText(Path.GetFullPath(Path.Combine(path, providerFileName)), fileContent);
        }

        static string AddSpacesToSentence(string text, bool preserveAcronyms = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if (!char.IsNumber(text[i - 1]) && (text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }

            return newText.ToString();
        }
    }
}