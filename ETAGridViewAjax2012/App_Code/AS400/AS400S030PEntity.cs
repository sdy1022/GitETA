using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.AS400
{
    /// <summary>
    /// Summary description for AS400S030PEntity
    /// </summary>
    public class AS400S030PEntity
    {
        public string ZZVDC { get; set; }
        public string ZZVDN { get; set; }
        ///string query = "SELECT ZZVDC,ZZVDN,ZZPMM,ZZTOD,ZZTOT,ZZDLS,ZZCTS,ZZSVS,ZZQTS,ZZTRC,ZZTRJ,ZZPPM  fROM  LIBDF7.S030P where ZZVDC='" + vendervalue + "'";
        public string ZZPMM { get; set; }
        public string ZZTOD { get; set; }
        public string ZZTOT { get; set; }
        public string ZZDLS { get; set; }
        public string ZZCTS { get; set; }
        public string ZZSVS { get; set; }
        public string ZZQTS { get; set; }
        public string ZZTRC { get; set; }
        public string ZZTRJ { get; set; }
        public string ZZPPM { get; set; }
        // New Added On 09/07/2010
        public string ZZYTD { get; set; }
        public string ZZQCMT { get; set; }
        public string ZZVAVE { get; set; }
        public string ZZCCMT { get; set; }
        public string ZZDCMT { get; set; }
        public string ZZSCMT { get; set; }
        public string ZZPCT { get; set; }
    }

    public class AS400S030P99Entity
    {
        public string ZZYTD { get; set; }
        public string ZZPMM { get; set; }

    }

}
