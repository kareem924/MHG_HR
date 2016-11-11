using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Service.Abstract;
using Excel;

namespace mhg_internalnet2.Controllers
{

    public class HomeController : Controller
    {
        private readonly IAttendancesService _attendanceServices;

        public HomeController(IAttendancesService attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    file.SaveAs(physicalPath);
                    FileStream stream = System.IO.File.Open(physicalPath, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();
                    DataTable dt = result.Tables[0];
                    var test = dt.Rows[1][0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime date;
                       var convertLastChar= dt.Rows[i][1].ToString().Substring(dt.Rows[i][1].ToString().Length - 1);
                        if (convertLastChar == "ص")
                        {
                            convertLastChar = dt.Rows[i][1].ToString().Replace(dt.Rows[i][1].ToString().Substring(dt.Rows[i][1].ToString().Length - 1), "AM") ;
                            date= DateTime.ParseExact(convertLastChar, "d/M/yyyy h:m:s tt", CultureInfo.InvariantCulture);
                        }
                        else if (convertLastChar == "م")
                        {
                            convertLastChar = dt.Rows[i][1].ToString().Replace(dt.Rows[i][1].ToString().Substring(dt.Rows[i][1].ToString().Length - 1), "PM");
                            date = DateTime.ParseExact(convertLastChar, "d/M/yyyy h:m:s tt", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            convertLastChar = dt.Rows[i][1].ToString().Remove(dt.Rows[i][1].ToString().Length - 1);
                           
                            date = Convert.ToDateTime(convertLastChar);
                        }

                      

                        _attendanceServices.InsertAttendance(new Attendance { FingerPrint = int.Parse(dt.Rows[i][0].ToString()), CheckedInAt = date });
                    }

                    System.IO.File.Delete(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {


                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}