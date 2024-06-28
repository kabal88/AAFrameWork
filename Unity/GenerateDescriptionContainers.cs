#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KabalFramework.Unity.Generator
{
    public class GenerateDescriptionContainers : Editor
    {
        private const string DefaultPath = "/Scripts/CustomGenerated/";
        private const string DescriptionDirectory = "DescriptionContainers/";
        private string dataPath = Application.dataPath;

        [MenuItem("Options/Generate Description Containers")]
        public static void GenerateDescriptions()
        {
            var generator = new Core.CodeGenerator.CodeGenerator();
            var unityProcessGeneration = new GenerateDescriptionContainers();
            generator.GatherAssembly();
            var containers = generator.GenerateDescriptionContainers();

            foreach (var container in containers)
            {
                unityProcessGeneration.SaveToFile(container.name, container.classBody,
                    DefaultPath + DescriptionDirectory, needToImport: true);
            }

            var library = generator.GenerateLibrary();
            unityProcessGeneration.SaveToFile(library.name, library.classBody, DefaultPath, true);
        }
        
        [MenuItem("Options/Generate Library")]
        public static void GenerateLibrary()
        {
            var generator = new Core.CodeGenerator.CodeGenerator();
            var unityProcessGeneration = new GenerateDescriptionContainers();
            generator.GatherAssembly();
            var library = generator.GenerateLibrary();
            unityProcessGeneration.SaveToFile(library.name, library.classBody, DefaultPath, true);
        }

        private void SaveToFile(string name, string data, string pathToDirectory = DefaultPath,
            bool needToImport = false)
        {
            var path = dataPath + pathToDirectory + name;

            try
            {
                if (!Directory.Exists(Application.dataPath + pathToDirectory))
                    Directory.CreateDirectory(Application.dataPath + pathToDirectory);

                File.WriteAllText(path, data);

                var sourceFile2 = path.Replace(Application.dataPath, "Assets");
                //sourceFile2 = sourceFile2.Replace(Extension, string.Empty);

                if (needToImport)
                    AssetDatabase.ImportAsset(sourceFile2);
            }
            catch
            {
                UnityEngine.Debug.LogError("не смогли осилить " + pathToDirectory);
            }
        }
    }
}
#endif
