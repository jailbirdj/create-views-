/*
 * Created by SharpDevelop.
 * User: jay.dunn
 * Date: 6/24/2014
 * Time: 5:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace DeleteSheets
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("8D264797-60DF-424C-BEFF-C42F013FC164")]
	public partial class ThisApplication
	{
		private void Module_Startup(object sender, EventArgs e)
		{

		}

		private void Module_Shutdown(object sender, EventArgs e)
		{

		}

		#region Revit Macros generated code
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(Module_Startup);
			this.Shutdown += new System.EventHandler(Module_Shutdown);
		}
		#endregion
		public void DeleteSheets()
		{
					//setup uidoc and doc for accessing Revit UI (uidoc) and the Model (doc)
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			
			//get all the elements in the model database
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			//filter out all elements except views
			ICollection<Element>collection = collector.OfClass(typeof(View)).ToElements();
			
			//Create a transaction
			using(Transaction t=new Transaction (doc, "DeleteViews"))
			{
				//Start the transaction
				t.Start();
				
				//add a counter to count views & sheets deleted
				int x = 0;
				
				//loop though each view in the model
				foreach(Element e in collection)
				{
					try
					{
						View view = e as View;
						
						//determine what type of view it is
						switch(view.ViewType)
						{
								//if view is a floor plan dont delete
							case ViewType.FloorPlan:
									break;
								//if view is a ceiling plan dont delete
							case ViewType.CeilingPlan:
								break;
								//all other views/sheets can be dseleted by an increment counter by 1
							default:
								doc.Delete(e);
								x+=1;
								break;
						}
					}
					catch(Exception ex)
					{
					}
				}
				//finalize the transaction
				t.Commit();
				//show massage with number deleted
				TaskDialog.Show("DeleteSheets", "Views & Sheets Deleted:" + x.ToString());
			}
		}
	}
}