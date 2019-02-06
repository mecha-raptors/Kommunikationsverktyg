using Kommunikationsverktyg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Kommunikationsverktyg.Repository
{
    public class SmsNotification
    {

        ApplicationDbContext db = new ApplicationDbContext();

        
        public void SendSmsNotification(string number, string bodymessage)
        {
           
            string accountSid = "ACf57b231a7f12f7efbf4b8402a4aacbd9";
             string authToken = "08105bc75e2353e9b25cde90c98fc75f";

            TwilioClient.Init(accountSid, authToken);
            
            var message = MessageResource.Create(
                body: bodymessage,
                from: new Twilio.Types.PhoneNumber("+46790644425"),
                to: new Twilio.Types.PhoneNumber(number)
            );

            Console.WriteLine(message.Sid);

        }
        public string getPhoneNumber(string id)
        {
            var number = from i in db.Users
                         where i.Id == id
                         select i.PhoneNumber;

            return number.ToString();
        }
    }
}
