﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackMeditativeWest : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffPianoMeditation());
            metaRiffList.Add(new MetaRiffPianoMeditation());
            metaRiffList.Add(new MetaRiffPadAum());
            metaRiffList.Add(new MetaRiffPadAum());
            return metaRiffList;
        }
    }
}
