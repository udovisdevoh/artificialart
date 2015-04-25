﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    internal class MetaRiffSynthLeadRom : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 81;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 17;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 80;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 68;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 28;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.MinorPentatonic);
            return scaleChooser;
        }

        public override IWave BuildPitchOrVelocityWave(Random random)
        {
            WavePack wavePack = new WavePack();

            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random);

            wavePack.Add(new Wave(random.NextDouble() * 0.333, 2, phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble() * 0.333, 3 * random.Next(1, 3), phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble() * 0.6, 8 * random.Next(1, 3), phase3, waveFunction3));

            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.125;
            //rythmPatternBuilderTimeSplit.MinimumNoteLength /= 2;
            rythmPatternBuilderTimeSplit.MaximumNoteLength /= 2;

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("trance");
            descriptionTagList.Add("synth");
            descriptionTagList.Add("lead");
            return descriptionTagList;
        }

        public override bool IsDrum
        {
            get { return false; }
        }

        public override int BuildPreferedTempo(Random random)
        {
            return random.Next(125, 140);
        }

        public override bool IsUltraRigidDrum
        {
            get { return false; }
        }
    }
}
