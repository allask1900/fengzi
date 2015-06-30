using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace FZ.Spider.Spider.Service
{
    [RunInstaller(true)]
    public partial class SpiderServiceInstaller : Installer
    {
        public SpiderServiceInstaller()
        {
            InitializeComponent();
        }
 
  }
 }