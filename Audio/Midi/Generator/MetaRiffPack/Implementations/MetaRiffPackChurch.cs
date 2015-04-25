﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackChurch : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffOrganChurch());
            metaRiffList.Add(new MetaRiffOrganChurch());
            metaRiffList.Add(new MetaRiffOrganChurch());
            metaRiffList.Add(new MetaRiffBellChurch());
            metaRiffList.Add(new MetaRiffBellChurch());
            return metaRiffList;
        }
    }
}
