using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Workshop.Manager
{
    public static class CompilationUtilitys
    {
        #region Public Method

        public static MemoryStream CompileClientProxy(IEnumerable<SyntaxTree> trees)
        {

            var references = new[]
               {
                MetadataReference.CreateFromFile(typeof(SyntaxTree).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(StatementSyntax).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(SyntaxFactory).GetTypeInfo().Assembly.Location)
            };
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var allReferences = assemblies.Where(c => !c.IsDynamic).Select(x => { 
                var f = MetadataReference.CreateFromFile(x.Location); 
                return f; 
            }).Concat(references);
            return Compile(AssemblyInfo.Create("RoslynSyntaxTool.Proxy.ConvertToCSharpProxy"), trees, allReferences);
        }

        public static MemoryStream Compile(AssemblyInfo assemblyInfo, IEnumerable<SyntaxTree> trees, IEnumerable<MetadataReference> references)
        {
            return Compile(assemblyInfo.Title, assemblyInfo, trees, references);
        }

        public static MemoryStream Compile(string assemblyName, AssemblyInfo assemblyInfo, IEnumerable<SyntaxTree> trees, IEnumerable<MetadataReference> references)
        {
            trees = trees.Concat(new[] { GetAssemblyInfo(assemblyInfo) });
            var compilation = CSharpCompilation.Create(assemblyName, trees, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            var stream = new MemoryStream();
            var result = compilation.Emit(stream);
            if (!result.Success)
            {
                foreach (var message in result.Diagnostics.Select(i => i.ToString()))
                {
                    System.Console.WriteLine(message);
                }
                return null;
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        #endregion Public Method

        #region Private Method

        private static SyntaxTree GetAssemblyInfo(AssemblyInfo info)
        {
            return SyntaxFactory.CompilationUnit()
                .WithUsings(
                    SyntaxFactory.List(
                        new[]
                        {
                            SyntaxFactory.UsingDirective(
                                SyntaxFactory.QualifiedName(
                                    SyntaxFactory.IdentifierName("System"),
                                    SyntaxFactory.IdentifierName("Reflection"))),
                            SyntaxFactory.UsingDirective(
                                SyntaxFactory.QualifiedName(
                                    SyntaxFactory.QualifiedName(
                                        SyntaxFactory.IdentifierName("System"),
                                        SyntaxFactory.IdentifierName("Runtime")),
                                    SyntaxFactory.IdentifierName("InteropServices"))),
                            SyntaxFactory.UsingDirective(
                                SyntaxFactory.QualifiedName(
                                    SyntaxFactory.QualifiedName(
                                        SyntaxFactory.IdentifierName("System"),
                                        SyntaxFactory.IdentifierName("Runtime")),
                                    SyntaxFactory.IdentifierName("Versioning")))
                        }))
                .WithAttributeLists(
                    SyntaxFactory.List(
                        new[]
                        {
                            SyntaxFactory.AttributeList(
            SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Attribute(
                    SyntaxFactory.IdentifierName("TargetFramework"))
                .WithArgumentList(
                    SyntaxFactory.AttributeArgumentList(
                        SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(
                            new SyntaxNodeOrToken[]{
                                SyntaxFactory.AttributeArgument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal("net6.0"))),
                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                SyntaxFactory.AttributeArgument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal("net6.0")))
                                .WithNameEquals(
                                    SyntaxFactory.NameEquals(
                                        SyntaxFactory.IdentifierName("FrameworkDisplayName")))})))))
        .WithTarget(
            SyntaxFactory.AttributeTargetSpecifier(
                SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("AssemblyTitle"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.Title))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("AssemblyProduct"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.Product))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("AssemblyCopyright"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.Copyright))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("ComVisible"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(info.ComVisible
                                                            ? SyntaxKind.TrueLiteralExpression
                                                            : SyntaxKind.FalseLiteralExpression)))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("Guid"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.Guid))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("AssemblyVersion"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.Version))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword))),
                            SyntaxFactory.AttributeList(
                                SyntaxFactory.SingletonSeparatedList(
                                    SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("AssemblyFileVersion"))
                                        .WithArgumentList(
                                            SyntaxFactory.AttributeArgumentList(
                                                SyntaxFactory.SingletonSeparatedList(
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal(info.FileVersion))))))))
                                .WithTarget(
                                    SyntaxFactory.AttributeTargetSpecifier(
                                        SyntaxFactory.Token(SyntaxKind.AssemblyKeyword)))
                        }))
                .NormalizeWhitespace()
                .SyntaxTree;
        }

        #endregion Private Method

        #region Help Class

        public class AssemblyInfo
        {
            public string Title { get; set; }
            public string Product { get; set; }
            public string Copyright { get; set; }
            public string Guid { get; set; }
            public string Version { get; set; }
            public string FileVersion { get; set; }
            public bool ComVisible { get; set; }

            public static AssemblyInfo Create(string name, string copyright = "Copyright © MatoApp", string version = "0.0.0.1")
            {
                return new AssemblyInfo
                {
                    Title = name,
                    Product = name,
                    Copyright = copyright,
                    Guid = System.Guid.NewGuid().ToString("D"),
                    ComVisible = false,
                    Version = version,
                    FileVersion = version
                };
            }
        }

        #endregion Help Class
    }
}