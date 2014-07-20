using System;
using JetBrains.ReSharper.Feature.Services.CodeCleanup;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.Interfaces
{
    public interface IProcessor
    {
        Func<CodeCleanupProfile, bool> IsProcessingActivated { get; }
    }
}