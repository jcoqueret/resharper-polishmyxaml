namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;
	using System.Windows.Shapes;

	using JetBrains.ReSharper.Feature.Services.CodeCleanup;
	using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
	using JetBrains.ReSharper.Psi.Tree;
	using JetBrains.ReSharper.Psi.Xaml.Tree;

	using Settings;

	public class GroupGeometryDefinitionAttributProcessor : GroupAttributProcessor {
		private static readonly string[] AttributesName = new[]
		{
			Line.X1Property.Name,
			Line.X2Property.Name,
			Line.Y1Property.Name,
			Line.Y2Property.Name,
			EllipseGeometry.RadiusXProperty.Name,
			EllipseGeometry.RadiusYProperty.Name,
			RectangleGeometry.RadiusXProperty.Name,
			RectangleGeometry.RadiusYProperty.Name,
			Rectangle.RadiusXProperty.Name,
			Rectangle.RadiusYProperty.Name
		};

		private static GroupGeometryDefinitionAttributProcessor _instance;

		public static GroupGeometryDefinitionAttributProcessor Instance {
			get { return _instance ?? (_instance = new GroupGeometryDefinitionAttributProcessor()); }
		}

		public override Func<CodeCleanupProfile, bool> IsProcessingActivated {
			get { return profile => profile.GetSetting(DescriptorSettings.GroupGeometryDefinitionAttributDescriptorInstance); }
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