namespace Utilits
{
    internal static class ExtensionObjects
    {
        public static bool IsNull(this object self) =>
            self is null;
        public static bool IsNotExist(this UnityEngine.Object target) =>
            target == null;
        public static bool IsExist(this UnityEngine.Object target) =>
            target != null;
    }
}
