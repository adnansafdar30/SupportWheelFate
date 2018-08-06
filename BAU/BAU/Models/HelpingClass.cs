using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAU.Models
{
    public class HelpingClass
    {
        dbBAUEntities context = new dbBAUEntities();
        internal int runRotationMethords(Rotation values, int rotationperiod)
        {
            int result = deleterotaiondata();//deleting any rotation exit before
            result = updateEmployeeRecords();//deleteing any employee shifts already exit
            result = removeshiftsrecords();//removing any shift record already exit
            result = addRotationTimeandDays(values, rotationperiod);//adding 14 days of 2 week rotatin in database

            //if (result == 1)
            //{
            //    result = updateEmployeeRecords();//deleteing any employee shifts already exit
            //    if (result == 1)
            //    {
            //        result = removeshiftsrecords();//removing any shift record already exit
            //        if (result == 1)
            //        {
            //            result = addRotationTimeandDays(values, rotationperiod);//adding 14 days of 2 week rotatin in database
            //            return result;
            //        }
            //    }
            //}

            return result;
        }
        //adding rotation of two weeks with 14 days interval
        private int addRotationTimeandDays(Rotation values, int rotationperiod)
        {
            Rotation rtn = new Rotation();
            DateTime startDate = ((DateTime)values.Start_Date).Date;//getting only date without time
            rtn.Start_Date = startDate;
            rtn.End_Date = startDate.AddDays(rotationperiod-1);
            context.Rotations.Add(rtn);
            int result = context.SaveChanges();
            return result;
        }//methord finish

        //removing any shift data if exit
        private int removeshiftsrecords()
        {
            var rows = from o in context.Shifts
                       select o;
            foreach (var row in rows)
            {
                context.Shifts.Remove(row);
            }
            int result = context.SaveChanges();
            return result;
        }//methord finish
        //updating employee record to set 0
        private int updateEmployeeRecords()
        {
            foreach (Employee emp in context.Employees)
            {
                DateTime datevalue = Convert.ToDateTime("01/02/2000");
                emp.Total_No_Shift = 0;
                emp.Last_Shift = datevalue;
                emp.Lock = false;
            }
            int result = context.SaveChanges();
            return result;
        }//methord finish
        //deleting any rotaion data in before
        private int deleterotaiondata()
        {

            var rows = from o in context.Rotations
                       select o;
            foreach (var row in rows)
            {
                context.Rotations.Remove(row);
            }
            int result = context.SaveChanges();
            return result;
        }//reotation data metord finish 
    }
}