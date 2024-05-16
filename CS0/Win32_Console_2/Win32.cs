using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Win32_Console.Win32;

namespace Win32_Console;

// Windows Data Types
// https://learn.microsoft.com/en-us/windows/win32/winprog/windows-data-types#long

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

	public const Int32 KEY_EVENT   = 0x0001;
	public const Int32 MOUSE_EVENT = 0x0002;
	public const Int32 WINDOW_BUFFER_SIZE_EVENT = 0x0004;
	public const Int32 MENU_EVENT  = 0x0008;
	public const Int32 FOCUS_EVENT = 0x0010;

	public const Int32 ENABLE_PROCESSED_INPUT = 0x0001;
	public const Int32 ENABLE_LINE_INPUT      = 0x0002;
	public const Int32 ENABLE_ECHO_INPUT      = 0x0004;
	public const Int32 ENABLE_WINDOW_INPUT    = 0x0008;
	public const Int32 ENABLE_MOUSE_INPUT     = 0x0010;
	public const Int32 ENABLE_INSERT_MODE     = 0x0020;
	public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
	public const Int32 ENABLE_EXTENDED_FLAGS  = 0x0080;
	public const Int32 ENABLE_AUTO_POSITION   = 0x0100;

	public const Int32 STD_INPUT_HANDLE  = -10;
	public const Int32 STD_OUTPUT_HANDLE = -11;
	public const Int32 STD_ERROR_HANDLE  = -12;

	// Win32 Strukturen
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

	[StructLayout(LayoutKind.Sequential)]
	public struct PCONSOLE_SCREEN_BUFFER_INFOEX
	{
		public     UInt32 cbSize;                // 4
		public      COORD dwSize;                // 2
		public      COORD dwCursorPosition;      // 2
		public     UInt16 wAttributes;           // 2
		public SMALL_RECT srWindow;              // 4 * 2 = 8
		public      COORD dwMaximumWindowSize;   // 2 * 2 = 4
		public     UInt16 wPopupAttributes;      // 2
		public      Int32 bFullscreenSupported;  // 4
		public     UInt32[] ColorTable;          // 4   Layout: 0x00bbggrr
		public PCONSOLE_SCREEN_BUFFER_INFOEX() { ColorTable = new UInt32[16]; }  // 16 * 4 = 64
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct INPUT_RECORD              
	{
		public Int32 EventType;
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
	public struct UCHAR                             // 2
	{
		[FieldOffset(0)] public UInt16 UnicodeChar;  // 2
		[FieldOffset(0)] public Byte AsciiChar;      // 1
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSE_EVENT_RECORD       // 16
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
	public struct MENU_EVENT_RECORD        // 4
	{
		public UInt32 dwCommandId;         // 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FOCUS_EVENT_RECORD       // 4
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
	public static extern SafeFileHandle CreateFile
		(
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
	public static extern SafeFileHandle CreateConsoleScreenBuffer
		(
			[MarshalAs(UnmanagedType.U4)] uint dwDesiredAccess,
			[MarshalAs(UnmanagedType.U4)] uint dwShareMode,
			IntPtr lpSecurityAttributes,
			[MarshalAs(UnmanagedType.U4)] uint dwFlags,
			IntPtr lpScreenBufferData //
		);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern bool GetConsoleScreenBufferInfoEx
		(
			SafeFileHandle hConsoleOutput,
			PCONSOLE_SCREEN_BUFFER_INFOEX lpConsoleScreenBufferInfoEx
		);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern SafeFileHandle GetStdHandle(Int32 nStdHandle);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern bool SetConsoleScreenBufferInfoEx
		(
			SafeFileHandle hConsoleOutput,
			PCONSOLE_SCREEN_BUFFER_INFOEX lpConsoleScreenBufferInfoEx
		);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern uint SetConsoleActiveScreenBuffer(SafeFileHandle hNewScreenBuffer);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern int WriteConsoleOutput
		(
			SafeFileHandle   hConsoleOutput,
				 CHAR_INFO[] lpBuffer,
			          uint   nNumberOfCharsToWrite,
			        IntPtr   lpNumberOfCharsWritten,
			        IntPtr   lpReserved
		);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool WriteConsoleOutputW
		(
			SafeFileHandle   hConsoleOutput,
			     CHAR_INFO[] lpBuffer,
			         COORD   dwBufferSize,
			         COORD   dwBufferCoord,
			ref SMALL_RECT   lpWriteRegion
		);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleMode(SafeFileHandle hConsoleHandle, UInt32 dwMode);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetConsoleMode(SafeFileHandle hConsoleHandle, ref UInt32 dwMode);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int GetNumberOfConsoleInputEvents(SafeFileHandle hConsoleInput, ref UInt32 lpcNumberOfEvents);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int ReadConsoleInput
		(
			SafeFileHandle hConsoleInput,
			[In, Out] INPUT_RECORD[] lpBuffer,
			UInt32 nBufferLength,
			ref UInt32 lpcNumberOfEvents
		);

	// Das hat nicht funktioniert:
	//
	//public static extern int ReadConsoleInput
	//	(
	//		SafeFileHandle hConsoleInput,
	//		//[MarshalAs(UnmanagedType.LPArray, SizeConst = 8)] INPUT_RECORD[] lpBuffer,
	//		//ref INPUT_RECORD lpBuffer,
	//		IntPtr lpBuffer,
	//		UInt32 nBufferLength,
	//		ref UInt32 lpcNumberOfEvents
	//	);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int SetCurrentConsoleFontEx(
		SafeFileHandle hConsoleOutput,
		Int32 bMaximumWindow,
		PCONSOLE_FONT_INFOEX lpConsoleCurrentFontEx
	);
}