using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackArab : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffGuitarArab());
            metaRiffList.Add(new MetaRiffGuitarArab());
            metaRiffList.Add(new MetaRiffViolinArab());
            metaRiffList.Add(new MetaRiffViolinArab());
            //metaRiffList.Add(new MetaRiffTrumpetTurkish());
            return metaRiffList;
        }
    }
}
