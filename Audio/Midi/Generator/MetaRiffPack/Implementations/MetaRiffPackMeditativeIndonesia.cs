﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackMeditativeIndonesia : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffXylophoneBali());
            metaRiffList.Add(new MetaRiffXylophoneBali());
            metaRiffList.Add(new MetaRiffPadAum());
            metaRiffList.Add(new MetaRiffPadAum());
            return metaRiffList;
        }
    }
}
