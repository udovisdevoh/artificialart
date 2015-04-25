﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackDrumOcta : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffDrumOcta());
            metaRiffList.Add(new MetaRiffDrumOcta());
            metaRiffList.Add(new MetaRiffDrumOcta());
            return metaRiffList;
        }
    }
}