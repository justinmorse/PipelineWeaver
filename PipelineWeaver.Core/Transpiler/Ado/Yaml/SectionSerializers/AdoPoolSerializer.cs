using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers.Interfaces;

namespace PipelineWeaver.Core.Transpiler.Ado.Yaml.SectionSerializers
{
    public class AdoPoolSerializer : IAdoYamlSectionSerializer
    {
        internal AdoYamlBuilder _builder = new AdoYamlBuilder();
        public void AppendSection(AdoSectionBase section, AdoYamlBuilder? builder, int startingIndent)
        {
            var pool = section as AdoSectionCollection<AdoPoolBase> ?? throw new ArgumentException(nameof(section));
            if (builder is not null)
                _builder = builder;

            AppendPools(pool, startingIndent);
        }

        private void AppendPools(AdoSectionCollection<AdoPoolBase> pool, int startingIndent)
        {
            if (pool.Count > 0)
            {
                _builder.AppendLine(startingIndent, "pools:");
                pool.ToList().ForEach(p =>
                {
                    switch (p)
                    {
                        case AdoHostedPool hostedPool:
                            {
                                AppendHostedPool(hostedPool, startingIndent);
                                break;
                            }
                        case AdoNamedPool pool:
                            {
                                AppendPool(pool, startingIndent);
                                break;
                            }
                        default:
                            throw new ArgumentException(nameof(pool));
                    }
                });
            }
        }

        internal void AppendHostedPool(AdoHostedPool pool, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 2, $"vmImage: {pool.VmImage}");
        }

        internal void AppendPool(AdoNamedPool pool, int startingIndent)
        {
            _builder.AppendLine(startingIndent + 2, $"name: {pool.Name}");
            _builder.AppendList("demands", startingIndent + 2, pool.Demands);
        }
    }
}