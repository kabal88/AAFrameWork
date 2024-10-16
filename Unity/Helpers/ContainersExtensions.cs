using System.Runtime.CompilerServices;
using AAFramework.Unity.Identifiers;

namespace Helpers
{
    public static class ContainersExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsIdentifiers(this IdentifierContainer identifierContainer, IdentifierContainer other)
        {
            if (!identifierContainer && !other)
                return true;
            return identifierContainer && other && identifierContainer.Id == other.Id;
        }
    }
}