﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Server.Regions
{
    public class ZoneCreation : ZoneInterne
    {
        public ZoneCreation(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
        }
    }
}