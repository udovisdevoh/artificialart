﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    internal class MetaRiffBellChurch : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 14;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 0;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 127;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 67;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 12;
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
            double phase3 = random.NextDouble();
            double phase4 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase4 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction4 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction5 = WaveFunctions.GetRandomWaveFunction(random);

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 2 * random.Next(1, 3), phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 3 * random.Next(1, 3), phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 4, random.NextDouble(), waveFunction3));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 8 * random.Next(1, 3), phase3, waveFunction4));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 16 * random.Next(1, 3), phase4, waveFunction5));

            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 16.0;
            rythmPatternBuilderTimeSplit.MinimumNoteLength *= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 4;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("bell");
            descriptionTagList.Add("aum");
            descriptionTagList.Add("tibetan");
            descriptionTagList.Add("tibet");
            descriptionTagList.Add("monk");
            descriptionTagList.Add("meditation");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(75, 110);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
