#region Namespaces
using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

#endregion

namespace LazyBIM
{
    class appSelectionFilter : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel m_Ribbon = a.CreateRibbonPanel("LazyBIM");
            string thisAssmblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData pbd1 = new PushButtonData(
                "cmdSelectionFilter",
                "Selection" + '\n'+ "Filter",
                thisAssmblyPath,
                "LazyBIM.commandSelectionFilter"
                );
            PushButton pb1 = (PushButton)m_Ribbon.AddItem(pbd1);
            pbd1.ToolTip = "Only select elements in specified categories";
            BitmapImage pb1_img = new BitmapImage(new Uri("pack://application:,,,/SelectionFilter;component/sf.png"));
            pb1.LargeImage = pb1_img;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}