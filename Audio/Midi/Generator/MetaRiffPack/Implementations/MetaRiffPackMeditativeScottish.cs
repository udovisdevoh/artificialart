using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackMeditativeScottish : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffBagPipeScottish());
            metaRiffList.Add(new MetaRiffBagPipeScottish());
            metaRiffList.Add(new MetaRiffPadBagPipe());
            metaRiffList.Add(new MetaRiffPadBagPipe());
            return metaRiffList;
        }
    }
}
