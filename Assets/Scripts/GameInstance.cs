using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameInstance : MonoBehaviour
{

    #if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    private const int GWL_STYLE = -16;
    private const uint WS_BORDER = 0x00800000;
    private const uint WS_CAPTION = 0x00C00000;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;

    private const uint WS_SYSMENU = 0x00080000; // 禁用系统菜单
    private const uint WS_MINIMIZEBOX = 0x00020000; // 禁用最小化按钮

    void Start()
    {
        // 获取当前窗口句柄
        IntPtr windowHandle = GetActiveWindow();
        // 获取当前窗口样式
        uint currentStyle = GetWindowLong(windowHandle, GWL_STYLE);
        // 去掉边框和标题栏
        SetWindowLong(windowHandle, GWL_STYLE, (currentStyle & ~(WS_BORDER | WS_CAPTION)));

        // 设置背景为透明
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0, 0, 0, 0); // 透明

        SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

        // 禁用系统菜单和最小化按钮
        SetWindowLong(windowHandle, GWL_STYLE, (currentStyle & ~(WS_SYSMENU | WS_MINIMIZEBOX)));

    }
    #endif
    
    #if UNITY_STANDALONE_OSX
    // NSWindowLevel 常量
    const int NSFloatingWindowLevel = 3;

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern IntPtr objc_getClass(string className);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern IntPtr sel_registerName(string selectorName);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern void objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg);

    [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
    private static extern void objc_msgSend_int(IntPtr receiver, IntPtr selector, int arg);

    [DllImport("libobjc.A.dylib", EntryPoint = "objc_msgSend")]
    public static extern void ObjcMsgSend_void(IntPtr receiver, IntPtr selector);

    [DllImport("libobjc.A.dylib", EntryPoint = "sel_registerName")]
    public static extern IntPtr SelRegisterName(string selectorName);

    [DllImport("libobjc.A.dylib", EntryPoint = "objc_msgSend")]
    public static extern IntPtr ObjcMsgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [DllImport("libobjc.A.dylib", EntryPoint = "objc_getClass")]
    public static extern IntPtr ObjcGetClass(string className);

    void Start()
    {
        // 获取 Unity 窗口的 NSWindow 实例
        IntPtr nsApp = objc_getClass("NSApplication");
        IntPtr sharedAppSelector = sel_registerName("sharedApplication");
        IntPtr appDelegate = objc_msgSend(nsApp, sharedAppSelector);
        IntPtr mainWindowSelector = sel_registerName("mainWindow");
        IntPtr mainWindow = objc_msgSend(appDelegate, mainWindowSelector);

        // 设置窗口透明度
        IntPtr windows = SelRegisterName("windows");
        IntPtr nsApplication = ObjcGetClass("NSApplication");
        IntPtr sharedApplication = SelRegisterName("sharedApplication");
        IntPtr app = ObjcMsgSend_IntPtr(nsApplication, sharedApplication, IntPtr.Zero);
        IntPtr windowArray = ObjcMsgSend_IntPtr(app, windows, IntPtr.Zero);
        IntPtr window = ObjcMsgSend_IntPtr(windowArray, SelRegisterName("objectAtIndex:"), (IntPtr)0);
        IntPtr setOpaque = SelRegisterName("setOpaque:");
        ObjcMsgSend_void(window, setOpaque); // 使窗口透明

        // 设置窗口层级为浮动窗口层级
        IntPtr setLevelSelector = sel_registerName("setLevel:");
        // objc_msgSend_int(mainWindow, setLevelSelector, NSFloatingWindowLevel);
        
        Debug.Log("Window is now set to topmost.");
    }
    
    #endif

}
