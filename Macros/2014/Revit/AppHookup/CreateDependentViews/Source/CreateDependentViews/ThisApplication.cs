/*
 * Created by SharpDevelop.
 * User: jay.dunn
 * Date: 6/25/2014
 * Time: 12:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace CreateDependentViews
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("7DF2E681-F2B9-4C6A-8349-FDDF537DD604")]
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
		//Description: Create 4 Dependent Views for currently active View
		public void DuplicateDependent()
		{
		//setup uidoc and doc for accessing the Revit UI (uidoc and the Model (doc)
		UIDocument uidoc = this.ActiveUIDocument;
		Document doc = uidoc.Document;
		
		//create a transaction
		using(Transaction t = new Transaction(doc, "Duplicate View 5x"))
		{
			//start the transaction
			t.Start();
			
			//create a counter for incrementing alphabet
			int i = 0;
			
			//loop through until you reach 4(change 4 to increase/decreas the before running macro)
			while(i<4)
			{
				//duplicate the currently active view
				ElementId dupViewId=uidoc.ActiveView.Duplicate(ViewDuplicateOption.AsDependent);
				//get the new dependent view
				View dupView = doc.GetElement(dupViewId) as View;
				//use char command to get the Letter A
				char c = (char)(i+65);
				//rename the dependent view to include the original name and the new Area
				dupView.Name = uidoc.ActiveView.Name + " - AREA " + c.ToString();
				
				//increment the char each loop (A, B, C, etc)
				i++;
			}
			//finalize the transaction
			t.Commit();
			
			}
		}
		}
	}
