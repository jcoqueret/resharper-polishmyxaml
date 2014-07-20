using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Interfaces;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup
{
    public class ProcessorCatalog
    {
        public static readonly IEnumerable<IProcessor> AvailableProcessors = new List<IProcessor>()
            {
                //Grouping processors
                GroupGridAttributProcessor.Instance,
                GroupAligmentAttributProcessor.Instance,
                GroupSizeAttributProcessor.Instance,
				GroupGeometryDefinitionAttributProcessor.Instance,
				GroupBorderOptionAttributProcessor.Instance,
				GroupCanvasAttributProcessor.Instance,
				GroupStrokeOptionAttributProcessor.Instance
            };

        public static IEnumerable<IProcessor> GetActivatedProcessors(CodeCleanupProfile profile)
        {
            return AvailableProcessors.Where(processor => processor.IsProcessingActivated(profile));
        }
    }
}