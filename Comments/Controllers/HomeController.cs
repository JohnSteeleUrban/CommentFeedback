using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Comments.Models;

namespace Comments.Controllers
{
    public class HomeController : Controller
    {
        private CommentsDataEntities db = new CommentsDataEntities();
        private SiteOwnerDataEntities _siteOwnerDb = new SiteOwnerDataEntities();
      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CommentId,Name,ContactEmail,PleaseContact,OriginatingSite,Comment1,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                System.Threading.Thread.Sleep(5000);
                db.Comments.Add(comment);
                db.SaveChanges();

                var originatingSite = comment.OriginatingSite;
                foreach (var siteOwnerRow in _siteOwnerDb.SiteOwnerTables)
                {
                    if (originatingSite.Contains(siteOwnerRow.Url))
                    {
                        var dataTable = new DataTable();
                        dataTable.Columns.Add("Field");
                        dataTable.Columns.Add("User Input");
                        dataTable.Rows.Add("Name:    ", comment.Name);
                        dataTable.Rows.Add("Please Contact:    ", comment.PleaseContact ? "Yes" : "No");
                        var subject = "Comment For " + comment.OriginatingSite;
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (comment.ContactEmail != null)
                        {
                            if (comment.PleaseContact && re.IsMatch(comment.ContactEmail))
                            {
                                dataTable.Rows.Add("Email:    ", comment.ContactEmail);
                                 SendMail.SendEmail.Send(subject, "Thank you for your submission." + "<br/><br/>" + "  Someone from " + comment.OriginatingSite + " will be in touch.", comment.ContactEmail);
                            }
                        }
                        dataTable.Rows.Add("Originating Site:    ", comment.OriginatingSite);
                        dataTable.Rows.Add("Comment:    ", comment.Comment1);
                        var tableHtml = SendMail.SendEmail.ConvertDataTableToHTML(dataTable);
                       
                        var message = "Hello " + siteOwnerRow.OwnerEmail + "<br/>" + "The following comment was submitted:" + "<br/><br/>" + tableHtml;
                        SendMail.SendEmail.Send(subject, message, siteOwnerRow.OwnerEmail);
                    }
                }
                
                Response.Redirect(comment.OriginatingSite);
                return RedirectToAction("Index");
            }
            return View(comment);
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
    }
}