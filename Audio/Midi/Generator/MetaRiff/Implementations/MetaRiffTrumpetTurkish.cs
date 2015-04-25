﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    internal class MetaRiffTrumpetTurkish : MetaRiff
    {
        public override IWave BuildPitchOrVelocityWave(Random random)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 6, phase1, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 4 * random.Next(1, 3), phase2, WaveFunctions.GetRandomWaveFunction(random)));
            wavePack.Add(new Wave(random.NextDouble() * 0.5, 16, phase3, WaveFunctions.GetRandomWaveFunction(random)));

            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.25;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.ArabicPentatonic);
            return scaleChooser;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 75;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 12;
        }

        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 59;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("turkish");
            descriptionTagList.Add("trumpet");
            descriptionTagList.Add("lead");
            return descriptionTagList;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 35;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 110;
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
