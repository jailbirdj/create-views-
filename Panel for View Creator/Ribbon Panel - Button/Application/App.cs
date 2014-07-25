#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
#endregion

namespace Application
{
    class App : IExternalApplication
    {
        //sets the address for the dll file
        public string assemblyloca = @"C:\Users\jay.dunn\AppData\Roaming\Autodesk\Revit\Addins\2014\Application.dll";

        //Built in startup command for Revit
        public Result OnStartup(UIControlledApplication a)
        {           
            //the name of the new tab to be created
            string tabName = "TLC";
            //method to create the tab
            a.CreateRibbonTab(tabName);
            //method to create a new panel
            RibbonPanel panel1 = a.CreateRibbonPanel(tabName, "TLC View Creator");

            //Buttons go in this area
            CreateView(panel1);
            //return succeeded
            return Result.Succeeded;
        }

        public void PullDown(RibbonPanel panel)
        {
            //creates a Push button for the get selection command
            PushButtonData bgetselect = new PushButtonData("getselec", "Get Selection", assemblyloca, "Application.App");
            
           
            
           

        }

        public void CreateView(RibbonPanel panel)
        {
            //This where we setup the command it's using in this case the HelloWorld.CS
            PushButtonData pushButtondataHello = new PushButtonData("CreateView", "Update View Data", assemblyloca, "Application.ProjectSetup");
            // This is how we add the button to our Panel
            PushButton pushButtonHello = panel.AddItem(pushButtondataHello) as PushButton;
            //This is how we add an Icon
            //Make sure you reference WindowsBase and PresentationCore, and import System.Windows.Media.Imaging namespace. 
            pushButtonHello.LargeImage = new BitmapImage(new Uri(@"C:\Users\jay.dunn\AppData\Roaming\Autodesk\Revit\Addins\2014\Images\bulb.png"));
            //Add a tooltip
            pushButtonHello.ToolTip = "This tool Creates Views for all Levels in a Project";
        }

                    public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
