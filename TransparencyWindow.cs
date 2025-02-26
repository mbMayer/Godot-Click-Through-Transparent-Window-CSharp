using Godot;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


public partial class TransparencyWindow : Node2D
{
	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

	[DllImport("user32.dll")]
	static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

	[DllImport("user32.dll")]
	static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll")]
	static extern bool SetForegroundWindow(IntPtr hWnd);

	const int GWL_EXSTYLE = -20;
	const int WS_EX_LAYERED = 0x00080000;
	const int WS_EX_TRANSPARENT = 0x00000020;
	const int LWA_COLORKEY = 1;
	const int WS_EX_TOPMOST = 0x000000008;

	static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
	const UInt32 SWP_NOSIZE = 0x0001;
	const UInt32 SWP_NOMOVE = 0x0002;
	const UInt32 SWP_SHOWWINDOW = 0x0040;
	const UInt32 SWP_NOZORDER = 0x0004;
	const UInt32 SWP_FRAMECHANGED = 0x0020;

	IntPtr hWnd;

	public override void _Ready()
	{
		hWnd = GetActiveWindow();
		SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
		SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
		SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED | SWP_SHOWWINDOW);
	}
}
