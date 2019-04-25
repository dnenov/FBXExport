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
using System.Collections;
#endregion

namespace FBXExporter
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary>
    class App : IExternalApplication
    {
        private static object TheInternalDoingPart(UIControlledApplication CApp, string TabName, string PanelName)
        {
            IList ERPs = null;

            ERPs = CApp.GetRibbonPanels(TabName);

            Autodesk.Revit.UI.RibbonPanel NewOrExtgRevitPanel = null;

            foreach (Autodesk.Revit.UI.RibbonPanel Pan in ERPs)
            {
                if (Pan.Name == PanelName)
                {
                    NewOrExtgRevitPanel = Pan;
                    goto FoundSoJumpPastNew;
                }
            }

            Autodesk.Revit.UI.RibbonPanel NewRevitPanel = null;

            NewRevitPanel = CApp.CreateRibbonPanel(TabName, PanelName);

            NewOrExtgRevitPanel = NewRevitPanel;
            FoundSoJumpPastNew:

            return NewOrExtgRevitPanel;
        }

        // class instance
        internal static App thisApp = null;
        public const string Message = "Batch exporter for FBX files.";
        /// <summary>
        /// Get absolute path to this assembly
        /// </summary>
        static string path = Assembly.GetExecutingAssembly().Location;
        static string contentPath = Path.GetDirectoryName(Path.GetDirectoryName(path)) + "/";
        static string helpFile = "file:///C:/ProgramData/Autodesk/ApplicationPlugins/FBXExporter.bundle/Content/Help/FBX%20Exporter%20_%20Revit%20_%20Autodesk%20App%20Store.html";
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
            // Create a custom ribbon panel
            String tabName = "Archilizer";
            String panelName = "Miscellaneous";
            try
            {
                a.CreateRibbonTab(tabName);
            }
            catch (Exception)
            {

            }
            RibbonPanel ribbonPanel = (RibbonPanel)TheInternalDoingPart(a, tabName, panelName);
            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            ContextualHelp ch = new ContextualHelp(ContextualHelpType.Url, @helpFile);

            CreatePushButton(ribbonPanel, string.Format("FBX{0}Exporter", Environment.NewLine), thisAssemblyPath, "FBXExporter.Command",
                string.Format("Exports each element of a 3D view to a separate FBX file.{0}v1.0", Environment.NewLine), "FBX_Exporter.png", ch);

        }
        private static void CreatePushButton(RibbonPanel ribbonPanel, string name, string path, string command, string tooltip, string icon, ContextualHelp ch)
        {
            PushButtonData pbData = new PushButtonData(
                name,
                name,
                path,
                command);

            PushButton pb = ribbonPanel.AddItem(pbData) as PushButton;
            pb.ToolTip = tooltip;
            pb.SetContextualHelp(ch);
            BitmapImage pb2Image = new BitmapImage(new Uri(String.Format("pack://application:,,,/FBXExporter;component/Resources/{0}", icon)));
            pb.LargeImage = pb2Image;
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
