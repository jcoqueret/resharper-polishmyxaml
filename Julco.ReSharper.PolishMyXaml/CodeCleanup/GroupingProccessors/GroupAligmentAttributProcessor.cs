using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xaml.Tree;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Settings;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors {
	public class GroupAligmentAttributProcessor : GroupAttributProcessor {
		private static readonly string[] AttributesName = new[]
            {
                FrameworkElement.HorizontalAlignmentProperty.Name,
                FrameworkElement.VerticalAlignmentProperty.Name
            };

		private static GroupAligmentAttributProcessor _instance;

		public static GroupAligmentAttributProcessor Instance {
			get { return _instance ?? (_instance = new GroupAligmentAttributProcessor()); }
		}

		public override Func<CodeCleanupProfile, bool> IsProcessingActivated {
			get { return profile => profile.GetSetting(DescriptorSettings.GroupAligmentAttributDescriptorInstance); }
		}

		public override Func<TreeElement, IEnumerable<IPropertyAttribute>> GetAttributesToProcess {
			get {
				return field => {
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