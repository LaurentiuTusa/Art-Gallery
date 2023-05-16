using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Art_Gallery.DAL.Email_Logs;

namespace Art_Gallery.DAL.ObserverDP
{
    public class ConcreteObserver : Observer
    {
        private string email;
        private string observerState;
        private ConcreteSubject subject;
        // Constructor
        public ConcreteObserver(
            ConcreteSubject subject, string email)
        {
            this.subject = subject;
            this.email = email;
        }
        public override void Update()
        {
            observerState = subject.SubjectState; //"New product added"
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(email, observerState, ConstantStrings.updateMsg);
        }

        // Gets or sets subject
        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
