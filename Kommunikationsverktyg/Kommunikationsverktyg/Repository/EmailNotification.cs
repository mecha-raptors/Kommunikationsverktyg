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

        public void SendEmailRegister(string sender, string recipient) {
            try
            {
                var viewModel = new RegisterViewModel();
                var fromAddress = new MailAddress(sender, "Admin");
                var toAddress = new MailAddress(recipient, viewModel.Firstname + ' ' + viewModel.Lastname);
                const string fromPassword = "Github2019";
                const string subject = "Angående ditt konto";
                const string body = "Vi har mottagit din förfrågan om konto på sidan. En administratör ska nu godkänna dig.";

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