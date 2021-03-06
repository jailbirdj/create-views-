﻿/*
 * Created by SharpDevelop.
 * User: jay.dunn
 * Date: 6/24/2014
 * Time: 5:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSetup
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("2CA18601-4B39-48B0-87CC-5DA085DE8B83")]
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
		public void createMEPViews()
		{
			UIDocument uidoc = this.ActiveUIDocument;
			Document doc = uidoc.Document;
			
			//get all elements in the model
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			//filter out everything buit levels
			ICollection<Element>collection = collector.OfClass(typeof(Level)).ToElements();
			
			//create and start a new transaction
			using(Transaction t = new Transaction(doc, "Create MEP Views"))
			{
				t.Start();
				
				//add a counter to count the number of views created
				int x = 0;
				
				//loop through each level in model
				foreach(Element e in collection)
				{
					try
					{
						Level level = e as Level;
						
						//to use the routines for createFloorPlan or createCeilingPlan, supply the following
						//lvl = level
						//planName = text to use after level name (including a space at Beginning)
						//viewTempName = the exact view template name to be applied to the view
						//uidoc = uidoc (from above)
						// doc = doc (from above)
						
						//Fire Alarm
						createFloorPlan(level, " - FIRE ALARM","E - Fire Alarm", uidoc, doc);
						x += 1;
						
						//Power
						createFloorPlan(level, " - POWER","E - Power", uidoc, doc);
						x += 1;
						
						//Fire Protection
						createFloorPlan(level, " - FIRE PROTECTION","FP - Plans", uidoc, doc);
						x += 1;
						
						//Ductwork
						createFloorPlan(level, " - DUCTWORK","M - Ductwork", uidoc, doc);
						x += 1;
						
						//Gravity
						createFloorPlan(level, " - GRAVITY","P - Gravity", uidoc, doc);
						x += 1;
						
						//Piping
						createFloorPlan(level, " - PIPING","M - Piping", uidoc, doc);
						x += 1;
						
						//Pressure
						createFloorPlan(level, " - PRESSURE","P - Pressure", uidoc, doc);
						x += 1;
						
						//Medgas
						createFloorPlan(level, " - MEDICAL GAS","P - Medical Gas", uidoc, doc);
						x += 1;
						
						//Technology
						createFloorPlan(level, " - TECHNOLOGY","T - Technology", uidoc, doc);
						x += 1;
						
						//Lighting
						createCeilingPlan(level, " - LIGHTING","E - Lighting", uidoc, doc);
						x += 1;
						
						//Fire Protection RCP
						createCeilingPlan(level, " - FIRE PROTECTION","FP - Fire Protection", uidoc, doc);
						x += 1;
						
						//Mechanical RCP
						createCeilingPlan(level, " - HVAC","M - Ceiling", uidoc, doc);
						x += 1;
						
					}
					catch(Exception ex)
					{
					}
				}
				
				//finalize transaction
				t.Commit();
				
				//show dialog of how many views were created
				TaskDialog.Show("CreateMEPViews", "Views Created:" + x.ToString());
				
			}
		}
		//Description: Create a new Floor Plan View and Apply View Template
		public void createFloorPlan(Level lvl, string planName, string viewTempName, UIDocument uidoc, Document doc)
		{
		//Find floor plan View Type
		IEnumerable<ViewFamilyType> viewFamilyTypes=from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
			let type = elem as ViewFamilyType where type.ViewFamily==ViewFamily.FloorPlan select type;
		
		//create a new Floor Plan
		ViewPlan newViewPlan = ViewPlan.Create(doc, viewFamilyTypes.First().Id, lvl.Id);
		//change the name to the level name (in uppercase) + the name provided (planName) when calling the routine
		newViewPlan.Name = lvl.Name.ToUpper() + planName;
		//find the view template provided (viewTempName) when calling the routine
		View viewTemp = (from v in new FilteredElementCollector(doc).OfClass(typeof(View)).Cast<View>()
		                 where v.IsTemplate == true && v.Name == viewTempName select v).First();
		//apply the view template to the view
		newViewPlan.ViewTemplateId = viewTemp.Id;
		
		//return to original macro
		return;
		
		}
		//Description: Create a new Ceiling Plan View and apply View Template
		public void createCeilingPlan(Level lvl, string planName, string viewTempName, UIDocument uidoc, Document doc)
		{
		//Find a Ceiling plan view type
		IEnumerable<ViewFamilyType>viewFamilyTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
			let type = elem as ViewFamilyType where type.ViewFamily== ViewFamily.CeilingPlan select type;
		
		//create a new ceiling plan
		ViewPlan newViewPlan = ViewPlan.Create(doc, viewFamilyTypes.First().Id, lvl.Id);
		//change the name to the level name(in uppercase) + the name provided (planName) when calling routine
		newViewPlan.Name = lvl.Name.ToUpper() + planName;
		//find the view template provided (viewTempName) when calling the routine
		View viewTemp = (from v in new FilteredElementCollector(doc).OfClass(typeof(View)).Cast<View>()
		                 where v.IsTemplate == true && v.Name == viewTempName select v).First();
		//apply the view template to the view
		newViewPlan.ViewTemplateId = viewTemp.Id;
		
		//return to original macro
		return;
		}

	}
}