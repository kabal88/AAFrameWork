using System;
using System.Collections.Generic;
using System.Linq;
using AAFramework.Core.Generator;
using Helpers;

namespace AAFramework.Core.CodeGenerator
{
    public partial class CodeGenerator
    {
        private const string Container = "Container";
        private const string Library = "Library";
        private const string Description = "Description";
        private const string Descriptions = "Descriptions";

        private List<Type> descriptionTypes;
        private List<Type> descriptionInterfaces;

        public static IEnumerable<Type> Assembly;

        public void GatherAssembly()
        {
            var descriptionType = typeof(Unity.Identifiers.IDescription);

            Assembly = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
            descriptionTypes = Assembly.Where(p =>
                descriptionType.IsAssignableFrom(p) && !p.IsGenericType && !p.IsAbstract && !p.IsInterface).ToList();
            descriptionInterfaces = Assembly.Where(p =>
                descriptionType.IsAssignableFrom(p) && p.Name != "IDescription" && p.IsInterface).ToList();
        }

        public List<(string name, string classBody)> GenerateDescriptionContainers()
        {
            var list = new List<(string name, string classBody)>();

            foreach (var c in descriptionTypes)
                list.Add((c.Name + Container + ".cs", GetDescriptionContainer(c)));

            return list;
        }

        public (string name, string classBody) GenerateLibrary()
        {
            return (Library + ".cs", GetLibrary(descriptionInterfaces));
        }

        private string GetDescriptionContainer(Type type)
        {
            var tree = new TreeSyntaxNode();
            tree.Add(new UsingSyntax("Descriptions"));
            tree.Add(new UsingSyntax("UnityEngine"));
            tree.Add(new UsingSyntax("System.Collections.Generic", 1));

            tree.Add(new NameSpaceSyntax("DescriptionContainers"));
            tree.Add(new LeftScopeSyntax());
            tree.Add(new TabSimpleSyntax(1, $"[CreateAssetMenu(fileName = " +
                                            $"{CParse.Quote}{type.Name}{CParse.Quote}, " +
                                            $"menuName = {CParse.Quote}Descriptions/{type.Name}{CParse.Quote})]"));
            tree.Add(new TabSimpleSyntax(1,
                $"public class {type.Name}{Container} : DescriptionContainer<{type.Name}>"));
            tree.Add(new LeftScopeSyntax(1));
            tree.Add(new RightScopeSyntax(1));
            tree.Add(new RightScopeSyntax());

            return tree.ToString();
        }

        private string GetLibrary(List<Type> interfaces)
        {
            var tree = new TreeSyntaxNode();

            var dictionaries = new TreeSyntaxNode();
            var methods = new TreeSyntaxNode();
            var initMethod = new TreeSyntaxNode();

            tree.Add(new UsingSyntax("System"));
            tree.Add(new UsingSyntax("System.Collections.Generic"));
            tree.Add(new UsingSyntax("System.Linq"));
            tree.Add(new UsingSyntax("Descriptions"));
            tree.Add(new UsingSyntax("Interfaces"));
            tree.Add(new UsingSyntax("UnityEngine", 1));

            tree.Add(new NameSpaceSyntax("Libraries"));
            tree.Add(new LeftScopeSyntax());
            // tree.Add(new TabSimpleSyntax(1, "[Serializable]"));
            tree.Add(new TabSimpleSyntax(1, $"public partial class {Library}"));
            tree.Add(new LeftScopeSyntax(1));
            // tree.Add(new TabSimpleSyntax(2, $"[SerializeField] private List<{Description}> {Descriptions} = new();"));
            tree.Add(new ParagraphSyntax());
            tree.Add(dictionaries);
            tree.Add(new ParagraphSyntax());
            tree.Add(new ParagraphSyntax());
            tree.Add(initMethod);
            tree.Add(new ParagraphSyntax());
            tree.Add(methods);
            tree.Add(new RightScopeSyntax(1));
            tree.Add(new RightScopeSyntax());


            foreach (var i in interfaces)
            {
                var typeName = i.Name.Remove(0, 1);
                typeName = typeName.ToCamelCase();
                
                dictionaries.Add(new TabSimpleSyntax(2,
                    $"private Dictionary<int, {i.Name}> {typeName}s = new();"));
            }

            initMethod.Add(GetInitMethod(interfaces));

            foreach (var type in interfaces)
            {
                methods.Add(GetGetterMethod(type));
            }

            return tree.ToString();
        }

        private ISyntax GetInitMethod(List<Type> interfaces)
        {
            var initMethod = new TreeSyntaxNode();

            initMethod.Add(new TabSimpleSyntax(2, "public void Init()"));
            initMethod.Add(new LeftScopeSyntax(2));
            initMethod.Add(new TabSimpleSyntax(3, $"foreach (var {Description.ToLower()} in {Descriptions})"));
            initMethod.Add(new LeftScopeSyntax(3));
            initMethod.Add(new TabSimpleSyntax(4, $"switch ({Description.ToLower()}.GetDescription)"));
            initMethod.Add(new LeftScopeSyntax(4));

            foreach (var type in interfaces)
            {
                var typeName = type.Name.Remove(0, 1);
                typeName = typeName.ToCamelCase();
                
                initMethod.Add(new TabSimpleSyntax(5, $"case {type.Name} data:"));
                initMethod.Add(new TabSimpleSyntax(6,
                    $"{typeName}s.Add({Description.ToLower()}.GetDescription.Id, data);"));
                initMethod.Add(new TabSimpleSyntax(6, "break;"));
            }

            initMethod.Add(new RightScopeSyntax(4));
            initMethod.Add(new RightScopeSyntax(3));
            initMethod.Add(new RightScopeSyntax(2));

            return initMethod;
        }

        private ISyntax GetGetterMethod(Type type)
        {
            var getterMethod = new TreeSyntaxNode();
            
            var typeName = type.Name.Remove(0, 1);
            
            var dictionaryName = typeName.ToCamelCase() + "s";

            getterMethod.Add(new TabSimpleSyntax(2, $"public {type.Name} Get{typeName}(int id)"));
            getterMethod.Add(new LeftScopeSyntax(2));
            getterMethod.Add(new TabSimpleSyntax(3, $"if ({dictionaryName}.TryGetValue(id, out var needed))"));
            getterMethod.Add(new LeftScopeSyntax(3));
            getterMethod.Add(new TabSimpleSyntax(4, "return needed;"));
            getterMethod.Add(new RightScopeSyntax(3));
            getterMethod.Add(new TabSimpleSyntax(3,
                $"throw new Exception({CParse.Quote}{typeName} description with id {CParse.Quote} + id + {CParse.Quote} not found{CParse.Quote});"));
            getterMethod.Add(new RightScopeSyntax(2));

            return getterMethod;
        }
    }
}