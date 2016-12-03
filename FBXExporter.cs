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
using System.Text.RegularExpressions;
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
        public static string Export(Autodesk.Revit.DB.Document document, System.Windows.Forms.ProgressBar progressBar, System.Windows.Forms.Label label, System.Windows.Forms.Label percent)
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
                
                ICollection<ElementId> removedElements = new List<ElementId>();

                //Hide everything first
                using (Transaction t = new Transaction(doc,"Hide all"))
                {
                    t.Start();
                    foreach(ElementId e in allAlements)
                    {
                        if (doc.GetElement(e).Category.Name.Contains("Curtain") || doc.GetElement(e).Category.Name.Contains("Lines")
                            || doc.GetElement(e).Category.Name.Contains("Top Rails") || doc.GetElement(e).Category.Name.Contains("Shaft Openings"))
                        {
                            removedElements.Add(e);
                            continue;
                        }
                        ICollection<ElementId> element = new List<ElementId>() { e };
                        try
                        {
                            activeView.HideElements(element);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                            removedElements.Add(e);
                        }
                    }
                    
                    t.Commit();
                }

                foreach(var obj in removedElements)
                {
                    allAlements.Remove(obj);
                }


                progressBar.Minimum = 0;
                progressBar.Maximum = allAlements.Count;

                string regexSearch = new string(System.IO.Path.GetInvalidFileNameChars());

                //Unhide each element one by one and export a view with it
                foreach (ElementId e in allAlements)
                {
                    ICollection<ElementId> element = new List<ElementId>() { e };
                    string category = doc.GetElement(e).Category.Name;
                    string famtype = doc.GetElement(e).get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM).AsValueString();
                    string name = String.Format("{0}-{1}-id{2}", category, famtype, e.ToString());
                    Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                    name = r.Replace(name, "");

                    using (Transaction tx = new Transaction(doc))
                    {
                        tx.Start(String.Format("Export element {0}", e.ToString()));
                        activeView.UnhideElements(element);
                        doc.Regenerate();
                        viewSet.Insert(activeView);
                        try
                        {
                            doc.Export(folder, name, viewSet, options);
                        }
                        catch(Exception ex)
                        {
                            TaskDialog.Show("Error", String.Format("There has been a problem executing this script.{0}{1}", Environment.NewLine, ex.Message));
                        }

                        activeView.HideElements(element);
                        tx.Commit();
                    }

                    viewSet.Clear();
                    progressBar.Value++;
                    label.Text = String.Format("Exported {0} of {1}", progressBar.Value.ToString(), progressBar.Maximum.ToString());
                    label.Refresh();
                    percent.Text = String.Format("{0}{1}", (Convert.ToInt16 ((progressBar.Value / (progressBar.Maximum * 1.0)) * 100)).ToString(), "%");
                    percent.Refresh();
                    c++;
                }
                //Finally unhide everything back
                using (Transaction t = new Transaction(doc, "Hide all"))
                {
                    t.Start();
                    activeView.UnhideElements(allAlements);
                    t.Commit();
                }

                string runMessage = String.Format("Successfully exported {0} elements", c);

                TaskDialog.Show("Result", runMessage);

                return runMessage;
            }
        }
    }
}
