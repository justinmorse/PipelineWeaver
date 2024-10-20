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
        public void AppendSection(AdoSectionBase section, AdoYamlBuilder builder, int startingIndent)
        {
            var pool = section as AdoSectionCollection<IAdoPool> ?? throw new ArgumentException(nameof(section));

            AppendPools(pool);

            builder.AppendLine(startingIndent, _builder.ToString(), true, true);
        }

        private void AppendPools(AdoSectionCollection<IAdoPool> pool)
        {
            if (pool.Count > 0)
            {
                _builder.AppendLine(0, "pools:");
                pool.ToList().ForEach(p =>
                {
                    switch (p)
                    {
                        case AdoHostedPool hostedPool:
                            {
                                AppendHostedPool(hostedPool);
                                break;
                            }
                        case AdoNamedPool pool:
                            {
                                AppendPool(pool);
                                break;
                            }
                        default:
                            throw new ArgumentException(nameof(pool));
                    }
                });
            }
        }

        internal void AppendHostedPool(AdoHostedPool pool)
        {
            _builder.AppendLine(2, $"vmImage: {pool.VmImage}");
        }

        internal void AppendPool(AdoNamedPool pool)
        {
            _builder.AppendLine(2, $"name: {pool.Name}");
            _builder.AppendArray("demands", 2, pool.Demands?.ToArray());
        }
    }
}