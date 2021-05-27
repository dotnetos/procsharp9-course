using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HelloWorldGenerator
{
    [Generator]
    public class PrintableFieldsGenerator : ISourceGenerator
    {
        private const string attributeText = @"
using System;
namespace PrintableFields
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class PrintableAttribute : Attribute
    {
        public PrintableAttribute()
        {
        }
    }
}";

        public void Execute(GeneratorExecutionContext context)
        {
            MySyntaxReceiver syntaxReceiver = (MySyntaxReceiver)context.SyntaxReceiver;

            CSharpParseOptions options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;
            Compilation compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(attributeText, Encoding.UTF8), options));
            INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName("PrintableFields.PrintableAttribute");

            ProcessPrintableAttributedFields(context, syntaxReceiver, compilation, attributeSymbol);
            ProcessPrintAllFieldPartialMethods(context, syntaxReceiver, compilation);
        }

        private void ProcessPrintAllFieldPartialMethods(GeneratorExecutionContext context, MySyntaxReceiver syntaxReceiver, Compilation compilation)
        {
            // TODO: Implement - look at GivenSpecificPartialMethods_WhenHasFields_ThenAddPrintAllFieldsMethod test
        }

        private void ProcessPrintableAttributedFields(GeneratorExecutionContext context, MySyntaxReceiver syntaxReceiver, Compilation compilation, INamedTypeSymbol attributeSymbol)
        {
            List<IFieldSymbol> fieldSymbols = new List<IFieldSymbol>();
            foreach (FieldDeclarationSyntax field in syntaxReceiver.CandidateFields)
            {
                SemanticModel model = compilation.GetSemanticModel(field.SyntaxTree);
                foreach (VariableDeclaratorSyntax variable in field.Declaration.Variables)
                {
                    IFieldSymbol fieldSymbol = model.GetDeclaredSymbol(variable) as IFieldSymbol;
                    if (fieldSymbol.GetAttributes()
                                   .Any(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default)))
                    {
                        fieldSymbols.Add(fieldSymbol);
                    }
                }
            }

            foreach (IGrouping<INamedTypeSymbol, IFieldSymbol> group in fieldSymbols.GroupBy(f => f.ContainingType))
            {
                string classSource = ProcessClassPrintableAttribute(group.Key, group.ToList(), attributeSymbol, context);
                context.AddSource($"{group.Key.Name}.Printables.cs", SourceText.From(classSource, Encoding.UTF8));
            }

            context.AddSource("PrintableFields.PrintableAttribute", SourceText.From(attributeText, Encoding.UTF8));
        }

        private string ProcessClassPrintableAttribute(INamedTypeSymbol classSymbol, List<IFieldSymbol> fields, ISymbol attributeSymbol, GeneratorExecutionContext context)
        {
            if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
            {
                return null;
            }
            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            StringBuilder source = new StringBuilder($@"
using System;
namespace {namespaceName}
{{
    public partial class {classSymbol.Name}
    {{
");
            foreach (IFieldSymbol fieldSymbol in fields)
            {
                ProcessFieldPrintableAttribute(source, fieldSymbol, attributeSymbol);
            }
            source.Append("} }");
            return source.ToString();
        }

        private void ProcessFieldPrintableAttribute(StringBuilder source, IFieldSymbol fieldSymbol, ISymbol attributeSymbol)
        {
            string fieldName = fieldSymbol.Name;
            source.Append($@"
private void Print{fieldName}()
{{
    Console.WriteLine(""{fieldName}: "" + {fieldName}.ToString());
}}");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }
    }

    class MySyntaxReceiver : ISyntaxReceiver
    {
        public List<FieldDeclarationSyntax> CandidateFields { get; } = new List<FieldDeclarationSyntax>();
        public List<MethodDeclarationSyntax> CandidateMethods { get; } = new List<MethodDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Business logic to decide what we're interested in goes here
            if (syntaxNode is FieldDeclarationSyntax fds &&
                fds.AttributeLists.Count > 0)
            {
                CandidateFields.Add(fds);
            }

            // TODO: probably you will be interested in storing information about PrintAllFields method here
        }
    }
}
