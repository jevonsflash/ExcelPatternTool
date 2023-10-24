using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;

namespace ExcelPatternTool.Common.Helper
{
    public class SyntaxHelper
    {

        public static SyntaxKind GetSyntaxKind(string text)
            => text switch
            {
                "true" => SyntaxKind.TrueKeyword,
                "false" => SyntaxKind.FalseKeyword,
                "double" => SyntaxKind.DoubleKeyword,
                "char" => SyntaxKind.CharKeyword,
                "string" => SyntaxKind.StringKeyword,
                "float" => SyntaxKind.FloatKeyword,
                "long" => SyntaxKind.LongKeyword,
                "int" => SyntaxKind.IntKeyword,
                "bool" => SyntaxKind.BoolKeyword,
                "var" => SyntaxKind.VarKeyword,
                "const" => SyntaxKind.ConstKeyword,
                "struct" => SyntaxKind.StructKeyword,
                "class" => SyntaxKind.ClassKeyword,
                "namespace" => SyntaxKind.NamespaceKeyword,
                "break" => SyntaxKind.BreakKeyword,
                "continue" => SyntaxKind.ContinueKeyword,
                "while" => SyntaxKind.WhileKeyword,
                "for" => SyntaxKind.ForKeyword,
                "if" => SyntaxKind.IfKeyword,
                "else" => SyntaxKind.ElseKeyword,
                "return" => SyntaxKind.ReturnKeyword,


                _ => SyntaxKind.BadToken,
            };


        public static string GetText(SyntaxKind kind)
            => kind switch
            {
                //Keywords
                SyntaxKind.TrueKeyword => "true",
                SyntaxKind.FalseKeyword => "false",
                SyntaxKind.DoubleKeyword => "double",
                SyntaxKind.CharKeyword => "char",
                SyntaxKind.StringKeyword => "string",
                SyntaxKind.FloatKeyword => "float",
                SyntaxKind.LongKeyword => "long",
                SyntaxKind.IntKeyword => "int",
                SyntaxKind.BoolKeyword => "bool",
                SyntaxKind.VarKeyword => "var",
                SyntaxKind.ConstKeyword => "const",
                SyntaxKind.StructKeyword => "struct",
                SyntaxKind.ClassKeyword => "class",
                SyntaxKind.NamespaceKeyword => "namespace",
                SyntaxKind.BreakKeyword => "break",
                SyntaxKind.ContinueKeyword => "continue",
                SyntaxKind.WhileKeyword => "while",
                SyntaxKind.ForKeyword => "for",
                SyntaxKind.IfKeyword => "if",
                SyntaxKind.ElseKeyword => "else",
                SyntaxKind.ReturnKeyword => "return",


                _ => "BadToken"
            };




    }
}