using System;
using System.Collections.Generic;
using System.Text;

namespace Medley.Common.Logging
{
    /// <summary>
    /// A class to point to a static declared instance of IMedleyLogger instance. 
    /// </summary>
   public sealed class MedleyLogger
   {
        // Fields
        private static volatile IMedleyLogger instance;
        private static object syncRoot = new object();

        // Properties
        public static IMedleyLogger Instance
        {
            set
            {
                instance = value;
            }

            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MedleyDefaultLogger();
                        }
                    }
                }
                return instance;
            }
        }
   }
 

}
