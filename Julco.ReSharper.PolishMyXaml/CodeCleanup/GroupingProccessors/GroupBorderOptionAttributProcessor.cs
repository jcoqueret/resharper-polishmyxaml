namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls;
	using System.Windows.Shapes;

	using JetBrains.ReSharper.Feature.Services.CodeCleanup;
	using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
	using JetBrains.ReSharper.Psi.Tree;
	using JetBrains.ReSharper.Psi.Xaml.Tree;

	using Settings;

	public class GroupBorderOptionAttributProcessor : GroupAttributProcessor {
		private static readonly string[] AttributesName = new[]
		{
			Border.BorderBrushProperty.Name,
			Border.BorderThicknessProperty.Name,
		};

		private static GroupBorderOptionAttributProcessor _instance;

		public static GroupBorderOptionAttributProcessor Instance {
			get { return _instance ?? (_instance = new GroupBorderOptionAttributProcessor()); }
		}

		public override Func<CodeCleanupProfile, bool> IsProcessingActivated {
			get { return profile => profile.GetSetting(DescriptorSettings.GroupBorderOptionAttributDescriptorInstance); }
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