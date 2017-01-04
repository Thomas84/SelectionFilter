#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace LazyBIM
{
    public class Selectionfitler : ISelectionFilter
    {

        public bool AllowElement(Element element)
        {
            if (FormSelectionCategories.cat.Contains(element.GetType().Name) || FormSelectionCategories.cat.Contains(element.Category.Name))
            {
                return true;
            }
            return false;
        }
        public bool AllowReference(Reference refer, XYZ point)
        {
            return false;
        }

    }

    [Transaction(TransactionMode.Manual)]
    public class commandSelectionFilter : IExternalCommand
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

            FormSelectionCategories.cancel_form = false;

            FormSelectionCategories form = new FormSelectionCategories();
            form.ShowDialog();

            if (FormSelectionCategories.cancel_form == true)
            {
                return Result.Cancelled;
            }

            IList<Reference> refs = new List<Reference>();
            IList<ElementId> ids = new List<ElementId>();
            Selectionfitler selfilter = new Selectionfitler();
            // prompt user to add to selection or remove from it
            Selection sel = uidoc.Selection;

            try
            {
                ICollection<ElementId> preSelectedElemIds = sel.GetElementIds();
                foreach (ElementId id in preSelectedElemIds)
                {
                    Reference elemRef = new Reference(doc.GetElement(id));
                    refs.Add(elemRef);
                }

                refs = sel.PickObjects(ObjectType.Element, selfilter, "Please pick element.", refs);
                foreach (Reference r in refs)
                {
                    ids.Add(r.ElementId);
                }
                sel.SetElementIds(ids);
                return Result.Succeeded;
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception)
            {
                return Result.Failed;
            }
        }

    }
}


