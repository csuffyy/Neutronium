﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chromium;
using Chromium.WebBrowser;
using HTMEngine.ChromiumFX.WPF;
using HTML_WPF.Component;
using MVVM.HTML.Core.JavascriptEngine.Window;

namespace HTMEngine.ChromiumFX.EngineBinding 
{
    internal class ChromiumFXWPFWindow : IWPFWebWindow 
    {
        private readonly ChromiumFxControl _ChromiumFxControl;
        private readonly ChromiumWebBrowser _ChromiumWebBrowser;
        private readonly ChromiumFxControlHTMLWindow _ChromiumFxControlHTMLWindow;

        public UIElement UIElement => _ChromiumFxControl;
        public bool IsUIElementAlwaysTopMost => true;
        public IHTMLWindow HTMLWindow => _ChromiumFxControlHTMLWindow;

        public ChromiumFXWPFWindow()
        {
            _ChromiumFxControl = new ChromiumFxControl()
            {
                Visibility = Visibility.Hidden,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ContextMenu = new ContextMenu() { Visibility = Visibility.Collapsed }
            };
            _ChromiumWebBrowser = _ChromiumFxControl.ChromiumWebBrowser;
            var dispatcher = new WPFUIDispatcher(_ChromiumFxControl.Dispatcher);
            _ChromiumFxControlHTMLWindow = new ChromiumFxControlHTMLWindow(_ChromiumWebBrowser, dispatcher);
        }

        public void Inject(Key keyToInject) 
        {
            var cxKeyEvent = new CfxKeyEvent() 
            {
                WindowsKeyCode = (int) keyToInject
            };

            _ChromiumWebBrowser.Browser.Host.SendKeyEvent(cxKeyEvent);
        }

        public bool OnDebugToolsRequest() 
        {
            CfxWindowInfo windowInfo = new CfxWindowInfo();

            windowInfo.Style = Chromium.WindowStyle.WS_OVERLAPPEDWINDOW | Chromium.WindowStyle.WS_CLIPCHILDREN | Chromium.WindowStyle.WS_CLIPSIBLINGS | Chromium.WindowStyle.WS_VISIBLE;
            windowInfo.ParentWindow = IntPtr.Zero;
            windowInfo.WindowName = "Dev Tools";
            windowInfo.X = 200;
            windowInfo.Y = 200;
            windowInfo.Width = 800;
            windowInfo.Height = 600;

            _ChromiumWebBrowser.BrowserHost.ShowDevTools(windowInfo, new CfxClient(), new CfxBrowserSettings(), null);
            return true;
        }

        public void CloseDebugTools() 
        {
            _ChromiumWebBrowser.BrowserHost.CloseDevTools();
        }

        public void Dispose()
        {
            _ChromiumWebBrowser.Dispose();
        }
    }
}
