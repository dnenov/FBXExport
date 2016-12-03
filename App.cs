// Copyright notice

// Archilizer Family Interface
// to be used in conjunction with Revit Family Editor
// Femily Editor Interface is ease of use type of plug-in -
// it main purpose is to make the process of creating and editting
// of Revit families smoother and more pleasent (less time consuming too)

#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using System.Linq;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System.Windows.Forms;
using System.Diagnostics;
#endregion

namespace FBXExporter
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary>
    class App : IExternalApplication
    {
        // class instance
        internal static App thisApp = null;
        public const string Message = "Batch exporter for FBX files.";
        /// <summary>
        /// Get absolute path to this assembly
        /// </summary>
        static string path = Assembly.GetExecutingAssembly().Location;
        static string contentPath = Path.GetDirectoryName(Path.GetDirectoryName(path)) + "/";
        //static string helpFile = "file:///C:/ProgramData/Autodesk/ApplicationPlugins/ViewportsArrange.bundle/Content/Family%20Editor%20Interface%20_%20Revit%20_%20Autodesk%20App%20Store.html";
        static string largeIcon = contentPath + "modeless32.png";
        static string smallIcon = contentPath + "modeless16.png";
        #region Ribbon
        /// <summary>
        /// Use embedded image to load as an icon for the ribbon
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static private BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(name);
                return BitmapFrame.Create(s);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Add ribbon panel 
        /// </summary>
        /// <param name="a"></param>
        private void AddRibbonPanel(UIControlledApplication a)
        {
            List<RibbonPanel> panels = a.GetRibbonPanels();
            Autodesk.Revit.UI.RibbonPanel rvtRibbonPanel = null;
            if (panels.FirstOrDefault(x => x.Name.Equals("Archilizer", StringComparison.OrdinalIgnoreCase)) == null)
            {
                rvtRibbonPanel = a.CreateRibbonPanel("Archilizer");
            }
            else
            {
                rvtRibbonPanel = panels.FirstOrDefault(x => x.Name.Equals("Archilizer", StringComparison.OrdinalIgnoreCase)) as RibbonPanel;
            }
            PulldownButtonData data = new PulldownButtonData("Options", "FBXExporter");

            BitmapSource img32 = new BitmapImage(new Uri(@largeIcon));
            BitmapSource img16 = new BitmapImage(new Uri(@smallIcon));

            //ContextualHelp ch = new ContextualHelp(ContextualHelpType.Url, @helpFile);

            PushButton fbxExporter = rvtRibbonPanel.AddItem(new PushButtonData("FBXExporter", "FBXExporter", path,
                "FBXExporter.Command")) as PushButton;

            fbxExporter.Image = img16;
            fbxExporter.LargeImage = img32;
            fbxExporter.ToolTip = Message;
            //familyEI.SetContextualHelp(ch);
        }
        #endregion

        public Result OnStartup(UIControlledApplication a)
        {
            ControlledApplication c_app = a.ControlledApplication;
            AddRibbonPanel(a);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
