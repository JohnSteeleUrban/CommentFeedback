using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Comments.SendMail
{
    public static class SendEmail
    {
        private static string _sender = "CommentApp@pagnet.org";
        private static string _host = "pagnet.org";
        private static int _port = 27;

        public static void Send(string subject, string message, string reciever)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(_sender)
                };
                mailMessage.To.Add(reciever);
                mailMessage.Bcc.Add("jUrban@pagregion.com");
                mailMessage.Subject = subject;
                mailMessage.Body = message;

                using (SmtpClient client = new SmtpClient(_host, _port))
                {
                    client.Send(mailMessage);
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }

        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table style=\"\"max-width: 800px\"";
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<th>" + dt.Columns[i].ColumnName + "</th>";
            html += "</tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
