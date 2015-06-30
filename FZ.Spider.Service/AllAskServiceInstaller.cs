using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace PS.Service
{
    [RunInstaller(true)]
    public partial class AllAskServiceInstaller : Installer
    {
        public AllAskServiceInstaller()
        {
            InitializeComponent();
        }
 
  }
 }