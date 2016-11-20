#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
#endregion


namespace FBXExporter
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;
            
            using(ProgressBarForm pform = new ProgressBarForm(doc))
            {
                var result = pform.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    return Result.Succeeded;
                }
                else
                {
                    return Result.Failed;
                }
            }
        }
    }
    internal static class ExportFBX
    {
        /// <summary>
        /// Exports all elements in 3D View to separate .fbx files.
        /// </summary>
        public static string Export(Autodesk.Revit.DB.Document document, System.Windows.Forms.ProgressBar progressBar, System.Windows.Forms.Label label)
        {
            Autodesk.Revit.DB.Document doc = document;
            Autodesk.Revit.DB.View activeView = doc.ActiveView;

            FBXExportOptions options = new FBXExportOptions();

            if (activeView.ViewType != ViewType.ThreeD)
            {
                TaskDialog.Show("Warning", "Can only be run in 3D View.");
                return "Failed";
            }

            ElementOwnerViewFilter elementOwnerViewFilter =
                new ElementOwnerViewFilter(activeView.Id);

            FilteredElementCollector col
                = new FilteredElementCollector(doc, activeView.Id)
                .WhereElementIsNotElementType();

            ICollection<ElementId> allAlements = col.ToElementIds();
                    
            using(System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if(folderDialog.ShowDialog() != DialogResult.OK)
                {
                    return "No folder chosen.";
                }

                string folder = folderDialog.SelectedPath;

                // Filtered element collector is iterable
                ViewSet viewSet = new ViewSet();

                int c = 0;

                progressBar.Minimum = 0;
                progressBar.Maximum = allAlements.Count;

                foreach (Element e in col)
                {
                    using (Transaction tx = new Transaction(doc))
                    {
                        tx.Start(String.Format("Export element {0}", e.Id.ToString()));
                        activeView.IsolateElementTemporary(e.Id);
                        activeView.ConvertTemporaryHideIsolateToPermanent();
                        viewSet.Insert(activeView);

                        doc.Export(folder, e.Id.ToString(), viewSet, options);

                        activeView.UnhideElements(allAlements);
                        doc.Regenerate();
                        tx.Commit();
                    }

                    viewSet.Clear();
                    progressBar.Value++;
                    label.Text = String.Format("Exported {0} of {1}", progressBar.Value.ToString(), progressBar.Maximum.ToString());
                    label.Refresh();
                    c++;
                }

                string runMessage = String.Format("Successfully exported {0} elements", c);

                TaskDialog.Show("Result", runMessage);

                return runMessage;
            }
        }
    }
}
