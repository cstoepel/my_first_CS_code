using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Win32_Console.Win32;

namespace Win32_Console;
// Info zu Win32 function calls (P/Invoke)
// https://www.pinvoke.net/index.aspx

// Custom text color in C# console application
// https://stackoverflow.com/questions/7937256/custom-text-color-in-c-sharp-console-application

// Windows Data Types
// https://learn.microsoft.com/en-us/windows/win32/winprog/windows-data-types#long

// Unsafe code, pointer types, and function pointers
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code?redirectedfrom=MSDN#fixed-size-buffers

public static class Win32
{
	// UnmanagedType Enumeration:
	// https://learn.microsoft.com/de-de/dotnet/api/system.runtime.interopservices.unmanagedtype?view=net-8.0
	//
	// UnmanagedTyp.U1     uint8
	// UnmanagedTyp.U2     uint16
	// UnmanagedTyp.U4     uint32
	// UnmanagedTyp.U8     uint64
	// UnmanagedType.Bool  Win32-BOOL 4 Byte
	// UnmanagedType.R4    Eine 4-Byte-Gleitkommazahl (float)
	// UnmanagedType.R8    Eine 8-Byte-Gleitkommazahl (double)
	//
	// IntPtr              Represents a signed integer where the bit-width is the same as a pointer.
	//                     https://learn.microsoft.com/en-us/dotnet/api/system.intptr?view=net-8.0
	// MarshalAsAttribute Class
	//                     https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshalasattribute?view=net-8.0

	// Konstanten
	public const Int32 CONSOLE_TEXTMODE_BUFFER = 1;

	public const UInt16 KEY_EVENT   = 0x00000001;
	public const UInt16 MOUSE_EVENT = 0x00000002;
	public const UInt16 WINDOW_BUFFER_SIZE_EVENT = 0x00000004;
	public const UInt16 MENU_EVENT  = 0x00000008;
	public const UInt16 FOCUS_EVENT = 0x00000010;

	public const UInt32 ENABLE_PROCESSED_INPUT = 0x00000001;
	public const UInt32 ENABLE_LINE_INPUT      = 0x00000002;
	public const UInt32 ENABLE_ECHO_INPUT      = 0x00000004;
	public const UInt32 ENABLE_WINDOW_INPUT    = 0x00000008;
	public const UInt32 ENABLE_MOUSE_INPUT     = 0x00000010;
	public const UInt32 ENABLE_INSERT_MODE     = 0x00000020;
	public const UInt32 ENABLE_QUICK_EDIT_MODE = 0x00000040;
	public const UInt32 ENABLE_EXTENDED_FLAGS  = 0x00000080;
	public const UInt32 ENABLE_AUTO_POSITION   = 0x00000100;

	public const Int32 STD_INPUT_HANDLE  = -10;
	public const Int32 STD_OUTPUT_HANDLE = -11;
	public const Int32 STD_ERROR_HANDLE  = -12;

	public const UInt32 GENERIC_READ     = 0x80000000;
	public const UInt32 GENERIC_WRITE    = 0x40000000;
	public const UInt32 FILE_SHARE_READ  = 0x00000001;
	public const UInt32 FILE_SHARE_WRITE = 0x00000002;

	// Win32 Strukturen
	// https://learn.microsoft.com/en-us/windows/console/console-structures

	[StructLayout(LayoutKind.Sequential)]
	public struct COORD { public Int16 X, Y; };

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT { public Int16 X0, Y0, X1, Y1; }

	[StructLayout(LayoutKind.Sequential)]
	public struct SMALL_RECT { public Int16 X0, Y0, X1, Y1; }

	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct CHAR_INFO
	{
		// wie C Union
		[FieldOffset(0)] public char  UnicodeChar;
		[FieldOffset(0)] public byte  AsciiChar;
		[FieldOffset(2)] public Int16 Attributes;
	}

	//[StructLayout(LayoutKind.Sequential)]
	//public struct PCONSOLE_SCREEN_BUFFER_INFOEX
	//{
	//	public UInt32 cbSize;                // 4
	//	public COORD  dwSize;                // 2
	//	public COORD  dwCursorPosition;      // 2
	//	public UInt16 wAttributes;           // 2
	//	public SMALL_RECT srWindow;          // 4 * 2 = 8
	//	public COORD  dwMaximumWindowSize;   // 2 * 2 = 4
	//	public UInt16 wPopupAttributes;      // 2
	//	public Int32  bFullscreenSupported;  // 4
	//	public fixed UInt32 ColorTable[16];        // 16 * 4 = 56   Layout: 0x00bbggrr
	//}

	[StructLayout(LayoutKind.Sequential)]
	public struct PCONSOLE_SCREEN_BUFFER_INFOEX
	{
		public int cbSize;
		public COORD dwSize;
		public COORD dwCursorPosition;
		public ushort wAttributes;
		public SMALL_RECT srWindow;
		public COORD dwMaximumWindowSize;
		public ushort wPopupAttributes;
		public bool bFullscreenSupported;
		public COLORREF color00;
		public COLORREF color01;
		public COLORREF color02;
		public COLORREF color03;
		public COLORREF color04;
		public COLORREF color05;
		public COLORREF color06;
		public COLORREF color07;
		public COLORREF color08;
		public COLORREF color09;
		public COLORREF color10;
		public COLORREF color11;
		public COLORREF color12;
		public COLORREF color13;
		public COLORREF color14;
		public COLORREF color15;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct COLORREF
	{
		public uint dwColor;
		public COLORREF(Color color) {dwColor = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);}
		public COLORREF(uint r, uint g, uint b){dwColor = r + (g << 8) + (b << 16);}
		public Color GetColor(){
			return Color.FromArgb((int)(0x000000fful & dwColor),
								  (int)(0x0000ff00ul & dwColor) >> 8,
								  (int)(0x00ff0000ul & dwColor) >> 16);
		}
		public void SetColor(Color color){dwColor = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct INPUT_RECORD
	{
		public UInt16 EventType;
		public Event EventRecord;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct Event
	{
		[FieldOffset(0)] public KEY_EVENT_RECORD KeyEvent;
		[FieldOffset(0)] public MOUSE_EVENT_RECORD MouseEvent;
		[FieldOffset(0)] public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
		[FieldOffset(0)] public MENU_EVENT_RECORD MenuEvent;
		[FieldOffset(0)] public FOCUS_EVENT_RECORD FocusEvent;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct KEY_EVENT_RECORD           // 24
	{
		public Int32 bKeyDown;               // 4  BOOL
		public Int16 wRepeatCount;           // 4
		public Int16 wVirtualKeyCode;        // 4
		public Int16 wVirtualScanCode;       // 4
		public UCHAR uChar;
		public Int32 dwControlKeyState;      // 4
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct UCHAR                              // 2
	{
		[FieldOffset(0)] public UInt16 UnicodeChar;  // 2
		[FieldOffset(0)] public Byte AsciiChar;      // 1
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSE_EVENT_RECORD        // 16
	{
		public COORD dwMousePosition;      // 2 * 2 = 4
		public Int32 dwButtonState;        // 4
		public Int32 dwControlKeyState;    // 4
		public Int32 dwEventFlags;         // 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct WINDOW_BUFFER_SIZE_RECORD // 4
	{
		public COORD dwSize;                // 2 * 2 = 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MENU_EVENT_RECORD         // 4
	{
		public UInt32 dwCommandId;         // 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FOCUS_EVENT_RECORD        // 4
	{
		public Int32 bSetFocus;            // 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PCONSOLE_FONT_INFOEX
	{
		UInt32 cbSize;
		Int32  nFont;
		COORD  dwFontSize;
		UInt32 FontFamily;
		UInt32 FontWeight;
		char[] FaceName;      // Zero terminated string
	}

	// ___Win32 Funktionen_______________________________________________________

	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	public static extern SafeFileHandle CreateFile (
			                                string fileName,
			[MarshalAs(UnmanagedType.U4)]     uint fileAccess,
			[MarshalAs(UnmanagedType.U4)]     uint fileShare,
			                                IntPtr securityAttributes,
			[MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
			[MarshalAs(UnmanagedType.U4)]      int flags,
			                                IntPtr template
		);

	// ___Console API Functions_______________________________________________
	// https://learn.microsoft.com/en-us/windows/console/console-functions

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateConsoleScreenBuffer
		(
			UInt32 dwDesiredAccess,
			UInt32 dwShareMode,
			IntPtr lpSecurityAttributes,
			UInt32 dwFlags,
			IntPtr lpScreenBufferData //
		);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern IntPtr GetStdHandle(Int32 nStdHandle);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref PCONSOLE_SCREEN_BUFFER_INFOEX csbe);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref PCONSOLE_SCREEN_BUFFER_INFOEX csbe);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern UInt32 SetConsoleActiveScreenBuffer ( IntPtr hNewScreenBuffer );

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern Int32 WriteConsoleOutput (
		     IntPtr hConsoleOutput,
		CHAR_INFO[] lpBuffer,
		       uint nNumberOfCharsToWrite,
		     IntPtr lpNumberOfCharsWritten,
			 IntPtr lpReserved);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool WriteConsoleOutputW (
		     IntPtr hConsoleOutput,
		CHAR_INFO[] lpBuffer,
		      COORD dwBufferSize,
		      COORD dwBufferCoord,
	 ref SMALL_RECT lpWriteRegion );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleMode ( IntPtr hConsoleHandle, UInt32 dwMode );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetConsoleMode ( IntPtr hConsoleHandle, ref UInt32 dwMode );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern Int32 GetNumberOfConsoleInputEvents ( IntPtr hConsoleInput, ref UInt32 lpcNumberOfEvents );

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern Int32 ReadConsoleInput (
		IntPtr hConsoleInput,
		[In, Out] INPUT_RECORD[] lpBuffer,
		UInt32 nBufferLength,
		ref UInt32 lpcNumberOfEvents);

	// Diese drei Varianten für lpBuffer haben nicht funktioniert:
	// 1. [MarshalAs(UnmanagedType.LPArray, SizeConst = 8)] INPUT_RECORD[] lpBuffer,
	// 2. ref INPUT_RECORD lpBuffer,
	// 3. IntPtr lpBuffer,

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern Int32 SetCurrentConsoleFontEx (
		IntPtr hConsoleOutput,
		Int32 bMaximumWindow,
		PCONSOLE_FONT_INFOEX lpConsoleCurrentFontEx );
}