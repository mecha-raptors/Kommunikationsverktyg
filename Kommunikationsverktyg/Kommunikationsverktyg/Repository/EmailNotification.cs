using Kommunikationsverktyg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Kommunikationsverktyg.Repository
{
    public class EmailNotification
    {
        ApplicationDbContext db = new ApplicationDbContext();
     
        public string getEmailAddress(string id)
        {
            var email = from i in db.Users
                        where i.Id == id
                        select i.Email;
       
            return email.ToString();
        }
        
        public void SendRegisterEmail(string recipientEmail) {
            var content = "Tack för att du registrerar ditt konto! En administratör kommer nu godkänna din förfrågan";
            var subject = "Registrering";
            SendEmail(recipientEmail, content, subject);
        }

        public void SendEventInvitationEmail(string recipientEmail)
        {
            var content = "Du har fått en ny mötesinbjudan. Logga in på ditt konto och gå till flikjäveln eller nåt typ.";
            var subject = "Mötesinbjudan";
            SendEmail(recipientEmail, content, subject);
        }

        public void SendEmail (string recipientEmail, string content, string subject)
        {
            try
            {
                var fromAddress = new MailAddress("kommunikationsverktyget@gmail.com", "The Mecha Raptors");
                var toAddress = new MailAddress(recipientEmail, "You");
                var body = content;

                const string fromPassword = "Github2019";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void SendContactEmail(string sender, string recipientEmail, string content, string subject)
        {
            try
            {
                var fromAddress = new MailAddress("kommunikationsverktyget@gmail.com", sender);
                var toAddress = new MailAddress(recipientEmail, "You");
                var body = content;

                const string fromPassword = "Github2019";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}