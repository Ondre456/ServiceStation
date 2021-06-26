using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public static class MailSender
    {
        public static void Send(string mail, string adress)
        {
            MailAddress from = new MailAddress("ownertest916@gmail.com", "Tom");
            MailAddress to = new MailAddress(adress);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "ServiceStation";
            m.Body = "<h2>"+ mail + "</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("ownertest916@gmail.com", "qqwwaesrztxy");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
        }
    }
}
