using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using ExcelPatternTool.Core.Helper;
using ExcelPatternTool.Core.Patterns;
using Newtonsoft.Json;
using ExcelPatternTool.Contracts.Patterns;

namespace ExcelPatternTool.Core.EntityProxy
{
    public class EntityProxyGenerator
    {

        private Assembly _generatedServiceProxyAssembly;
        private Pattern _pattern;
        public EntityProxyGenerator()
        {
            _pattern = LocalDataHelper.ReadObjectLocal<Pattern>();

        }
        public EntityProxyGenerator(string filePath)
        {
            if (DirFileHelper.IsExistFile(filePath))
            {
                var serializedstr = DirFileHelper.ReadFile(filePath);
                _pattern = JsonConvert.DeserializeObject<Pattern>(serializedstr);
            }
        }
        public AttributeListSyntax CreateAttribult(string attr)
        {
            var result = SyntaxFactory.AttributeList(
                        SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                            SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName(attr))));
            return result;
        }


        public AttributeListSyntax CreateAttribult(string attr, string value)
        {
            var result = SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                                SyntaxFactory.Attribute(
                                                    SyntaxFactory.IdentifierName(attr))
                                                .WithArgumentList(
                                                    SyntaxFactory.AttributeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                            SyntaxFactory.AttributeArgument(
                                                                SyntaxFactory.LiteralExpression(
                                                                    SyntaxKind.StringLiteralExpression,
                                                                    SyntaxFactory.Literal(value))))))


                                                ));
            return result;
        }

        public AttributeListSyntax CreateAttribult(string attr, int value)
        {
            var result = SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                                SyntaxFactory.Attribute(
                                                    SyntaxFactory.IdentifierName(attr))
                                                .WithArgumentList(
                                                    SyntaxFactory.AttributeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                            SyntaxFactory.AttributeArgument(
                                                                SyntaxFactory.LiteralExpression(
                                                                    SyntaxKind.NumericLiteralExpression,
                                                                    SyntaxFactory.Literal(value))))))


                                                ));
            return result;
        }


        public PropertyDeclarationSyntax CreateProperty(string type, string propName, string name, int order)
        {
            var result = SyntaxFactory.PropertyDeclaration(
                        GetTypeSyntax(type),
                                  SyntaxFactory.Identifier(propName))
                              .WithAttributeLists(
                                  SyntaxFactory.List<AttributeListSyntax>(
                                      new AttributeListSyntax[]{
                                       this.CreateAttribult("DisplayName",name),
                                       this.CreateAttribult("Exportable",name),
                                       this.CreateAttribult("Importable",order)

                                      }))
                              .WithModifiers(
                                  SyntaxFactory.TokenList(
                                      SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                              .WithAccessorList(
                                  SyntaxFactory.AccessorList(
                                      SyntaxFactory.List<AccessorDeclarationSyntax>(
                                          new AccessorDeclarationSyntax[]{
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.GetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.SetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken))})));


            return result;
        }

        public PropertyDeclarationSyntax CreateProperty(string genericType, string type, string propName, string name, int order)
        {
            var result = SyntaxFactory.PropertyDeclaration(
                 SyntaxFactory.GenericName(
                     SyntaxFactory.Identifier(genericType))
                 .WithTypeArgumentList(
                     SyntaxFactory.TypeArgumentList(
                         SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                    GetTypeSyntax(type)))),
                 SyntaxFactory.Identifier(propName))
                         .WithAttributeLists(
                                          SyntaxFactory.List<AttributeListSyntax>(
                                              new AttributeListSyntax[]{
                                               this.CreateAttribult("DisplayName",name),
                                               this.CreateAttribult("Exportable",name),
                                               this.CreateAttribult("Importable",order)

                                              }))
                                      .WithModifiers(
                                          SyntaxFactory.TokenList(
                                              SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                                      .WithAccessorList(
                                          SyntaxFactory.AccessorList(
                                              SyntaxFactory.List<AccessorDeclarationSyntax>(
                                                  new AccessorDeclarationSyntax[]{
                                                    SyntaxFactory.AccessorDeclaration(
                                                        SyntaxKind.GetAccessorDeclaration)
                                                    .WithSemicolonToken(
                                                        SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                                    SyntaxFactory.AccessorDeclaration(
                                                        SyntaxKind.SetAccessorDeclaration)
                                                    .WithSemicolonToken(
                                                        SyntaxFactory.Token(SyntaxKind.SemicolonToken))})));
            return result;

        }


        private TypeSyntax GetTypeSyntax(string type)
        {
            var syntaxKind = SyntaxHelper.GetSyntaxKind(type);
            if (syntaxKind!=SyntaxKind.BadToken)
            {
                return SyntaxFactory.PredefinedType(
                                                SyntaxFactory.Token(syntaxKind));
            }
            else
            {
                return SyntaxFactory.IdentifierName(type);
            }
        }

        public List<MemberDeclarationSyntax> CreateElement()
        {

            var result = new List<MemberDeclarationSyntax>();

            if (_pattern.DbExport!=null && _pattern.DbExport?.TableKeyType!=TableKeyType.NoKey)
            {

                result.Add(SyntaxFactory.PropertyDeclaration(
                                    GetTypeSyntax(_pattern.DbExport?.TableKeyType.ToString()),
                                    SyntaxFactory.Identifier("Id"))
                                .WithAttributeLists(
                                    SyntaxFactory.List<AttributeListSyntax>(
                                        new AttributeListSyntax[]{
                                        SyntaxFactory.AttributeList(
                                            SyntaxFactory.SeparatedList<AttributeSyntax>(
                                                new SyntaxNodeOrToken[]{
                                                    SyntaxFactory.Attribute(
                                                        SyntaxFactory.IdentifierName("Key")),
                                                    SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                    SyntaxFactory.Attribute(
                                                        SyntaxFactory.IdentifierName("DatabaseGenerated"))
                                                    .WithArgumentList(
                                                        SyntaxFactory.AttributeArgumentList(
                                                            SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                                SyntaxFactory.AttributeArgument(
                                                                    SyntaxFactory.MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        SyntaxFactory.IdentifierName("DatabaseGeneratedOption"),
                                                                        SyntaxFactory.IdentifierName("Identity"))))))})),
                                        SyntaxFactory.AttributeList(
                                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                                SyntaxFactory.Attribute(
                                                    SyntaxFactory.IdentifierName("Exportable"))
                                                .WithArgumentList(
                                                    SyntaxFactory.AttributeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                            SyntaxFactory.AttributeArgument(
                                                                SyntaxFactory.LiteralExpression(
                                                                    SyntaxKind.TrueLiteralExpression))
                                                            .WithNameColon(
                                                                SyntaxFactory.NameColon(
                                                                    SyntaxFactory.IdentifierName("ignore"))))))))}))
                                .WithModifiers(
                                    SyntaxFactory.TokenList(
                                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                                .WithAccessorList(
                                    SyntaxFactory.AccessorList(
                                        SyntaxFactory.List<AccessorDeclarationSyntax>(
                                            new AccessorDeclarationSyntax[]{
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.GetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.SetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken))}))));
            }
            result.Add(SyntaxFactory.PropertyDeclaration(
                                SyntaxFactory.PredefinedType(
                                    SyntaxFactory.Token(SyntaxKind.LongKeyword)),
                                SyntaxFactory.Identifier("RowNumber"))
                            .WithAttributeLists(
                                SyntaxFactory.List<AttributeListSyntax>(
                                    new AttributeListSyntax[]{
                                        SyntaxFactory.AttributeList(
                                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                                SyntaxFactory.Attribute(
                                                    SyntaxFactory.IdentifierName("Exportable"))
                                                .WithArgumentList(
                                                    SyntaxFactory.AttributeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                            SyntaxFactory.AttributeArgument(
                                                                SyntaxFactory.LiteralExpression(
                                                                    SyntaxKind.TrueLiteralExpression))
                                                            .WithNameColon(
                                                                SyntaxFactory.NameColon(
                                                                    SyntaxFactory.IdentifierName("ignore")))))))),
                                        SyntaxFactory.AttributeList(
                                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                                SyntaxFactory.Attribute(
                                                    SyntaxFactory.IdentifierName("Importable"))
                                                .WithArgumentList(
                                                    SyntaxFactory.AttributeArgumentList(
                                                        SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                            SyntaxFactory.AttributeArgument(
                                                                SyntaxFactory.LiteralExpression(
                                                                    SyntaxKind.TrueLiteralExpression))
                                                            .WithNameColon(
                                                                SyntaxFactory.NameColon(
                                                                    SyntaxFactory.IdentifierName("ignore"))))))))}))
                            .WithModifiers(
                                SyntaxFactory.TokenList(
                                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                            .WithAccessorList(
                                SyntaxFactory.AccessorList(
                                    SyntaxFactory.List<AccessorDeclarationSyntax>(
                                        new AccessorDeclarationSyntax[]{
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.GetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                            SyntaxFactory.AccessorDeclaration(
                                                SyntaxKind.SetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                SyntaxFactory.Token(SyntaxKind.SemicolonToken))}))));

         
            foreach (var patternItem in _pattern.Patterns)
            {
                var propertyType = patternItem.PropType.ToString();
                var genericType = patternItem.CellType.ToString();
                var propertyName = patternItem.PropName;
                var headerName = patternItem.HeaderName;
                var order = patternItem.Order;

                if (patternItem.CellType!=CellType.GeneralType)
                {
                    result.Add(this.CreateProperty(genericType, propertyType, propertyName, headerName, order));
                }
                else
                {
                    result.Add(this.CreateProperty(propertyType, propertyName, headerName, order));
                }
            }
            return result;
        }

        public void Process()
        {
            try
            {
                var treeFrame = SyntaxFactory.CompilationUnit()
.WithUsings(
    SyntaxFactory.List<UsingDirectiveSyntax>(
        new UsingDirectiveSyntax[]{
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.IdentifierName("Microsoft"),
                    SyntaxFactory.IdentifierName("EntityFrameworkCore"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.IdentifierName("System")),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.IdentifierName("System"),
                    SyntaxFactory.IdentifierName("ComponentModel"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.IdentifierName("ExcelPatternTool"),
                            SyntaxFactory.IdentifierName("Core")),
                        SyntaxFactory.IdentifierName("Excel")),
                    SyntaxFactory.IdentifierName("Attributes"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.QualifiedName(
                                SyntaxFactory.IdentifierName("ExcelPatternTool"),
                                SyntaxFactory.IdentifierName("Core")),
                            SyntaxFactory.IdentifierName("Excel")),
                        SyntaxFactory.IdentifierName("Core")),
                    SyntaxFactory.IdentifierName("AdvancedTypes"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.QualifiedName(
                                SyntaxFactory.IdentifierName("ExcelPatternTool"),
                                SyntaxFactory.IdentifierName("Core")),
                            SyntaxFactory.IdentifierName("Excel")),
                        SyntaxFactory.IdentifierName("Models")),
                    SyntaxFactory.IdentifierName("Interfaces"))),

            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.IdentifierName("System"),
                        SyntaxFactory.IdentifierName("ComponentModel")),
                    SyntaxFactory.IdentifierName("DataAnnotations"))),
            SyntaxFactory.UsingDirective(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.IdentifierName("System"),
                            SyntaxFactory.IdentifierName("ComponentModel")),
                        SyntaxFactory.IdentifierName("DataAnnotations")),
                    SyntaxFactory.IdentifierName("Schema")))



        }))
.WithMembers(
    SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
        SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.QualifiedName(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.IdentifierName("ExcelPatternTool"),
                    SyntaxFactory.IdentifierName("Core")),
                SyntaxFactory.IdentifierName("Entites")))
        .WithMembers(
            SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                SyntaxFactory.ClassDeclaration("ExcelEntity")
                .WithAttributeLists(
            SyntaxFactory.List<AttributeListSyntax>(
                _pattern.DbExport?.TableKeyType==TableKeyType.NoKey ?
                new AttributeListSyntax[]{
                    CreateAttribult("Keyless"),
                    CreateAttribult("Table", string.IsNullOrEmpty(_pattern.DbExport?.TableName)?"DefaultTable": _pattern.DbExport?.TableName)
                  } : new AttributeListSyntax[] {
                    CreateAttribult("Table", string.IsNullOrEmpty(_pattern.DbExport?.TableName)?"DefaultTable": _pattern.DbExport?.TableName)

                  }))
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(
                    SyntaxFactory.BaseList(
                        SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
                            SyntaxFactory.SimpleBaseType(
                                SyntaxFactory.IdentifierName("IExcelEntity")))))
                .WithMembers(
                    SyntaxFactory.List<MemberDeclarationSyntax>(

                        CreateElement()


                        ))))))
.NormalizeWhitespace().SyntaxTree;


                var stream = CompilationUtilitys.CompileClientProxy(new List<SyntaxTree>() { treeFrame });

                using (stream)
                {
                    var assembly = Assembly.Load(stream.ToArray());
                    _generatedServiceProxyAssembly = assembly;
                }

            }
            catch (Exception ex)
            {

                //Console.WriteLine(ex);
                throw ex;
            }
        }

        public Assembly GetEntityProxyAssembly()
        {
            if (this._generatedServiceProxyAssembly==null)
            {
                throw new Exception("尚未生成代理类");
            }
            return _generatedServiceProxyAssembly;
        }
    }
}
