﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Waves;

namespace ArtificialArt.Audio.Midi.Generator
{
    /// <summary>
    /// Create key modulators
    /// </summary>
    class ModulatorBuilder
    {
        #region Public Method
        /// <summary>
        /// Create  modulator
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="modulationStrength">0: none 1: full</param>
        /// <returns>New key modulator</returns>
        public Modulator Build(Random random, double modulationStrength)
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

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random);

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble(), 1, phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble(), 2, phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble(), 3, phase3, waveFunction3));
            wavePack.Normalize();

            return new Modulator(wavePack, modulationStrength);
        }
        #endregion
    }
}