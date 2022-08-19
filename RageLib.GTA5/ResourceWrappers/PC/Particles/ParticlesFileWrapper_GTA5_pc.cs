// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Particles;
using RageLib.ResourceWrappers.Particles;
using System;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Particles
{
    public class ParticlesFileWrapper_GTA5_pc : IParticlesFile
    {
        private ParticleEffectsList particles;

        public IParticles Particles
        {
            get
            {
                return new ParticlesWrapper_GTA5_pc(particles);
            }
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<ParticleEffectsList>();
            resource.Load(fileName);

            particles = resource.ResourceData;
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<ParticleEffectsList>();
            resource.ResourceData = particles;
            resource.Version = 68;
            resource.Save(fileName);
        }

        public void Load(Stream stream)
        {
            var resource = new Resource7<ParticleEffectsList>();
            resource.Load(stream);

            if (resource.Version != 68)
                throw new Exception("version error");

            particles = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<ParticleEffectsList>();
            resource.ResourceData = particles;
            resource.Version = 68;
            resource.Save(stream);
        }
    }
}