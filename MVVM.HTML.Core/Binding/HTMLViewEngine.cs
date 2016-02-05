﻿using MVVM.HTML.Core.JavascriptEngine;
using MVVM.HTML.Core.V8JavascriptObject;

namespace MVVM.HTML.Core.Binding
{
    public class HTMLViewEngine
    {
        private IHTMLWindowProvider _HTMLWindowProvider;

        private IJavascriptSessionInjectorFactory _SessionInjectorFactory;

        internal HTMLViewEngine(IHTMLWindowProvider hTMLWindowProvider, IJavascriptSessionInjectorFactory sessionInjectorFactory)
        {
            _HTMLWindowProvider = hTMLWindowProvider;
            _SessionInjectorFactory = sessionInjectorFactory;
        }

        public HTMLViewContext GetContext()
        {
            return new HTMLViewContext(MainView, _HTMLWindowProvider.UIDispatcher, _SessionInjectorFactory);
        }

        public IWebView MainView
        {
            get { return _HTMLWindowProvider.HTMLWindow.MainFrame; }
        }
    }
}