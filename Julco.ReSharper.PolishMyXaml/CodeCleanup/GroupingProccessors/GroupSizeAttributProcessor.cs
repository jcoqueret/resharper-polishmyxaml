using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xaml.Tree;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Settings;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors
{
	public class GroupSizeAttributProcessor : GroupAttributProcessor
    {
        private static readonly string[] AttributesName = new[]
            {
                FrameworkElement.WidthProperty.Name,
                FrameworkElement.MinWidthProperty.Name,
                FrameworkElement.MaxWidthProperty.Name,
                FrameworkElement.HeightProperty.Name,
                FrameworkElement.MinHeightProperty.Name,
                FrameworkElement.MaxHeightProperty.Name
            };

        private static GroupSizeAttributProcessor _instance;

        public static GroupSizeAttributProcessor Instance
        {
            get { return _instance ?? (_instance = new GroupSizeAttributProcessor()); }
        }

        public override Func<CodeCleanupProfile, bool> IsProcessingActivated
        {
            get { return profile => profile.GetSetting(DescriptorSettings.GroupSizeAttributDescriptorInstance); }
        }

        public override Func<TreeElement, IEnumerable<IPropertyAttribute>> GetAttributesToProcess
        {
            get
            {
                return field =>
                    {
                        if (field.FirstChild == null)
                            return Enumerable.Empty<IPropertyAttribute>();

                        return (from xNode in field.FirstChild.Children<IPropertyAttribute>()
                                where AttributesName.Contains(xNode.AttributeName)
                                select xNode);
                    };
            }
        }
    }
}