using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentRackConfiguration
{
    public class OverrideTimer : System.Timers.Timer
    {
     
          private int feerackId;
          private string feerackno;

          public string Feerackno
          {
              get { return feerackno; }
              set { feerackno = value; }
          }
          
          public int FeerackId
          {
              set { feerackId = value; }
              get { return feerackId; }
          }
         
          public OverrideTimer(): base()
          {

          }

     
    }
}
