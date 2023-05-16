using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Art_Gallery.DAL.Email_Logs;

namespace Art_Gallery.DAL.ObserverDP
{
    public class ConcreteObserverLog : Observer
    {
        private string title;
        private ConcreteSubject subject;
        // Constructor
        public ConcreteObserverLog(ConcreteSubject subject, string title)
        {
            this.subject = subject;
            this.title = title;
        }
        public override void Update()
        {
            LogWriter logWriter = new LogWriter(ConstantStrings.logFilePath);
            logWriter.AppendLog(subject.SubjectState, title);
        }

        // Gets or sets subject
        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
