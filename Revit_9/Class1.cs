using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Architecture;

namespace Revit_8
{
    [Transaction(TransactionMode.Manual)]
    public class AutoNumberRoom : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
                    
            
            Transaction trans = new Transaction(doc, "Нумерация комнат");
            trans.Start();

            int i = 0;

            var collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);


            foreach (Element el in collector)
            {
                Room room = el as Room;
                if(room!=null)
                {
                    i++;
                    room.Number = room.Level.Name + '_' + i.ToString();
                }
            }
            
            trans.Commit();

            return Result.Succeeded;
        }


    }
}

