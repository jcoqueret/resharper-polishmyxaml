using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xaml.Tree;
using Julco.ReSharper.PolishMyXaml.CodeCleanup.Interfaces;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.GroupingProccessors
{
    public abstract class GroupAttributProcessor : ITreeElementProcessor
    {
        public abstract Func<CodeCleanupProfile, bool> IsProcessingActivated { get; }

        public abstract Func<TreeElement, IEnumerable<IPropertyAttribute>> GetAttributesToProcess { get; }

        public void Process(CodeCleanupProfile profile, TreeElement fieldNode)
        {
            //if (!IsProcessingActivated(profile))
            //    return;

            var attributes = GetAttributesToProcess(fieldNode).ToList();
            if (attributes.Count > 1)
            {
                for (int i = attributes.Count-1; i > 0 ; i--)
                {
                    //var previous = attributes[i].PrevSibling;
                    //if (previous is IPropertyAttribute && attributes.Contains(previous as IPropertyAttribute))
                    //{
                    //    continue;
                    //}
                    //if (previous is XmlFloatingTextToken)
                    //{
                    //    var previousBis = previous.PrevSibling;
                    //    if (previousBis is IPropertyAttribute && attributes.Contains(previousBis as IPropertyAttribute))
                    //    {
                    //        continue;
                    //    }
                    //}

                    ModificationUtil.DeleteChild(attributes[i]);
                    ModificationUtil.AddChildAfter(attributes.First(), attributes[i]);
                }
            }
        }


    }
}