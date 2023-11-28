﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Drawables;
using RageLib.GTA5.ResourceWrappers.PC.Fragments;
using RageLib.GTA5.ResourceWrappers.PC.Particles;
using RageLib.GTA5.ResourceWrappers.PC.Textures;
using RageLib.ResourceWrappers;
using RageLib.ResourceWrappers.Drawables;
using RageLib.ResourceWrappers.Fragments;
using RageLib.ResourceWrappers.Particles;
using System;
using System.Collections.Generic;

namespace TextureTool.Models
{
    public enum FileType
    {
        TextureDictionaryFile,
        DrawableDictionaryFile,
        DrawableFile,
        FragmentFile,
        ParticlesFile,
        None
    }

    public class MainModel
    {
        private ITextureDictionaryFile textureDictionaryFile;
        private IDrawableDictionaryFile drawableDictionaryFile;
        private IDrawableFile drawableFile;
        private IFragmentFile fragmentFile;
        private IParticlesFile particlesFile;
        private string fileName;

        public FileType FileType
        {
            get
            {
                if (textureDictionaryFile != null)
                    return FileType.TextureDictionaryFile;
                else if (drawableDictionaryFile != null)
                    return FileType.DrawableDictionaryFile;
                else if (drawableFile != null)
                    return FileType.DrawableFile;
                else if (fragmentFile != null)
                    return FileType.FragmentFile;
                else if (particlesFile != null)
                    return FileType.ParticlesFile;
                return FileType.None;
            }
        }

        public List<TextureDictionaryModel> TextureDictionaries
        {
            get
            {
                var list = new List<TextureDictionaryModel>();
                if (textureDictionaryFile != null)
                {
                    list.Add(new TextureDictionaryModel(textureDictionaryFile.TextureDictionary));
                }
                else if (drawableDictionaryFile != null)
                {
                    if (drawableDictionaryFile.DrawableDictionary.Drawables != null)
                    {
                        for (int i = 0; i < drawableDictionaryFile.DrawableDictionary.Drawables.Count; i++)
                        {
                            var drawable = drawableDictionaryFile.DrawableDictionary.Drawables[i];
                            var drawableName = "0x" + drawableDictionaryFile.DrawableDictionary.GetHash(i).ToString("X8");
                            if (drawable.ShaderGroup != null &&
                               drawable.ShaderGroup.TextureDictionary != null)
                            {
                                list.Add(new TextureDictionaryModel(drawable.ShaderGroup.TextureDictionary, drawableName));
                            }
                            else
                            {
                                list.Add(new TextureDictionaryModel(null, drawableName));
                            }
                        }
                    }
                }
                else if (drawableFile != null)
                {
                    if (drawableFile.Drawable.ShaderGroup != null &&
                        drawableFile.Drawable.ShaderGroup.TextureDictionary != null)
                    {
                        list.Add(new TextureDictionaryModel(drawableFile.Drawable.ShaderGroup.TextureDictionary));
                    }
                }
                else if (fragmentFile != null)
                {
                    if (fragmentFile.FragType.Drawable1 != null &&
                        fragmentFile.FragType.Drawable1.ShaderGroup != null &&
                        fragmentFile.FragType.Drawable1.ShaderGroup.TextureDictionary != null)
                    {
                        list.Add(new TextureDictionaryModel(fragmentFile.FragType.Drawable1.ShaderGroup.TextureDictionary, fragmentFile.FragType.Drawable1.Name));
                    }
                    if (fragmentFile.FragType.Drawable2 != null &&
                        fragmentFile.FragType.Drawable2.ShaderGroup != null &&
                        fragmentFile.FragType.Drawable2.ShaderGroup.TextureDictionary != null)
                    {
                        list.Add(new TextureDictionaryModel(fragmentFile.FragType.Drawable2.ShaderGroup.TextureDictionary, fragmentFile.FragType.Drawable2.Name));
                    }
                }
                else if (particlesFile != null)
                {
                    list.Add(new TextureDictionaryModel(particlesFile.Particles.TextureDictionary));
                }

                return list;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        public void New()
        {
            this.textureDictionaryFile = new TextureDictionaryFileWrapper_GTA5_pc();
            this.drawableDictionaryFile = null;
            this.drawableFile = null;
            this.fragmentFile = null;
            this.particlesFile = null;
            this.fileName = null;
        }

        public void Load(string fileName)
        {
            if (fileName.EndsWith(".ytd"))
            {
                this.textureDictionaryFile = new TextureDictionaryFileWrapper_GTA5_pc();
                this.textureDictionaryFile.Load(fileName);
                this.drawableDictionaryFile = null;
                this.drawableFile = null;
                this.fragmentFile = null;
                this.particlesFile = null;
                this.fileName = fileName;
            }
            else if (fileName.EndsWith(".ydd"))
            {
                this.textureDictionaryFile = null;
                this.drawableDictionaryFile = new DrawableDictionaryFileWrapper_GTA5_pc();
                this.drawableDictionaryFile.Load(fileName);
                this.drawableFile = null;
                this.fragmentFile = null;
                this.particlesFile = null;
                this.fileName = fileName;
            }
            else if (fileName.EndsWith(".ydr"))
            {
                this.textureDictionaryFile = null;
                this.drawableDictionaryFile = null;
                this.drawableFile = new DrawableFileWrapper_GTA5_pc();
                this.drawableFile.Load(fileName);
                this.fragmentFile = null;
                this.particlesFile = null;
                this.fileName = fileName;
            }
            else if (fileName.EndsWith(".yft"))
            {
                this.textureDictionaryFile = null;
                this.drawableDictionaryFile = null;
                this.drawableFile = null;
                this.fragmentFile = new FragmentFileWrapper_GTA5_pc();
                this.fragmentFile.Load(fileName);
                this.particlesFile = null;
                this.fileName = fileName;
            }
            else if (fileName.EndsWith(".ypt"))
            {
                this.textureDictionaryFile = null;
                this.drawableDictionaryFile = null;
                this.drawableFile = null;
                this.fragmentFile = null;
                this.particlesFile = new ParticlesFileWrapper_GTA5_pc();
                this.particlesFile.Load(fileName);
                this.fileName = fileName;
            }
            else
            {
                throw new Exception("Unsupported file type.");
            }
        }

        public void Save(string fileName)
        {
            if (textureDictionaryFile != null)
            {
                this.textureDictionaryFile.Save(fileName);
                this.fileName = fileName;
            }
            else if (drawableDictionaryFile != null)
            {
                this.drawableDictionaryFile.Save(fileName);
                this.fileName = fileName;
            }
            else if (drawableFile != null)
            {
                this.drawableFile.Save(fileName);
                this.fileName = fileName;
            }
            else if (fragmentFile != null)
            {
                this.fragmentFile.Save(fileName);
                this.fileName = fileName;
            }
            else if (particlesFile != null)
            {
                this.particlesFile.Save(fileName);
                this.fileName = fileName;
            }
        }
    }
}