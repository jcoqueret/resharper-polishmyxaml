using System;
using System.Collections.Generic;

using JetBrains.Application;
using JetBrains.Application.Progress;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xaml;
using JetBrains.ReSharper.Psi.Xaml.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;

using Julco.ReSharper.PolishMyXaml.CodeCleanup.Interfaces;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Settings;
using System.Linq;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup
{
    using JetBrains.ReSharper.Psi.Files;

    [CodeCleanupModule]
    public class PolishXamlCodeCleanup : ICodeCleanupModule
    {
        private readonly IShellLocks _shellLocks;
        private IEnumerable<IProcessor> _processors;

        public PsiLanguageType LanguageType
        {
            get { return XamlLanguage.Instance; }
        }

        public ICollection<CodeCleanupOptionDescriptor> Descriptors
        {
            get { return Settings.DescriptorSettings.ProvideDescriptors(); }
        }

        public bool IsAvailableOnSelection
        {
            get { return false; }
        }

        public PolishXamlCodeCleanup(IShellLocks shellLocks)
        {
            _shellLocks = shellLocks;
        }

        public void SetDefaultSetting(CodeCleanupProfile profile, JetBrains.ReSharper.Feature.Services.CodeCleanup.CodeCleanup.DefaultProfileType profileType)
        {
            switch (profileType)
            {
                case JetBrains.ReSharper.Feature.Services.CodeCleanup.CodeCleanup.DefaultProfileType.FULL:
                    profile.SetSetting(DescriptorSettings.GroupGridAttributDescriptorInstance, true);
                    profile.SetSetting(DescriptorSettings.GroupAligmentAttributDescriptorInstance, true);
                    profile.SetSetting(DescriptorSettings.GroupSizeAttributDescriptorInstance, true);
                    break;

                case JetBrains.ReSharper.Feature.Services.CodeCleanup.CodeCleanup.DefaultProfileType.REFORMAT:
                    profile.SetSetting(DescriptorSettings.GroupGridAttributDescriptorInstance, false);
                    profile.SetSetting(DescriptorSettings.GroupAligmentAttributDescriptorInstance, false);
                    profile.SetSetting(DescriptorSettings.GroupSizeAttributDescriptorInstance, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("profileType");
            }
        }

        public bool IsAvailable(IPsiSourceFile sourceFile)
        {
            return sourceFile.GetDominantPsiFile<XamlLanguage>() != null;
        }

        public void Process(IPsiSourceFile sourceFile, IRangeMarker rangeMarker, CodeCleanupProfile profile, IProgressIndicator progressIndicator)
        {
            IXamlFile xamlFile = sourceFile.GetDominantPsiFile<XamlLanguage>() as IXamlFile;
            if (xamlFile == null)
                return;

            _processors = ProcessorCatalog.GetActivatedProcessors(profile);

            xamlFile.GetPsiServices().Transactions.Execute(
                "Code cleanup",
                () =>
                {
                    using (_shellLocks.UsingWriteLock())
                    {
                        if (xamlFile.FirstChild != null)
                        {
                            ProcessFile(xamlFile, profile, xamlFile.FirstChild);
                        }
                    }
                });
        }

        private void ProcessFile(IXamlFile xamlFile, CodeCleanupProfile profile, ITreeNode rootNode)
        {

            rootNode.ProcessChildren<ITreeNode>(node =>
                {
                    //var typeNode = node as ITypeDeclaration;
                    //if (typeNode != null)
                    //{
                    //    HandleTypeDeclarationNode(profile, typeNode);
                    //}

                    TreeElement treeElement = node as TreeElement;
                    if (treeElement != null)
                    {
                        HandleFieldDeclaration(profile, treeElement);
                    }
                });
        }

        private void HandleFieldDeclaration(CodeCleanupProfile profile, TreeElement fieldNode)
        {
            //Execute all treeElementProcessors
            IEnumerable<ITreeElementProcessor> treeElementProcessors = _processors.OfType<ITreeElementProcessor>();
            foreach (ITreeElementProcessor treeElementProcessor in treeElementProcessors)
            {
                treeElementProcessor.Process(profile, fieldNode);
            }
        }

        private void HandleTypeDeclarationNode(CodeCleanupProfile profile, ITypeDeclaration typeDeclarationNode)
        {
            var headerNode = typeDeclarationNode.FirstChild;
            if (headerNode != null)
            {
                var identifier = headerNode.Children<IXamlIdentifier>().FirstOrDefault();
                var classAttribute = headerNode.Children<IXClassAttribute>().FirstOrDefault();
                if (identifier == null || classAttribute == null)
                    return;

                var namespaceAlias = headerNode.Children<INamespaceAlias>().ToList();
                //var properties = headerNode.Children<IPropertyAttribute>().ToList();

                ModificationUtil.DeleteChild(classAttribute);
                namespaceAlias.ForEach(ModificationUtil.DeleteChild);
                //properties.ForEach(ModificationUtil.DeleteChild);



                //properties.OrderByDescending(p => p.AttributeName).ForEach(node => ModificationUtil.AddChildAfter(identifier, node));
                namespaceAlias.OrderByDescending(n => n.DeclaredName).ForEach(node =>
                    {
                        ModificationUtil.AddChildAfter(identifier, node);

                        XmlFloatingTextToken floatingText = new XmlFloatingTextToken(
                            XmlTokenTypes.GetInstance(XamlLanguage.Instance).NEW_LINE,
                            " " );
                        

                        ModificationUtil.AddChildAfter(identifier, floatingText);

                        });
                
                ModificationUtil.AddChildAfter(identifier, classAttribute);



            }

        }

    }
}