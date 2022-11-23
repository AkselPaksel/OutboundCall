namespace OutboundCall
{
    internal class ResponseSchema
    {

        public class Rootobject
        {
            public string topicName { get; set; }
            public string version { get; set; }
            public Eventbody eventBody { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Eventbody
        {
            public string id { get; set; }
            public Participant[] participants { get; set; }
            public string recordingState { get; set; }
            public string address { get; set; }
        }

        public class Participant
        {
            public string id { get; set; }
            public DateTime connectedTime { get; set; }
            public string userId { get; set; }
            public string externalContactId { get; set; }
            public Wrapup wrapup { get; set; }
            public string name { get; set; }
            public string queueId { get; set; }
            public string purpose { get; set; }
            public string address { get; set; }
            public bool wrapupRequired { get; set; }
            public bool wrapupExpected { get; set; }
            public Attributes attributes { get; set; }
            public Call[] calls { get; set; }
            public Additionalproperties additionalProperties { get; set; }
            public DateTime endTime { get; set; }
            public Workflow workflow { get; set; }
            public Conversationroutingdata conversationRoutingData { get; set; }
        }
        public class Wrapup
        {
            public string code { get; set; }
            public string notes { get; set; }
            public int durationSeconds { get; set; }
            public DateTime endTime { get; set; }
            public Additionalproperties2 additionalProperties { get; set; }
        }

        public class Attributes
        {
            public string CallSurveyCulture { get; set; }
        }

        public class Additionalproperties
        {
        }

        public class Workflow
        {
            public string workflowId { get; set; }
            public Additionalproperties1 additionalProperties { get; set; }
        }

        public class Additionalproperties1
        {
        }

        public class Conversationroutingdata
        {
            public Queue queue { get; set; }
            public Language language { get; set; }
            public int priority { get; set; }
        }

        public class Queue
        {
            public string id { get; set; }
        }

        public class Language
        {
        }

        public class Call
        {
            public string id { get; set; }
            public string state { get; set; }
            public string initialState { get; set; }
            public bool recording { get; set; }
            public string recordingState { get; set; }
            public bool muted { get; set; }
            public bool confined { get; set; }
            public bool held { get; set; }
            public string direction { get; set; }
            public Self self { get; set; }
            public Other other { get; set; }
            public string provider { get; set; }
            public DateTime connectedTime { get; set; }
            public bool afterCallWorkRequired { get; set; }
            public Additionalproperties4 additionalProperties { get; set; }
            public string disconnectType { get; set; }
            public string peerId { get; set; }
            public DateTime disconnectedTime { get; set; }
        }

        public class Self
        {
            public string name { get; set; }
            public string nameRaw { get; set; }
            public string addressNormalized { get; set; }
            public string addressRaw { get; set; }
            public string addressDisplayable { get; set; }
            public Additionalproperties2 additionalProperties { get; set; }
        }

        public class Additionalproperties2
        {
        }

        public class Other
        {
            public string name { get; set; }
            public string nameRaw { get; set; }
            public string addressNormalized { get; set; }
            public string addressRaw { get; set; }
            public string addressDisplayable { get; set; }
            public Additionalproperties3 additionalProperties { get; set; }
        }

        public class Additionalproperties3
        {
        }

        public class Additionalproperties4
        {
        }

        public class Metadata
        {
            public string CorrelationId { get; set; }
        }


    }
}
