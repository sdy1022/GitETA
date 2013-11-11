using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DAL.AS400
{
    [DataContract ]
    [Serializable]
    public class AS400A004PEntity
    {
        // ADODN, ADATT, ADMDN, ADMST, ADEON, ADAON
        [DataMember]
        public string ADODN { get; set; } // order number
        [DataMember]
        public string ADATT { get; set; } // Attachment Hostingt
        [DataMember]
        public string ADMDN { get; set; } // Model
        [DataMember]
        public string ADMST { get; set; } // Mast Type
        [DataMember]
        public string ADEON { get; set; } // Estimated Inline Date
        [DataMember]
        public string ADAON { get; set; } // Actual Inline Date
        [DataMember]
        public string TFC { get; set; } // LIBDF7.A021P.AWPDC
        [DataMember]
        public string WholeSalesCode { get; set; } // sales code
        [DataMember]
        public string WholeOptions { get; set; } // LIBDF7.A021P.AWPDC
        
    }
}
