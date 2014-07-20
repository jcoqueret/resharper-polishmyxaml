using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.Settings
{
    public static class DescriptorSettings
    {
        public const string XamlCategory = "XAML" ;

        public static readonly GroupGridAttributDescriptor GroupGridAttributDescriptorInstance = new GroupGridAttributDescriptor();
        public static readonly GroupAligmentAttributDescriptor GroupAligmentAttributDescriptorInstance = new GroupAligmentAttributDescriptor();
        public static readonly GroupSizeAttributDescriptor GroupSizeAttributDescriptorInstance = new GroupSizeAttributDescriptor();
		public static readonly GroupGeometryDefinitionAttributDescriptor GroupGeometryDefinitionAttributDescriptorInstance = new GroupGeometryDefinitionAttributDescriptor();
		public static readonly GroupBorderOptionAttributDescriptor GroupBorderOptionAttributDescriptorInstance = new GroupBorderOptionAttributDescriptor();
		public static readonly GroupCanvasAttributDescriptor GroupCanvasAttributDescriptorInstance = new GroupCanvasAttributDescriptor();
		public static readonly GroupStrokeOptionAttributDescriptor GroupStrokeOptionAttributDescriptorInstance = new GroupStrokeOptionAttributDescriptor();

        public static ICollection<CodeCleanupOptionDescriptor> ProvideDescriptors()
        {
            return new CodeCleanupOptionDescriptor[]
                {
                    GroupGridAttributDescriptorInstance,
                    GroupAligmentAttributDescriptorInstance,
                    GroupSizeAttributDescriptorInstance,
                    GroupGeometryDefinitionAttributDescriptorInstance,
                    GroupBorderOptionAttributDescriptorInstance,
                    GroupCanvasAttributDescriptorInstance,
                    GroupStrokeOptionAttributDescriptorInstance,
                };
        }
    }

    #region Descriptors definitions

    [DefaultValue(false)]
    [DisplayName("Regroup grid position attributes")]
    [Category(DescriptorSettings.XamlCategory)]
    public class GroupGridAttributDescriptor : CodeCleanupBoolOptionDescriptor
    {
        private const string DescriptorName = "GroupGridAttribut";

        public GroupGridAttributDescriptor()
            : base(DescriptorName)
        {
        }
    }

    [DefaultValue(false)]
    [DisplayName("Regroup aligment attributes")]
    [Category(DescriptorSettings.XamlCategory)]
    public class GroupAligmentAttributDescriptor : CodeCleanupBoolOptionDescriptor
    {
        private const string DescriptorName = "GroupAligmentAttribut";

        public GroupAligmentAttributDescriptor()
            : base(DescriptorName)
        {
        }
    }

    [DefaultValue(false)]
    [DisplayName("Regroup size attributes")]
    [Category(DescriptorSettings.XamlCategory)]
    public class GroupSizeAttributDescriptor : CodeCleanupBoolOptionDescriptor
    {
        private const string DescriptorName = "GroupSizeAttribut";

        public GroupSizeAttributDescriptor()
            : base(DescriptorName)
        {
        }
    }

	[DefaultValue(false)]
	[DisplayName("Regroup geometry definition attributes")]
	[Category(DescriptorSettings.XamlCategory)]
	public class GroupGeometryDefinitionAttributDescriptor : CodeCleanupBoolOptionDescriptor {
		private const string DescriptorName = "GroupGeometryDefinitionAttribut";

		public GroupGeometryDefinitionAttributDescriptor()
			: base(DescriptorName) {
		}
	}

	[DefaultValue(false)]
	[DisplayName("Regroup border option attributes")]
	[Category(DescriptorSettings.XamlCategory)]
	public class GroupBorderOptionAttributDescriptor : CodeCleanupBoolOptionDescriptor {
		private const string DescriptorName = "GroupBorderOptionAttribut";

		public GroupBorderOptionAttributDescriptor()
			: base(DescriptorName) {
		}
	}

	[DefaultValue(false)]
	[DisplayName("Regroup cavas position attributes")]
	[Category(DescriptorSettings.XamlCategory)]
	public class GroupCanvasAttributDescriptor : CodeCleanupBoolOptionDescriptor {
		private const string DescriptorName = "GroupCanvasAttribut";

		public GroupCanvasAttributDescriptor()
			: base(DescriptorName) {
		}
	}

	[DefaultValue(false)]
	[DisplayName("Regroup stroke options attributes")]
	[Category(DescriptorSettings.XamlCategory)]
	public class GroupStrokeOptionAttributDescriptor : CodeCleanupBoolOptionDescriptor {
		private const string DescriptorName = "GroupStrokeOptionAttribut";

		public GroupStrokeOptionAttributDescriptor()
			: base(DescriptorName) {
		}
	}

    #endregion


}
