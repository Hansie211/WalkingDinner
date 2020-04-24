﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner {
    public static class EmailServer {

        public static bool SendEmail( string to, string subject, string body ) {

            /**
             * 
             * DEMO CODE!!!
             * 
             */

            string FullLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string LocationDir  = Path.GetDirectoryName( FullLocation )  + @"\..\..\..\Emails";

            string subdir = Path.GetFullPath( LocationDir );

            Directory.CreateDirectory( subdir );

            string destname = $"{to}-{DateTime.Now:yyyy-MM-dd HH-mm-ss}.html";
            
            string content = $"<html><body><h2>Subject: { subject }</h2>\n\n<pre>{ body }</pre></body></html>";
            File.WriteAllText( Path.Join(subdir, destname), content);

            return true;
        }

    }
}
