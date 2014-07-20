using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xaml.Tree;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Settings;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors {
	public class GroupGridAttributProcessor : GroupAttributProcessor {
		private static GroupGridAttributProcessor _instance;

		public static GroupGridAttributProcessor Instance {
			get { return _instance ?? (_instance = new GroupGridAttributProcessor()); }
		}

		public override Func<CodeCleanupProfile, bool> IsProcessingActivated {
			get { return profile => profile.GetSetting(DescriptorSettings.GroupGridAttributDescriptorInstance); }
		}

		public override Func<TreeElement, IEnumerable<IPropertyAttribute>> GetAttributesToProcess {
			get {
				return field => {
						if (field.FirstChild == null)
							return Enumerable.Empty<IPropertyAttribute>();

						return (from xNode in field.FirstChild.Children<IPropertyAttribute>()
								where xNode.AttributeName.Contains("Grid.")
								select xNode);
					};
			}
		}
	}
}