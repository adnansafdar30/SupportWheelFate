using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAU.Models
{
    public class HelpingClass
    {
        dbBAUEntities context = new dbBAUEntities();
        List<Employee> employeearray = new List<Employee>();

        int random1 = 0;
        int random2 = 0;
        //***** All Rotation Controller Methords  *****//
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
            rtn.End_Date = startDate.AddDays(rotationperiod - 1);
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


        //***** ALL Shift Controller Methords ******//
        internal int getShifts()
        {
            int result = 0;
            DateTime date = getshiftDate();//get rotation start date
            if (date != null)
            {
                do
                {
                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        result = updatelockStatus(date);//check last day employee who worked
                        getEmployeeList();//get employeearray who are avaialbe to work
                        getrandomValues(employeearray);//assinging values to random 1 and random 2
                        if (random1 != 0 && random2 != 0)
                        {
                            result = savetoShift(date, random1, random2);
                            if (result == 1)
                            {
                                addshiftinEmployee(random1, random2, date);

                            }
                        }
                        date = date.AddDays(1);

                    }
                    else
                    {
                        date = date.AddDays(1);
                    }
                  
                } while (employeearray.Count>2);
            }
            return result;
        }
        //set in employee table last date and last randomly chosen employee to lock status
        private void addshiftinEmployee(int random1, int random2, DateTime date)
        {
            if (random1 == 0) { }
            else
            {
                var emp = context.Employees.Where(x => x.Employee_ID == random1).ToList();
                emp.ForEach(a => a.Total_No_Shift = a.Total_No_Shift + 1);
                emp.ForEach(a => a.Last_Shift = date.Date);
                context.SaveChanges();
            }
            if (random2 == 0) { }
            else
            {

                var emp1 = context.Employees.Where(x => x.Employee_ID == random2).ToList();
                emp1.ForEach(a => a.Total_No_Shift = a.Total_No_Shift + 1);
                emp1.ForEach(a => a.Last_Shift = date.Date);
                context.SaveChanges();
            }
        }

        //save data to the shift
        private int savetoShift(DateTime date, int random1, int random2)
        {
            Shift sft = new Shift();
            sft.Shift_Date = date.Date;
            sft.Morning_Shift = random1;
            sft.Evening_Shift = random2;
            context.Shifts.Add(sft);
            int result = context.SaveChanges();
            return result;
        }//finish methord

        //generate random values
        private void getrandomValues(List<Employee> employeearray)
        {
            if (employeearray.Count != 0)//if 
            {
                int count = employeearray.Count();
                int[] randlist = new int[count];
                Random random = new Random();
                int i = 0;
                foreach (var emp in employeearray)
                {
                    randlist[i] = emp.Employee_ID;
                    i++;
                }
                random1 = randlist[random.Next(0, randlist.Length)];
                do
                {
                    random2 = randlist[random.Next(0, randlist.Length)];
                }
                while
                   (random1 == random2);
            }
        }//random methord completed

        //get employee arraye who are availbale to work for the day 
        private List<Employee> getEmployeeList()
        {
            employeearray = null;
            List<Employee> empllist = new List<Employee>();
            empllist = context.Employees.Where(x => x.Total_No_Shift < 2)
                   .ToList()
                   .Where(x => x.Lock == false).ToList();
            employeearray = empllist;
            return empllist;
        }//list methord completed

        //update employees lock status who has worked yesterday 
        private int updatelockStatus(DateTime date)
        {
            foreach (var emp in context.Employees)
            {
                if (emp.Last_Shift.AddDays(1) == date)
                {
                    //
                    emp.Lock = true;
                }
                else
                {
                    emp.Lock = false;
                }
            }
            int result = context.SaveChanges();
            return result;
        }

        //start rortation date get values
        private DateTime getshiftDate()
        {
            var showPiece = context.Rotations
                      .OrderByDescending(p => p.Rotation_ID)
                      .FirstOrDefault();
            DateTime date = (DateTime)showPiece.Start_Date;
            return date;
        }//date methord finish 
    }
}