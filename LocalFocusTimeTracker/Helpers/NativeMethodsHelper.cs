using System.Runtime.InteropServices;

namespace LocalFocusTimeTracker.Helpers
{
    internal class NativeMethodsHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
