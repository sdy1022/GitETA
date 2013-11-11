using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for EnumerationTypes
/// </summary>

   

 public enum PartNoValidationResult
         {
             ExceptionError=-4,
             InvalidValue = -3, // with valid format
             BeyondRecursiveLevel=-2,
             InvalidFormat=-1,            
             Warning=0,
             Valid=1
             

         }



