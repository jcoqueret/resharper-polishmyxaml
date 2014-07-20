namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using JetBrains.ReSharper.Feature.Services.CodeCleanup;
	using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
	using JetBrains.ReSharper.Psi.Tree;
	using JetBrains.ReSharper.Psi.Xaml.Tree;

	using Settings;

	public class GroupCanvasAttributProcessor : GroupAttributProcessor {
		private static GroupCanvasAttributProcessor _instance;

		public static GroupCanvasAttributProcessor Instance {
			get { return _instance ?? (_instance = new GroupCanvasAttributProcessor()); }
		}

		public override Func<CodeCleanupProfile, bool> IsProcessingActivated {
			get { return profile => profile.GetSetting(DescriptorSettings.GroupCanvasAttributDescriptorInstance); }
		}

		public override Func<TreeElement, IEnumerable<IPropertyAttribute>> GetAttributesToProcess {
			get {
				return field => {
					if (field.FirstChild == null)
						return Enumerable.Empty<IPropertyAttribute>();

					return (from xNode in field.FirstChild.Children<IPropertyAttribute>()
							where xNode.AttributeName.Contains("Canvas.")
							select xNode);
				};
			}
		}
	}
}