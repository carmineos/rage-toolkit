// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.PSOWrappers;
using RageLib.GTA5.RBF;
using RageLib.GTA5.RBFWrappers;
using RageLib.GTA5.ResourceWrappers.PC.Meta;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions;
using RageLib.Services;

namespace RageLib.GTA5.Services
{
    public class MetaConverter
    {
        private readonly JenkinsDictionary joaatDictionary;
        private MetaDefinitions metaDefinitions;
        
        public MetaConverter(JenkinsDictionary dictionary)
        {
            this.joaatDictionary = dictionary ?? new JenkinsDictionary();
        }

        public void ConvertXmlToResource(string filePath)
        {
            string inputFileName = filePath;
            string outputFileName = inputFileName.Replace(".xml", "");

            // TODO: Maybe provide definitions with DI
            if (metaDefinitions is null)
                metaDefinitions = MetaDefinitions.LoadEmbedded();

            var importer = new MetaXmlImporter2(metaDefinitions);
            var imported = importer.Import(inputFileName);

            var writer = new MetaWriter(metaDefinitions);
            writer.Write(imported, outputFileName);
        }

        public void ConvertResourceToXml(string filePath)
        {
            string inputFileName = filePath;
            string outputFileName = inputFileName + ".xml";

            var reader = new MetaReader();
            var meta = reader.Read(inputFileName);
            var exporter = new MetaXmlExporter();
            exporter.HashMapping = joaatDictionary;
            exporter.Export(meta, outputFileName);
        }

        public void ConvertPsoToXml(string filePath)
        {
            string inputFileName = filePath;
            string outputFileName = inputFileName + ".pso.xml";

            var reader = new PsoReader();
            var meta = reader.Read(inputFileName);
            var exporter = new PsoXmlExporter();
            exporter.HashMapping = joaatDictionary;
            exporter.Export(meta, outputFileName);
        }

        public void ConvertRbfToXml(string filePath)
        {
            string inputFileName = filePath;
            string outputFileName = inputFileName + ".rbf.xml";

            var rbf = new RbfFile().Load(inputFileName);
            new RbfXmlExporter().Export(rbf, outputFileName);
        }
    }
}
