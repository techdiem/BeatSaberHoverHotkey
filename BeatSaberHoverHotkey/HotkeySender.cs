using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HoverHotkey
{
    public static class HotkeySender
    {
		[DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = false)]
		static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        #region SendInput INPUT definition

        private enum InputType
		{
			INPUT_MOUSE = 0,
			INPUT_KEYBOARD = 1,
			INPUT_HARDWARE = 2,
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct INPUT
		{
			internal InputType type;
			internal InputUnion U;
			internal static int Size
			{
				get { return Marshal.SizeOf(typeof(INPUT)); }
			}
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct InputUnion
		{
			[FieldOffset(0)]
			internal MOUSEINPUT mi;
			[FieldOffset(0)]
			internal KEYBDINPUT ki;
			[FieldOffset(0)]
			internal HARDWAREINPUT hi;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct MOUSEINPUT
		{
			internal int dx;
			internal int dy;
			internal int mouseData;
			internal MOUSEEVENTF dwFlags;
			internal uint time;
			internal UIntPtr dwExtraInfo;
		}

		[Flags]
		internal enum MOUSEEVENTF : uint
		{
			ABSOLUTE = 0x8000,
			HWHEEL = 0x01000,
			MOVE = 0x0001,
			MOVE_NOCOALESCE = 0x2000,
			LEFTDOWN = 0x0002,
			LEFTUP = 0x0004,
			RIGHTDOWN = 0x0008,
			RIGHTUP = 0x0010,
			MIDDLEDOWN = 0x0020,
			MIDDLEUP = 0x0040,
			VIRTUALDESK = 0x4000,
			WHEEL = 0x0800,
			XDOWN = 0x0080,
			XUP = 0x0100
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct HARDWAREINPUT
		{
			internal int uMsg;
			internal short wParamL;
			internal short wParamH;
		}


		[StructLayout(LayoutKind.Sequential)]
		private struct KEYBDINPUT : IDisposable
		{
			public short wVk;
			public short Scan;
			public KEYEVENTF dwFlags;
			public int time;
			public IntPtr dwExtraInfo;

			public void Dispose()
			{
			}
		}

		[Flags()]
		private enum KEYEVENTF
		{
			EXTENDEDKEY = 0x0001,
			KEYUP = 0x0002,
			SCANCODE = 0x0008,
			UNICODE = 0x0004,
		}

        #endregion


        public static void PressHotkey(short keycode)
        {
			//Key Down
			INPUT keyDown = new INPUT();
			keyDown.type = InputType.INPUT_KEYBOARD;
			keyDown.U.ki.wVk = keycode;

			SendInput(1, new INPUT[] { keyDown }, Marshal.SizeOf(keyDown));

			//Key Up
			INPUT keyUp = new INPUT();
			keyUp.type = InputType.INPUT_KEYBOARD;
			keyUp.U.ki.wVk = keycode;
			keyUp.U.ki.dwFlags = KEYEVENTF.KEYUP;

			SendInput(1, new INPUT[] { keyUp }, Marshal.SizeOf(keyUp));
		}

	}
}
