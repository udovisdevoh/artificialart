using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialArt.Audio.Midi.Generator
{
    class MetaRiffPackGuitarFolk : MetaRiffPack
    {
        protected override IEnumerable<MetaRiff> BuildMetaRiffList()
        {
            List<MetaRiff> metaRiffList = new List<MetaRiff>();
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            metaRiffList.Add(new MetaRiffGuitarFolkBinary());
            return metaRiffList;
        }
    }
}
