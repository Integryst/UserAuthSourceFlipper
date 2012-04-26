// Copyright (c) 2012 Integryst, LLC, http://www.integryst.com/
// See LICENSE.txt for licensing information

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UserAuthSourceFlipper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
