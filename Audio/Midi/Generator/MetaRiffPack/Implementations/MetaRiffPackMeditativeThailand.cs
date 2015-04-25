﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackMeditativeThailand : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffSitarQuad());
            metaRiffList.Add(new MetaRiffBellTibet());
            metaRiffList.Add(new MetaRiffShamisenChinese());
            metaRiffList.Add(new MetaRiffPadAum());
            return metaRiffList;
        }
    }
}
