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


namespace Export
{
    public class ExportFBX
    {
        /// <summary>
        /// Exports all elements in 3D View to separate .fbx files.
        /// </summary>
        public string Export(Autodesk.Revit.DB.Document document)
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

            IList<Element> viewElements = col.ToElements();

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
                    c++;
                }

                string runMessage = String.Format("Run {0} times", c);

                TaskDialog.Show("Result", runMessage);

                return String.Format("Successfully exported {0} elements", c);
            }
        }
    }
}
