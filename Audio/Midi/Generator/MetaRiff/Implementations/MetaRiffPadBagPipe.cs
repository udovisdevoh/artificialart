﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    internal class MetaRiffPadBagPipe : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 109;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 70;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 70;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return random.Next(55, 60);
        }

        public override int BuildPreferedRadius(Random random)
        {
            return random.Next(8, 12);
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.MajorPentatonic);
            return scaleChooser;
        }

        public override IWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();


            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble(), 0.125 * random.Next(1, 3), phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble(), 0.0625 * random.Next(1, 3), phase2, waveFunction2));
            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MinimumNoteLength *= 16.0;
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 32.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 8;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("pad");
            descriptionTagList.Add("bagpipe");
            descriptionTagList.Add("scottish");
            descriptionTagList.Add("meditation");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(73, 146);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
