using System;
using System.Runtime.InteropServices;

namespace DocumentForms
{
    internal static class WindowsApi
    {
        public const uint WmWindowPosChanged = 0x47;
        public const int WmNcLButtonDown = 0xA1;
        public const int HtCaption = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, ref WindowPos lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
                                               SetWindowPosFlags uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseCapture();

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPos
        {
            public IntPtr Hwnd;
            public IntPtr HwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int Flags;
        }
    }

    [Flags]
    public enum SetWindowPosFlags
    {
        SwpDrawFrame = 0x20,
        SwpFrameChanged = 0x20,
        SwpHideWindow = 0x80,
        SwpNoActivate = 0x10,
        SwpNoCopyBits = 0x100,
        SwpNoMove = 0x02,
        SwpNoOwnerZOrder = 0x200,
        SwpNoRedraw = 0x08,
        SwpNoReposition = 0x200,
        SwpNoSendChanging = 0x400,
        SwpNoSize = 0x01,
        SwpNoZOrder = 0x04,
        SwpShowWindow = 0x40
    }

}