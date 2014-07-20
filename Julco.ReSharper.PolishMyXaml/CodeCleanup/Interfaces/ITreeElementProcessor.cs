using JetBrains.ReSharper.Feature.Services.CodeCleanup;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xaml.Tree;

namespace Julco.ReSharper.PolishMyXaml.CodeCleanup.Interfaces
{
    public interface  ITreeElementProcessor : IProcessor
    {
        void Process(CodeCleanupProfile profile, TreeElement fieldNode);
    }
}