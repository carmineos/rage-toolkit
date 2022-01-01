// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.GTA5.PC.Particles;
using RageLib.ResourceWrappers;
using RageLib.GTA5.ResourceWrappers.PC.Textures;
using RageLib.ResourceWrappers.Particles;

namespace RageLib.GTA5.ResourceWrappers.PC.Particles
{
    public class ParticlesWrapper_GTA5_pc : IParticles
    {
        private ParticleEffectsList particles;

        public ITextureDictionary TextureDictionary
        {
            get
            {
                if (particles.TextureDictionary != null)
                    return new TextureDictionaryWrapper_GTA5_pc(particles.TextureDictionary);
                else
                    return null;
            }
        }

        public ParticlesWrapper_GTA5_pc(ParticleEffectsList particles)
        {
            this.particles = particles;
        }
    }
}