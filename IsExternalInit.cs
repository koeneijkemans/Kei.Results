using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
#if NETSTANDARD2_0 || NETSTANDARD2_1

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit { }
#endif
}
