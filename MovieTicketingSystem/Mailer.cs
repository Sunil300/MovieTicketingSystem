using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using MovieTicketingSystem.Models;

namespace MovieTicketingSystem
{
    public class Mailer
    {
        public void SendMail(Ticket ticket)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("email address", "Movie Ticketing System");

                message.To.Add(new MailAddress(ticket.Email, "To Name"));
                //TODO: qrcode generator to make qrcodes
                string strFilePath = @"c:\qrcode.png";

                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(strFilePath);
                attachment1.Name = System.IO.Path.GetFileName(strFilePath);
                attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                attachment1.ContentDisposition.Inline = true;
                attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                string cid = attachment1.ContentId;
                message.Attachments.Add(attachment1);
                message.Subject = "Booking Room " + ticket.Playing.Room.RoomNo + ", Seat " + ticket.SeatNo  + ", playing " + ticket.Playing.PlayingDate.ToShortDateString() + " " + ticket.Playing.TimeSlot.TimeFrom + ", Electronic Receipt";
                message.Body = "Title : " + ticket.Playing.Movie.Title + Environment.NewLine +
                    "Playing Time : " + ticket.Playing.PlayingDate.ToShortDateString() + " " + ticket.Playing.TimeSlot.TimeFrom + Environment.NewLine +
                    "Booking Room : " + ticket.Playing.Room.RoomNo + Environment.NewLine +
                    "Seat : " + ticket.SeatNo + Environment.NewLine +
                "< img src = cid:"+ cid + "/>";
                using (var client = new SmtpClient("smtp.xxx.com"))
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Port = 25;
                    client.Credentials = new NetworkCredential("email address", "password");
                    client.EnableSsl = true;
                    client.Send(message);
                    //Console.WriteLine(message.Body);
                }
            }
        }

    }
}
