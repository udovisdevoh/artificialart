﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    internal class MetaRiffOrchestraHitHipHop : MetaRiff
    {
        public override int BuildPreferedMidiInstrument(Random random)
        {
            return 55;
        }

        public override int BuildMinimumVelocity(Random random)
        {
            return 0;
        }

        public override int BuildMaximumVelocity(Random random)
        {
            return 100;
        }

        public override int BuildPreferedMidPitch(Random random)
        {
            return 64;
        }

        public override int BuildPreferedRadius(Random random)
        {
            return 18;
        }

        public override ScaleChooser BuildScaleChooser()
        {
            ScaleChooser scaleChooser = new ScaleChooser();
            scaleChooser.Add(Scales.MinorPentatonic);
            scaleChooser.Add(Scales.Japanese);
            scaleChooser.Add(Scales.Hexatonic);
            scaleChooser.Add(Scales.Blues);
            scaleChooser.Add(Scales.Indonesian);
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

            wavePack.Add(new Wave(random.NextDouble() * 0.3, 4, phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble() * 0.3, 6, phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble() * 0.3, 8, phase3, waveFunction3));

            wavePack.Normalize();

            return wavePack;
        }

        public override RythmPattern BuildRythmPattern(Random random)
        {
            rythmPatternBuilderTimeSplit.MaximumNoteLength *= 2.0;
            rythmPatternBuilderTimeSplit.MinimumNoteLength *= 2.0;
            rythmPatternBuilderTimeSplit.DesiredRythmLength = 0.25 * random.Next(1, 3);

            rythmPatternBuilderTimeSplit.Random = random;
            rythmPatternBuilderTimeSplit.IsAllowedTernary = false;
            rythmPatternBuilderTimeSplit.IsAllowedQuinternary = false;
            RythmPattern rythmPattern = rythmPatternBuilderTimeSplit.Build();
            return rythmPattern;
        }

        public override IEnumerable<string> GetDescriptionTagList()
        {
            List<string> descriptionTagList = new List<string>();
            descriptionTagList.Add("techno");
            descriptionTagList.Add("pop");
            descriptionTagList.Add("orchestra hit");
            descriptionTagList.Add("synth");
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
