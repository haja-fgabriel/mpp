using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum AthleticsEvent
    {
        ChildSaved
    };

    public class AthleticsEventArgs : EventArgs
    {
        private readonly AthleticsEvent athleticsEvent;
        private readonly Object data;

        public AthleticsEventArgs(AthleticsEvent athleticsEvent, object data)
        {
            this.athleticsEvent = athleticsEvent;
            this.data = data;
        }

        public AthleticsEvent AthleticsEventType
        {
            get { return athleticsEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
